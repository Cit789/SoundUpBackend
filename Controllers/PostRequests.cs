using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.Contracts;
using SoundUp.Models;

namespace SoundUp.Controllers
{
    [Controller]
    public class PostRequests(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext _dbcontext = dbContext;

        [HttpPost("/PostAlbum")]
        public async Task PostAlbum([FromBody] CreateAlbumRequest RequestAlbum)
        {
            var NewAlbum = new Album()
            {
                Id = Guid.NewGuid(),
                Name = RequestAlbum.Name,
                Avatar = RequestAlbum.Avatar,
                Description = RequestAlbum.Description,
                AuthorId = RequestAlbum.AuthorId,

            };
            await _dbcontext.Albums.AddAsync(NewAlbum);
            await _dbcontext.SaveChangesAsync();
        }


        [HttpPost("/PostMusic")]
        public async Task PostMusic([FromBody] CreateMusicRequest RequestMusic)
        {
            Guid MusicId = Guid.NewGuid();
            Guid MusicAudioId = Guid.NewGuid();
            var NewMusic = new Music()
            {
                Id = MusicId,
                AuthorId = RequestMusic.AuthorId,
                AlbumId = RequestMusic.AlbumId,
                Name = RequestMusic.Name,
                Avatar = RequestMusic.Avatar,
                Category = RequestMusic.Category,
                MusicAudioId = MusicAudioId,
                MusicAudio = new MusicAudio()
                {
                    Id = MusicAudioId,
                    MusicId = MusicId,
                    Audio = RequestMusic.Audio
                }
            };

            

            await _dbcontext.Music.AddAsync(NewMusic);
            await _dbcontext.SaveChangesAsync();
        }


        [HttpPost("/PostAuthor")]
        public async Task PostAuthor([FromBody] CreateAuthorRequest createAuthorRequest)
        {
            var AuthorId = Guid.NewGuid();
            var ListenHistoryForAuthor = new ListenHistory()
            {
                Id = Guid.NewGuid(),
                UserId = AuthorId
            };
            var NewAuthor = new UserAuthor()
            {
                Name = createAuthorRequest.Name,
                Password = createAuthorRequest.Password,
                Avatar = createAuthorRequest.Avatar,
                Id = AuthorId,
                ListenHistory = ListenHistoryForAuthor

            };

            await _dbcontext.UserAuthors.AddAsync(NewAuthor);
            await _dbcontext.SaveChangesAsync();
        }
    }

}
