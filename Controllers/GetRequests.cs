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

        [HttpGet("/GetMusic")]
        public async Task<List<Music>> Musics(int Page, int PageSize)
        {
           return await _dbcontext.Music
                .AsNoTracking()
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(); 
        }
        [HttpGet("/GetAlbumFromAuthor")]
        public async Task<List<MusicDto>> getalbum(Guid id)
        {
            var author = await _dbcontext.UserAuthors
            .Include(a => a.CreatedMusics)
            .SingleOrDefaultAsync(x => x.Id == id);

            if (author == null)
                return new List<MusicDto>();

            return author.CreatedMusics.Select(m => new MusicDto
            {
                Id = m.Id,
                Name = m.Name,
                Avatar = m.Avatar,
                Category = m.Category,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt,
                AudioId = m.MusicAudioId
            }).ToList();
        }
    }
}
