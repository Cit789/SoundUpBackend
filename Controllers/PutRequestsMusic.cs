using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.Contracts;
using SoundUp.Interfaces.Repository;


namespace SoundUp.Controllers
{
    
    [Controller]
    [Route("api/[controller]/[action]")]
    public class PutRequestsMusic(ApplicationDbContext dbContext, IMusicRepository musicRepository) : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext = dbContext;
        private readonly IMusicRepository _musicRepository = musicRepository;
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutMusicInPlaylist([FromBody] AddMusicInPlaylistRequest addMusicInPlaylistRequest)
        {
            var Music = await _musicRepository.GetMusicById(addMusicInPlaylistRequest.MusicId);
            var Playlist = await _dbcontext.PlayLists
                .Include(p => p.MusicList)
                .FirstOrDefaultAsync(x => x.Id == addMusicInPlaylistRequest.PlaylistId);

            if (Playlist != null && Music != null && !Playlist.MusicList.Any(m => m.Id == addMusicInPlaylistRequest.MusicId))
            {
                Playlist.MusicList.Add(Music);
                await _dbcontext.SaveChangesAsync();
                return Ok("Музыка добавлена");
            }
            else if (Playlist == null && Music == null)
            {
                return NotFound("Музыка и плейлист не найдены");
            }
            else if (Playlist == null || Music == null)
            {
                return Playlist == null ? NotFound("Пользователь не найден") : NotFound("Музыка не найдена");
            }
            else if (Playlist.MusicList.Any(m => m.Id == addMusicInPlaylistRequest.MusicId))
            {
                return Conflict("Музыка уже добавлена");
            }

            return StatusCode(500, "Ошибка сохранения данных");


        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> PutMusicInFavourite([FromBody] AddMusicInFavouritesRequest addMusicInFavouritesRequest)
        {
            var Music = await _dbcontext.Music
                .FirstOrDefaultAsync(m => m.Id == addMusicInFavouritesRequest.MusicId);

            var User = await _dbcontext.AllUsers
                .Include(u => u.Favorites)
                .FirstOrDefaultAsync(u => u.Id == addMusicInFavouritesRequest.UserId);

            if (User != null && Music != null && !User.Favorites.Any(m => m.Id == addMusicInFavouritesRequest.MusicId))
            {
                User.Favorites.Add(Music);
                await _dbcontext.SaveChangesAsync();
                return Ok("Музыка добавлена");
            }
            else if (User == null && Music == null)
            {
                return NotFound("Музыка и пользователь не найдены");
            }
            else if (User == null || Music == null)
            {
                return User == null ? NotFound("Пользователь не найден") : NotFound("Музыка не найдена");
            }
            else if (User.Favorites.Any(m => m.Id == addMusicInFavouritesRequest.MusicId))
            {
                return Conflict("Музыка уже добавлена");
            }

            return StatusCode(500,"Ошибка сохранения данных");



        }

    }
}
