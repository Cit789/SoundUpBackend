using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.DtoForUpdate;

namespace SoundUp.Controllers
{
    [Controller]
    public class PutRequests(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext = dbContext;

        [HttpPut("/AddMusicInPlaylist")]
        public async Task<IActionResult> PutMusicInPlaylist([FromBody] AddMusicInPlaylistRequest addMusicInPlaylistRequest)
        {
            var Music = await _dbcontext.Music
                .FirstOrDefaultAsync(m => m.Id == addMusicInPlaylistRequest.MusicId);
            var Playlist = await _dbcontext.PlayLists
                .Include(p => p.MusicList)
                .FirstOrDefaultAsync(x => x.Id == addMusicInPlaylistRequest.PlaylistId);

            if (Playlist != null && Music != null && !Playlist.MusicList.Contains(Music))
            {
                Playlist.MusicList.Add(Music);
                await _dbcontext.SaveChangesAsync();
                return Ok();
            }
            else if (Playlist == null && Music == null)
            {
                return NotFound("Музыка и плейлист не найдены");
            }
            else if (Playlist == null || Music == null)
            {
                return Playlist == null ? NotFound("Пользователь не найден") : NotFound("Музыка не найдена");
            }
            else if (!Playlist.MusicList.Contains(Music))
            {
                return Conflict("Музыка уже добавлена");
            }

            return StatusCode(500, "Ошибка сохранения данных");


        }
        [HttpPut("/AddMusicInFavourites")]
        public async Task<IActionResult> PutMusicInFavourite([FromBody] AddMusicInFavouritesRequest addMusicInFavouritesRequest)
        {
            var Music = await _dbcontext.Music
                .FirstOrDefaultAsync(m => m.Id == addMusicInFavouritesRequest.MusicId);

            var User = await _dbcontext.AllUsers
                .Include(u => u.Favorites)
                .FirstOrDefaultAsync(u => u.Id == addMusicInFavouritesRequest.UserId);

            if (User != null && Music != null && !User.Favorites.Contains(Music))
            {
                User.Favorites.Add(Music);
                await _dbcontext.SaveChangesAsync();
                return Ok();
            }
            else if (User == null && Music == null)
            {
                return NotFound("Музыка и пользователь не найдены");
            }
            else if (User == null || Music == null)
            {
                return User == null ? NotFound("Пользователь не найден") : NotFound("Музыка не найдена");
            }
            else if (!User.Favorites.Contains(Music))
            {
                return Conflict("Музыка уже добавлена");
            }

            return StatusCode(500,"Ошибка сохранения данных");



        }

    }
}
