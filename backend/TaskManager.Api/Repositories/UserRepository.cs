using Microsoft.Data.SqlClient;
using System.Xml.Linq;
using TaskManager.Api.Models;

namespace TaskManager.Api.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _queries;

        public UserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found");
            _queries = LoadQueries();
        }

        private Dictionary<string, string> LoadQueries()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Queries", "UserQueries.xml");
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


            public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_queries["GetAllUsers"], connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(MapReaderToUser(reader));
                    }
                }
            }
            return users;
        }

        private User MapReaderToUser(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
            };
        }

        public int CreateUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(_queries["CreateUser"], connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    var newId = (int)command.ExecuteScalar();
                    return newId;
                }
            }
        }





    }
    
}