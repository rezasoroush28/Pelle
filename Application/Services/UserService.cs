using Core.Entities;
using Core.Interfaces;
using System.Linq;

namespace Application.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly JwtHelper _jwtHelper;
        public UserService(IRepository<User> userRepository, JwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var users = await _userRepository.GetAllAsync();
            var foundUser = users.FirstOrDefault(u => (u.Username == username && u.Password == password));
            if (foundUser == null)
            {
                return null;
            }

            return _jwtHelper.GenerateToken(username);
        }

        public async Task<bool> UpdateUserAsync(string currentUsername, string targetUsername, string newUsername, string newPassword)
        {
            var Users = await _userRepository.GetAllAsync();

            var targetUser = Users.FirstOrDefault(u => u.Username == targetUsername);
            if (targetUser == null)
            {
                return false;
            }

            if (currentUsername != "user1" && targetUsername != currentUsername)
            {
                return false;
            }

            if (newUsername != "")
            {
                targetUser.Username = newUsername;
            }

            targetUser.Password = newPassword;

            await _userRepository.UpdateAsync(targetUser);

            return true;
        }

    }
}
