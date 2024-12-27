using Microsoft.AspNetCore.Mvc;

namespace SoundUp.Controllers
{
    [Controller]
    [Route("/[controller]/[action]")]
    public class PutRequestAlbum(ApplicationDbContext dbcontext) : Controller
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;

        [HttpPut]
        public async Task<ActionResult> AddAlbumInFavorite([FromHeader] Guid AlbumId, [FromHeader] Guid UserId)
        {
            var Album = _dbcontext.Albums.FirstOrDefault(a => a.Id == AlbumId);
            var User = _dbcontext.AllUsers.FirstOrDefault(a => a.Id == UserId);

            if(User is null || Album is null) return NotFound("Альбом или пользователь не найдены");

            User.FavoriteAlbums.Add(Album);
            await _dbcontext.SaveChangesAsync();
            return Ok("Альбом добавлен в фавориты");
        
        }
    }

}
