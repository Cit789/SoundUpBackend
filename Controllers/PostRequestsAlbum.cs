using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundUp.Contracts;
using SoundUp.Interfaces.Repository;


namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class PostRequestsAlbum(IAlbumRepository albumRepository, IAuthorRepository authorRepository) : ControllerBase
    {
        private readonly IAlbumRepository _albumRepository = albumRepository;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAlbum([FromBody] CreateAlbumRequest RequestAlbum)
        {

            var Album = await _albumRepository.PostNewAlbum(RequestAlbum);

            return Album is null ? StatusCode(500, "Ошибка, возможно автор уже создал альбом с таким именем") : Ok(Album.Id);

        }
    }
}
