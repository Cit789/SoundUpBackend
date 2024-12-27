
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SoundUp.Controllers
{
    [Controller]
    [Route("/[controller]/[action]")]
    public class PutRequestPlaylist(ApplicationDbContext dbcontext) : Controller
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        [HttpPut]
        public async Task<ActionResult> PutPlaylistInUser([FromHeader] Guid UserId, [FromHeader] Guid PlaylistId)
        {
            var User = await _dbcontext.AllUsers.FirstOrDefaultAsync(u => u.Id == UserId);
            var Playlist = await _dbcontext.PlayLists.FirstOrDefaultAsync(p => p.Id == PlaylistId);

            if (User is null || Playlist is null) return NotFound("Плейлист или пользователь не найдены");
            User.Playlists.Add(Playlist);
            await _dbcontext.SaveChangesAsync();

            return Ok("Плейлист добавлен");
        }
    }
}
