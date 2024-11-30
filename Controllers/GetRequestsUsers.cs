using Microsoft.AspNetCore.Mvc;
using SoundUp.Dto;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;

namespace SoundUp.Controllers
{

    [Controller]
    [Route("/api/[controller]/[action]")]
    public class GetRequestsUsers(IUserRepository userRepository) : ControllerBase
    {

        
        private readonly IUserRepository _userRepository = userRepository;

        [HttpGet]
        public async Task<IActionResult> GetUser(Guid UserId)
        {
           var FindedUser = await _userRepository.GetUserById(UserId);
           string UserType = FindedUser is UserAuthor ? "UserAuthor" : "User";
            if (FindedUser != null)
            {
                return Ok(new UserDto(FindedUser.Id, FindedUser.Name, FindedUser.Password, UserType, FindedUser.Avatar, FindedUser.CreatedAt, FindedUser.UpdatedAt));
            }
            return NotFound("Пользователь не найден");
        }
    }
}
