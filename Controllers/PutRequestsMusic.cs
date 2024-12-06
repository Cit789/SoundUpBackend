using Microsoft.AspNetCore.Mvc;
using SoundUp.Contracts;
using SoundUp.Models;



namespace SoundUp.Controllers
{
    
    [Controller]
    [Route("api/[controller]/[action]")]
    public class PutRequestsMusic(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext = dbContext;
      
        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> PutMusicInPlaylist([FromBody] AddMusicInPlaylistRequest addMusicInPlaylistRequest)
        {
           

            var PlaylistMusic = new PlaylistMusic()
            {
                PlaylistId = addMusicInPlaylistRequest.PlaylistId,
                MusicId = addMusicInPlaylistRequest.MusicId
            };
            try
            {

                await _dbcontext.PlaylistMusic.AddAsync(PlaylistMusic);
                var Changes = await _dbcontext.SaveChangesAsync();

                return Changes > 0 ? Ok("Музыка добавлена") : BadRequest("Неккоректные значения");
            }
            catch
            {
                return BadRequest("Плейлист уже добавлен или айди являются ошибочными");
            }

        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> PutMusicInFavourite([FromBody] AddMusicInFavouritesRequest addMusicInFavouritesRequest)
        {
            var userMusicFavorite = new UserMusicFavorites()
            {
                UserId = addMusicInFavouritesRequest.UserId,
                MusicId = addMusicInFavouritesRequest.MusicId
            };

            try
            {

                await _dbcontext.UserMusicFavorite.AddAsync(userMusicFavorite);
                var Changes = await _dbcontext.SaveChangesAsync();

                return Changes > 0 ? Ok("Музыка добавлена") : BadRequest("Неккоректные значения");
            }
            catch
            {
                return BadRequest("Музыка уже добавлена или айди являются ошибочными");
            }

            
           



        }

    }
}
