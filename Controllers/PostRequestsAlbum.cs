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

        

            return await _albumRepository.PostNewAlbum(RequestAlbum) ? Ok("Альбом сохранен") : StatusCode(500, "Ошибка сохранения");

        }
    }
}
