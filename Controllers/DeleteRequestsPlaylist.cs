using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class DeleteRequestsPlaylist(ApplicationDbContext dbcontext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePlaylist([FromHeader] Guid UserId, Guid id)
        {
           var Changes = await _dbcontext.PlayLists
                .Where(p => p.Id == id && p.CreatorId == UserId)
                .ExecuteDeleteAsync();

            
            return Changes == 0 ? BadRequest("Плейлист не найден, или пользвоатель не является создателем плейлиста") : Ok("Музыка удалена из плейлиста");
        }
    }
}
