using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.Dto;
using SoundUp.Models;

namespace SoundUp.Controllers
{
    [Controller]
    public class GetRequests(ApplicationDbContext dbContext) : ControllerBase
    {
       
        private readonly ApplicationDbContext _dbcontext = dbContext;
        
        [HttpGet("/GetAllMusic")]
        public async Task<IActionResult> Musics(int Page, int PageSize)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest("Номер страницы или ее размер не может быть отрицательным,(Отсчет страниц начинается с 1)");
            }
            var Music = await _dbcontext.Music
                 .AsNoTracking()
                 .Include(m => m.Author)
                 .Include(m => m.Album)
                 .Skip((Page - 1) * PageSize)
                 .Take(PageSize)
                  .Select(m => new MusicDto(m.Id, m.AuthorId, m.MusicAudioId, m.AlbumId, m.Album!.Name, m.Name, m.Avatar, m.Category, m.CreatedAt, m.UpdatedAt,new List<string> { m.Author!.Name }))
                 .ToListAsync();

            return Music.Count == 0 ? NotFound("Музыка не найдена") : Ok(Music);
        }

        [HttpGet("/GetCreatedAuthorMusic")]
        public async Task<IActionResult> GetCreatedAuthorMusic(int Page, int PageSize, Guid AuthorId)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest("Номер страницы или ее размер не может быть отрицательным,(Отсчет страниц начинается с 1)");
            }
            var author = await _dbcontext.UserAuthors
            .AsNoTracking()
            .Include(a => a.CreatedMusics)
            .Include(a => a.Albums)
            .FirstOrDefaultAsync(x => x.Id == AuthorId);

            if (author == null)
                return NotFound("Автор не найден");

            return Ok(author.CreatedMusics
                .Select(m => new MusicDto(m.Id, m.AuthorId, m.MusicAudioId, m.AlbumId,m.Album!.Name, m.Name, m.Avatar, m.Category, m.CreatedAt, m.UpdatedAt, [author.Name]))
                 .Skip((Page - 1) * PageSize)
                 .Take(PageSize)
                .ToList());
        }


        [HttpGet("/GetPlaylistMusic")]
        public async Task<IActionResult> GetPlayListMusic(int Page, int PageSize, Guid PlaylistId)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest("Номер страницы или ее размер не может быть отрицательным,(Отсчет страниц начинается с 1)");
            }
            var PlayList = await _dbcontext.PlayLists
                .AsNoTracking()
                .Include(p => p.MusicList)
                .Include(p => p.Creator)
                .FirstOrDefaultAsync(p => p.Id == PlaylistId);


            if (PlayList == null)
                return NotFound("Плейлист не найден");

            return Ok(PlayList.MusicList
                .Select(m => new MusicDto(m.Id, m.AuthorId, m.MusicAudioId,m.AlbumId,m.Album!.Name, m.Name, m.Avatar, m.Category, m.CreatedAt, m.UpdatedAt,[m.Author!.Name]))
                 .Skip((Page - 1) * PageSize)
                 .Take(PageSize)
                .ToList());
        }


        [HttpGet("/GetUser")]
        public async Task<IActionResult> GetUser(Guid UserId)
        {
            var FindedUser = await _dbcontext.AllUsers
                 .AsNoTracking()
                 .FirstOrDefaultAsync(u => u.Id == UserId);

            string UserType = FindedUser is UserAuthor ? "UserAuthor" : "User";

            if (FindedUser != null)
            {
                return Ok(new UserDto(FindedUser.Id, FindedUser.Name, FindedUser.Password, UserType, FindedUser.Avatar, FindedUser.CreatedAt, FindedUser.UpdatedAt));
            }
            return NotFound("Пользователь не найден");
        }

        
    }
}
