using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundUp.Dto;
using SoundUp.Interfaces.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;


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
        public async Task<IActionResult> GetAllMusics(int Page, int PageSize, Guid UserId)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest(PAGINATION_ERROR);
            }
            var Music = await _musicRepository.GetAllMusicWithPagination(Page, PageSize, UserId);

            return Music.Count == 0 ? NotFound(MUSIC_NOTFOUND_ERROR) : Ok(Music);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCreatedAuthorMusic(int Page, int PageSize, Guid AuthorId, Guid UserId)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest(PAGINATION_ERROR);
            }
            var Musics = await _musicRepository.GetCreatedAuthorMusic(Page, PageSize, AuthorId, UserId);
            return Musics.Count == 0 ? NotFound(MUSIC_NOTFOUND_ERROR) : Ok(Musics);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetPlayListMusic(int Page, int PageSize, Guid PlaylistId, Guid UserId, bool IsMixed)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest(PAGINATION_ERROR);
            }

            var Musics = await _musicRepository.GetMusicInPlaylist(Page, PageSize, PlaylistId, UserId);
            if (IsMixed) Random.Shared.Shuffle(CollectionsMarshal.AsSpan(Musics));
            return Musics.Count == 0 ? NotFound(MUSIC_NOTFOUND_ERROR) : Ok(Musics);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMusicById(Guid MusicId, Guid UserId, bool WithAudio)
        {
            var Music = await _musicRepository.GetMusicByIdInDto(MusicId, UserId);
            string MusicAudio = string.Empty;
            if (WithAudio)
            {
                var Audio = await _musicRepository.GetMusicAudio(MusicId);
                MusicAudio = Audio;

            }

            
            if (Music == null) return NotFound("Музыка не найдена");
            return Ok(new MusicWithAudioDto(Music, MusicAudio));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetFavouriteMusicById(int Page,int PageSize,Guid UserId, string OrderBy)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest(PAGINATION_ERROR);
            }
            var Music = await _musicRepository.GetFavouriteMusicByUserId(Page,PageSize,UserId);

            if (Music.Count == 0) return NotFound("Музыка не найдена");
            if (OrderBy == "Desc") return Ok(Music.OrderByDescending(m => m.CreatedAt));

            return Ok(Music.OrderBy(m => m.CreatedAt));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAlbumMusic(int Page, int PageSize, Guid AlbumId, Guid UserId)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest(PAGINATION_ERROR);
            }
            return Ok(await _musicRepository.GetAlbumMusic(Page,PageSize, AlbumId, UserId));
        }
    }
}
