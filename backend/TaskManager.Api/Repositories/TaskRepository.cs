using Microsoft.Data.SqlClient;
using System.Xml.Linq;
using TaskManager.Api.Models;

namespace TaskManager.Api.Repositories
{
    public class TaskRepository
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _queries;

        public TaskRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found");
            _queries = LoadQueries();
        }

        private Dictionary<string, string> LoadQueries()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Queries", "TaskQueries.xml");
            var doc = XDocument.Load(path);
            var result = new Dictionary<string, string>();

            foreach (var element in doc.Descendants("query"))
            {
                var name = element.Attribute("name")?.Value;
                var sql = element.Value.Trim();
                if (name != null)
                {
                    result[name] = sql;
                }
            }

            return result;
        }


        public List<TaskItem> GetTasksByUserId(int userId)
        {
            var tasks = new List<TaskItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(_queries["GetTasksByUserId"], connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(MapReaderToTask(reader));
                        }
                    }
                }
            }

            return tasks;
        }

        private TaskItem MapReaderToTask(SqlDataReader reader)
        {
            return new TaskItem

            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Title = reader.GetString(reader.GetOrdinal("Title")),
                Description = reader.IsDBNull(reader.GetOrdinal("Description"))
                   ? null
                   : reader.GetString(reader.GetOrdinal("Description")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                DueDate = reader.IsDBNull(reader.GetOrdinal("DueDate"))
                  ? null
                  : reader.GetDateTime(reader.GetOrdinal("DueDate"))


            };
        }
    


        public int CreateTask(TaskItem task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_queries["CreateTask"], connection))
                {
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", task.Description ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Status", task.Status);
                    command.Parameters.AddWithValue("@UserId", task.UserId);
                    command.Parameters.AddWithValue("@DueDate", task.DueDate ?? (object)DBNull.Value);

                    var newId = (int)command.ExecuteScalar();
                    return newId;
                }


            }


        }




        public void UpdateTaskStatus(int taskId, string newStatus)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_queries["UpdateTaskStatus"], connection))
                {
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    command.Parameters.AddWithValue("@Status", newStatus);

                    command.ExecuteNonQuery();


                }
            }
        }


        public void DeleteTask(int taskId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_queries["DeleteTask"] , connection))
                {
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    command.ExecuteNonQuery();



                }

                    
                    
            }
        }
    }

}
