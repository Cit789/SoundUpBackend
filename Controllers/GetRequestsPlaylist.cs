using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.Dto;
using SoundUp.Interfaces.Repository;

namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class GetRequestsPlaylist(IPlaylistRepository playlistRepository,ApplicationDbContext dbcontext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        private readonly IPlaylistRepository _playlistRepository = playlistRepository;
        private const string PAGINATION_ERROR = "Номер страницы или ее размер не может быть отрицательным,(Отсчет страниц начинается с 1)";

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPlaylistById([FromHeader] Guid PlaylistId)
        {
            var Playlist = await _playlistRepository.GetPlayListById(PlaylistId);
            if(Playlist == null) return NotFound("Плейлист не найден");

            return Ok(new PlaylistDto(Playlist.Id,Playlist.Avatar,Playlist.Avatar,Playlist.CreatorId,Playlist.CreatedAt,Playlist.UpdatedAt));
        }
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserPlaylists(int Page, int PageSize, [FromHeader] Guid UserId)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest(PAGINATION_ERROR);
            }
            var Playlists = await _playlistRepository.GetUserPlaylists(Page, PageSize,UserId);
            if (Playlists.Count == 0) return NotFound("Плейлисты не найдены");
            return Ok(Playlists);
        }
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserCountPlaylists([FromHeader] Guid UserId)
        {

           var User =await _dbcontext.AllUsers
                .AsNoTracking()
                .Include(u => u.Playlists)
                .FirstOrDefaultAsync(u => u.Id == UserId);
            if (User == null) return NotFound("Пользвоатель не найден");
            return Ok(User.Playlists.Count);
        }
    }
}
