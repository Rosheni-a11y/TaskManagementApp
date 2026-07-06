using TaskManager.Api.Models;
using TaskManager.Api.Repositories;

namespace TaskManager.Api.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;
        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public List<User> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }
        public int CreateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentException("Name and Email are requird");

            }
            return _repository.CreateUser(user);

        }
    }
}