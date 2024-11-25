using Microsoft.AspNetCore.Mvc;
using SoundUp.Contracts;
using SoundUp.Interfaces.Auth;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;


namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class PostRequestsUsers(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvaider = jwtProvider;

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] CreateAuthorOrUserRequest createUserRequest)
        {

            string token = string.Empty;

            if (createUserRequest.UserType == "UserAuthor")
                token = await _userRepository.CreateUser<UserAuthor>(createUserRequest);

            else if (createUserRequest.UserType == "DefaultUser")
                token = await _userRepository.CreateUser<UserAuthor>(createUserRequest);

            if (!string.IsNullOrEmpty(token))
            {

                HttpContext.Response.Cookies.Append("cookie", token);
                return Ok("Пользователь создан");
            }
            return StatusCode(500, "Ошибка сохранения данных");
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest user)
        {
            var FindedUser = await _userRepository.GetUserByUserName(user.UserName);

            if (FindedUser != null && _passwordHasher.Verify(user.Password, FindedUser.Password))
            {
                var token = _jwtProvaider.GenerateToken(FindedUser);
                HttpContext.Response.Cookies.Append("cookie", token);
                return Ok(token);
            }

            return NotFound("Ошибка логина");

        }
    }
}
