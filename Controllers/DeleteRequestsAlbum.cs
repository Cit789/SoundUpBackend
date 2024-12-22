using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.Models;
using System.Security.Claims;

namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class DeleteRequestsAlbum(ApplicationDbContext dbcontext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;

        [HttpDelete]
        public async Task<ActionResult> DeleteAlbum([FromHeader] Guid AlbumId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var Album = await _dbcontext.Albums.FirstOrDefaultAsync(m => m.Id == AlbumId);

            if (Album == null) return NotFound("Альбом не найден");

            if (Album.AuthorId.ToString() != userId)
                return Unauthorized("Нет доступа");

            _dbcontext.Albums.Remove(Album);
            await _dbcontext.SaveChangesAsync();
            return Ok("Альбом удален");
        }
    }
}