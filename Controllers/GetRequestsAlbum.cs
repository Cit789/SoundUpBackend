using Microsoft.AspNetCore.Mvc;
using SoundUp.Dto;
using SoundUp.Interfaces.Repository;


namespace SoundUp.Controllers
{
    [Controller]
    [Route("api/[controller]/[action]")]
    public class GetRequestsAlbum(IAlbumRepository albumRepository) : ControllerBase
    {
        private readonly IAlbumRepository _albumRepository = albumRepository;
        private const string PAGINATION_ERROR = "Номер страницы или ее размер не может быть отрицательным,(Отсчет страниц начинается с 1)";
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> AlbumInfoById([FromHeader] Guid Id)
        {
            var Album = await _albumRepository.GetAlbumById(Id);
            if(Album  == null) return NotFound("Альбом не найден");
            return Ok(new AlbumDto(Album.Id,Album.AuthorId,Album.Name,Album.Description,Album.CreatedAt,Album.UpdatedAt));
            
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAlbums(int Page,int PageSize)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest(PAGINATION_ERROR);
            }
            var Albums = await _albumRepository.GetAllAlbums(Page, PageSize);
            return Albums.Count == 0 ? NotFound("Альбомы не найдены"): Ok(Albums);
        }
        [HttpGet]
        public async Task<IActionResult> GetAlbumFromAuthor(int Page, int PageSize, [FromHeader] Guid AuthorId)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest(PAGINATION_ERROR);
            }
            var Albums = await _albumRepository.GetAlbumsFromAuthor(Page, PageSize, AuthorId);
            return Albums.Count == 0 ? NotFound("Альбом не найден") : Ok(Albums) ;
        }
    }
    
}
