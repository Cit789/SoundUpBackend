using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundUp.Contracts;
using SoundUp.Interfaces.Repository;


namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class PostRequestsMusic(IAuthorRepository authorRepository, IMusicRepository musicRepository) : ControllerBase
    {

        private readonly IAuthorRepository _authorRepository = authorRepository;
        private readonly IMusicRepository _musicRepository = musicRepository;

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostMusic([FromBody] CreateMusicRequest RequestMusic)
        {
            if (await _authorRepository.IsAuthorCreateThisMusic(RequestMusic.AuthorId, RequestMusic.Name)) return Conflict("Музыка с таким именем уже существует");

            return await _musicRepository.CreateMusic(RequestMusic) ? Ok("Музыка создана") : StatusCode(500, "Ошибка сохранения данных");
        }

    }
}
