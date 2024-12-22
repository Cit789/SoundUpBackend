using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.Models;
using System.Security.Claims;

namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class DeleteRequestsMusic(ApplicationDbContext dbcontext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMusicFromPlaylist([FromHeader] Guid PlaylistId, [FromHeader] Guid CreatorId,Guid id) 
        {
            var Playlist = await _dbcontext.PlayLists
                .Include(p => p.MusicList)
                .FirstOrDefaultAsync(p => p.Id == PlaylistId);

            if (Playlist == null) return NotFound("Плейлист не найден");
            var Music = Playlist.MusicList
                .FirstOrDefault(ml => ml.MusicId == id);

            if (Playlist.CreatorId != CreatorId) return Conflict("Пользователь не имеет прав на удаление музыки");

            if (Music == null) return NotFound("Музыка не найдена");
            Playlist.MusicList.Remove(Music);

        
            await _dbcontext.SaveChangesAsync();
            return Ok("Музыка удалена из плейлиста");
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMusicFromFavorites([FromHeader] Guid UserId, Guid id)
        {
            var User = await _dbcontext.AllUsers
                .Include(p => p.Favorites)
                .FirstOrDefaultAsync(p => p.Id == UserId);

            if (User == null) return NotFound("Пользователь не найден");
            var Music = User.Favorites
                .FirstOrDefault(ml => ml.MusicId == id);

            if (Music == null) return NotFound("Музыка не найдена");
            User.Favorites.Remove(Music);


            await _dbcontext.SaveChangesAsync();
            return Ok("Музыка удалена из любимых треков");
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteMusic([FromHeader] Guid MusicId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           
            var music = await _dbcontext.Music.FirstOrDefaultAsync(m => m.Id == MusicId);

            if (music == null) return NotFound("Музыка не найдена");

            if (music.AuthorId.ToString() != userId)
                return  Unauthorized("Нет доступа");

            _dbcontext.Music.Remove(music);
            await _dbcontext.SaveChangesAsync();
            return Ok("Музыка удалена");
        }

    }
}
