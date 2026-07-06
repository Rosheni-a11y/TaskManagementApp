using TaskManager.Api.Models;
using TaskManager.Api.Repositories;

namespace TaskManager.Api.Services
{
    public class TaskService
    {
        private readonly TaskRepository _repository;
        

        public TaskService(TaskRepository repository)
        {
            _repository = repository;
        }

        public List<TaskItem> GetTasksByUserId(int userId)

        {
            return _repository.GetTasksByUserId(userId);
        }

        public int CreateTask(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                throw new ArgumentException("Title is required");

            }
            return _repository.CreateTask(task);
        }

        public void UpdateTaskStatus(int taskId, string newStatus)
        {
             _repository.UpdateTaskStatus(taskId , newStatus);
        }

        public void DeleteTask(int taskId)
        {
            _repository.DeleteTask(taskId);
        }
    }
}