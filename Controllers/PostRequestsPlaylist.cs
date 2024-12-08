using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundUp.Contracts;
using SoundUp.Interfaces.Repository;


namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class PostRequestsPlaylist(IPlaylistRepository playlistRepository) : ControllerBase
    {

        private readonly IPlaylistRepository _playlistRepository = playlistRepository;
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> PostPlaylist([FromBody] CreatePlaylistRequest createPlaylistRequest)
        {
            var Playlist = await _playlistRepository.CreatePlaylist(createPlaylistRequest);
            return Playlist is null ?  Conflict("Ошибка, плейлист не создан, автор может быть не найден") : Ok(Playlist.Id);

        }
    }
}
