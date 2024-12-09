using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLoginRequest)
        {
            var token = await _userService.LoginAsync(userLoginRequest.UserName, userLoginRequest.Password);
            if(token == null)
            {
                return Unauthorized("the username or password is invalid");
            }

            return Ok(new {Token = token});
        }

        
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest updateUserRequest)
        {
            var logedUser = User.Identity.Name;

            var isDone = await _userService.UpdateUserAsync(logedUser, updateUserRequest.TargetUserName, updateUserRequest.NewUsename, updateUserRequest.NewPassword);
            if(!isDone)
            {
                return Forbid("the user is not found or update is not Authorized");
            }

            return Ok("The Update is done Successfully");
        }
       

        public class UserLoginRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class UpdateUserRequest
        {
            public string TargetUserName { get; set; }
            public string NewUsename { get; set; }
            public string NewPassword { get; set; }
        }
       
    }
}
