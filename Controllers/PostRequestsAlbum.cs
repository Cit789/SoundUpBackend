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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAlbum([FromBody] CreateAlbumRequest RequestAlbum)
        {

            if (await _authorRepository.IsAuthorHasAlbum(RequestAlbum.AuthorId, RequestAlbum.AlbumName)) return Conflict("Альбом с таким именем уже существует");

            return await _albumRepository.PostNewAlbum(RequestAlbum) ? Ok("Альбом сохранен") : StatusCode(500, "Ошибка сохранения");

        }
    }
}
