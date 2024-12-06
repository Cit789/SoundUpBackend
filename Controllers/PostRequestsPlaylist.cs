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
            return await _playlistRepository.CreatePlaylist(createPlaylistRequest) ? Ok("плейлист создан") : Conflict("Ошибка, плейлист не создан, автор может быть не найден");

        }
    }
}
