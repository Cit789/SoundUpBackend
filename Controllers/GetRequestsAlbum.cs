
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AlbumInfoById(Guid Id)
        {
            var Album = await _albumRepository.GetAlbumById(Id);
            if(Album  == null) return NotFound("Альбом не найден");
            return Ok(new AlbumDto(Album.Id,Album.AuthorId,Album.Name,Album.Description,Album.CreatedAt,Album.UpdatedAt));
            
        }
    }
}
