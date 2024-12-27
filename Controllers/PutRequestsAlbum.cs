using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var Album =await  _dbcontext.Albums.FirstOrDefaultAsync(a => a.Id == AlbumId);
            var User = await  _dbcontext.AllUsers.FirstOrDefaultAsync(a => a.Id == UserId);

            if(User is null || Album is null) return NotFound("Альбом или пользователь не найдены");
            if (User.FavoriteAlbums.Any(a => a.Id == AlbumId)) return Conflict("Альбом уже добавлен");
            User.FavoriteAlbums.Add(Album);
            await _dbcontext.SaveChangesAsync();
            return Ok("Альбом добавлен в фавориты");
        
        }
    }

}
