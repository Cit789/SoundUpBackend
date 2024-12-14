using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.Contracts;
using SoundUp.Interfaces.Auth;

namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class PutRequestsUser(ApplicationDbContext dbcontext, IPasswordHasher passwordhasher) : ControllerBase
    {
        private readonly IPasswordHasher _passwordHasher = passwordhasher;
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        [HttpPut]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileRequest NewUserData)
        {
            var User = await _dbcontext.AllUsers.FirstOrDefaultAsync(u => u.Id == NewUserData.UserId);

            if(User == null) return NotFound("Пользователь не найден");

            if (!string.IsNullOrEmpty(NewUserData.NewAvatar))
                User.Avatar = NewUserData.NewAvatar;
            if (!string.IsNullOrEmpty(NewUserData.NewUserName))
                User.Name = NewUserData.NewUserName;
            if (!string.IsNullOrEmpty(NewUserData.NewPassword))
                User.Password = _passwordHasher.Generate(NewUserData.NewPassword);
            var Count = await _dbcontext.SaveChangesAsync();

            return Count == 0 ? StatusCode(500, "Ошибка сохранения данных") : Ok("Данные обновлены");
        }
    }
}
