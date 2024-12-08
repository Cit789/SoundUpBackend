
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SoundUp.Contracts;
using SoundUp.Dto;
using SoundUp.Interfaces.Auth;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;


namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class PostRequestsUsers(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider) : ControllerBase
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvaider = jwtProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository = refreshTokenRepository;

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateAuthorOrUserRequest createUserRequest)
        {
           
            TokensDto tokens = new(string.Empty,string.Empty, Guid.Empty);

            
            if (createUserRequest.UserType == "UserAuthor")
                tokens = await _userRepository.CreateUser<UserAuthor>(createUserRequest);

            else if (createUserRequest.UserType == "DefaultUser")
                tokens = await _userRepository.CreateUser<UserAuthor>(createUserRequest);

            if (tokens.RefreshToken != string.Empty && tokens.JwtToken != string.Empty)
            {

                HttpContext.Response.Cookies.Append(nameof(AccessToken), tokens.JwtToken);
                HttpContext.Response.Cookies.Append(nameof(RefreshToken), tokens.RefreshToken);
                return Ok(tokens);
            }
            return StatusCode(500, "Ошибка сохранения данных");
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest user)
        {
            var FindedUser = await _userRepository.GetUserByUserName(user.UserName);

            if (FindedUser != null && _passwordHasher.Verify(user.Password, FindedUser.Password))
            {
                var JwtToken = _jwtProvaider.GenerateToken(FindedUser);
                var RefreshToken = await _refreshTokenRepository.UpdateToken(FindedUser.Id);
                
                HttpContext.Response.Cookies.Append(nameof(AccessToken), JwtToken);
                HttpContext.Response.Cookies.Append(nameof(RefreshToken), RefreshToken);
                return Ok(new TokensDto(RefreshToken,JwtToken,FindedUser.Id));
            }

            return Unauthorized("Ошибка логина");

        }
        
        [HttpPost]
        public async Task<IActionResult> GetNewRefreshAndJwtToken([FromBody] GetRegenerateTokensRequest regenerateTokensRequest)
        {
            var User = await _userRepository.GetUserById(regenerateTokensRequest.UserId);

            if (!await _refreshTokenRepository.IsValidRefreshToken(regenerateTokensRequest.RefreshToken)) return Unauthorized("Refresh токен невалиден");

            var RefreshToken = await _refreshTokenRepository.UpdateToken(regenerateTokensRequest.UserId);

            if (string.IsNullOrEmpty(RefreshToken) || User == null) return NotFound("Пользователь не существует, либо у него нет токена");
            var JwtToken = _jwtProvaider.GenerateToken(User);
            
            HttpContext.Response.Cookies.Append(nameof(AccessToken), JwtToken);
            HttpContext.Response.Cookies.Append(nameof(RefreshToken), RefreshToken);
            return Ok(new TokensDto(RefreshToken,JwtToken,User.Id));


        }

    }
}
