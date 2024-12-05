using Core.Entities;
using Core.Interfaces;
using System.Linq;

namespace Application.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string>  LoginAsync(string username, string password)
        {
            var users = await _userRepository.GetAllAsync();
            var foundUser = users.FirstOrDefault(u => (u.Username == username && u.Password == password));
            if (foundUser != null)
            {
                return null;
            }

            return JwtHelper.GenerateToken(username);
        }


    }
}
