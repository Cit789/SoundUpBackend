using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SoundUp.Controllers
{
    [Controller]
    [Route("/api/[controller]/[action]")]
    public class DeleteRequestsUser(ApplicationDbContext dbcontext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
           var count = await _dbcontext.AllUsers.Where(u => u.Id == id).ExecuteDeleteAsync();

           return count != 0 ? Ok("Пользователь удален") : BadRequest("Пользователь не найден, возможно произошла ошибка при удалени");
            
        }
    }
}
