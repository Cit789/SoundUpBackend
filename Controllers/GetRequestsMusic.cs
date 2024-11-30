using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundUp.Dto;
using SoundUp.Interfaces.Repository;


namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class GetRequestsMusic(IMusicRepository musicRepository) : ControllerBase
    {

        
        private readonly IMusicRepository _musicRepository = musicRepository;
        private const string PAGINATION_ERROR = "Номер страницы или ее размер не может быть отрицательным,(Отсчет страниц начинается с 1)";
        private const string MUSIC_NOTFOUND_ERROR = "музыка не найдена";

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AllMusics(int Page, int PageSize, Guid UserId)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest(PAGINATION_ERROR);
            }
            var Music = await _musicRepository.GetAllMusicWithPagination(Page, PageSize, UserId);

            return Music.Count == 0 ? NotFound(MUSIC_NOTFOUND_ERROR) : Ok(Music);
        }

        [HttpGet]
        public async Task<IActionResult> CreatedAuthorMusic(int Page, int PageSize, Guid AuthorId, Guid UserId)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest(PAGINATION_ERROR);
            }
            var Musics = await _musicRepository.GetCreatedAuthorMusic(Page, PageSize, AuthorId, UserId);
            return Musics.Count == 0 ? NotFound(MUSIC_NOTFOUND_ERROR) : Ok(Musics);
        }


        [HttpGet]
        public async Task<IActionResult> PlayListMusic(int Page, int PageSize, Guid PlaylistId, Guid UserId)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest(PAGINATION_ERROR);
            }

            var Musics = await _musicRepository.GetMusicInPlaylist(Page, PageSize, PlaylistId, UserId);
            return Musics.Count == 0 ? NotFound(MUSIC_NOTFOUND_ERROR) : Ok(Musics);
        }
        [HttpGet]
        public async Task<IActionResult> GetMusicById(Guid MusicId, Guid UserId, bool WithAudio)
        {
            var MusicTask = _musicRepository.GetMusicByIdInDto(MusicId, UserId);
            string MusicAudio = string.Empty;
            if (WithAudio)
            {
                var Audio = await _musicRepository.GetMusicAudio(MusicId);
                MusicAudio = Audio;
               
            }

            var Music = await MusicTask;
            if(Music == null) return NotFound("Музыка не найдена");
            return Ok(new MusicWithAudioDto(Music, MusicAudio));
        }




    }
}
