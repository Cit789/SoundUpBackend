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
            if(Page <= 0 || PageSize < 0)
            {
                return BadRequest("Номер страницы или ее размер не может быть отрицательным,(Отсчет страниц начинается с 1)");
            }
            var Music = await _dbcontext.Music
                 .AsNoTracking()
                 .Skip((Page - 1) * PageSize)
                 .Take(PageSize)
                 .Select(m => new MusicDto(m.Id, m.AuthorId, m.MusicAudioId, m.Name, m.Avatar, m.Category, m.CreatedAt, m.UpdatedAt))
                 .ToListAsync();

            return Music.Count == 0 ? NotFound("Музыка не найдена") : Ok(Music);
        }

        [HttpGet("/GetAlbumMusic")]
        public async Task<IActionResult> GetAlbumMusic(int Page, int PageSize, Guid AuthorId)
        {
            if (Page <= 0 || PageSize < 0)
            {
                return BadRequest("Номер страницы или ее размер не может быть отрицательным,(Отсчет страниц начинается с 1)");
            }
            var author = await _dbcontext.UserAuthors
            .AsNoTracking()
            .Include(a => a.CreatedMusics)
            .FirstOrDefaultAsync(x => x.Id == AuthorId);

            if (author == null)
                return NotFound("Автор не найден");

            return Ok(author.CreatedMusics
                .Select(m => new MusicDto(m.Id, m.AuthorId, m.MusicAudioId, m.Name, m.Avatar, m.Category, m.CreatedAt, m.UpdatedAt))
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
                .FirstOrDefaultAsync(p => p.Id == PlaylistId);


            if (PlayList == null)
                return NotFound("Плейлист не найден");

            return Ok(PlayList.MusicList
                .Select(m => new MusicDto(m.Id, m.AuthorId, m.MusicAudioId, m.Name, m.Avatar, m.Category, m.CreatedAt, m.UpdatedAt))
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
