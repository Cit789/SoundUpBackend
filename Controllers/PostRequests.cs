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
        public async Task<IActionResult> PostAlbum([FromBody] CreateAlbumRequest RequestAlbum)
        {
            var AuthorAlbum = await _dbcontext.UserAuthors
                .AsNoTracking()
                .Include(u => u.Albums)
                .FirstOrDefaultAsync(u => u.Id == RequestAlbum.AuthorId);

            if (AuthorAlbum == null)
            {
                return NotFound("Автор не найден");
            }
            if (AuthorAlbum.Albums.Any(a => a.Name == RequestAlbum.Name))
            {
                return Conflict("У автора уже есть альбом с этим именем");
            };

            var NewAlbum = new Album()
            {
                Id = Guid.NewGuid(),
                Name = RequestAlbum.Name,
                Avatar = RequestAlbum.Avatar,
                Description = RequestAlbum.Description,
                AuthorId = RequestAlbum.AuthorId,

            };
            await _dbcontext.Albums.AddAsync(NewAlbum);
            var Count = await _dbcontext.SaveChangesAsync();
            return Count > 0 ? StatusCode(500, "Ошибка в сохранении данных") : Ok();
        }

        [HttpPost("/PostMusic")]
        public async Task<IActionResult> PostMusic([FromBody] CreateMusicRequest RequestMusic)
        {
            var AuthorAlbum = await _dbcontext.UserAuthors
               .AsNoTracking()
               .Include(u => u.CreatedMusics)
               .FirstOrDefaultAsync(u => u.Id == RequestMusic.AuthorId);

            if (AuthorAlbum == null)
            {
                return NotFound("Автор не найден");
            }
            if (AuthorAlbum.Albums.Any(a => a.Name == RequestMusic.Name))
            {
                return Conflict("У автора уже есть музыка с этим именем");
            };
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
            var Count = await _dbcontext.SaveChangesAsync();
            return Count > 0 ? StatusCode(500, "Ошибка в сохранении данных") : Ok();
        }

        [HttpPost("/PostAuthor")]
        public async Task<IActionResult> PostAuthor([FromBody] CreateAuthorOrUserRequest createAuthorRequest)
        {
            if (_dbcontext.AllUsers.Any(u => u.Name == createAuthorRequest.Name))
            {
                return Conflict("Уже существует пользователь с данным именем");
            }
            var AuthorId = Guid.NewGuid();
            var ListenHistoryId = Guid.NewGuid();
            var ListenHistory = new ListenHistory()
            {
                Id = ListenHistoryId,
                UserId = AuthorId
            };
            var NewAuthor = new UserAuthor()
            {
                Name = createAuthorRequest.Name,
                Password = createAuthorRequest.Password,
                Avatar = createAuthorRequest.Avatar,
                Id = AuthorId,
                ListenHistory = ListenHistory,
                ListenHistoryId = ListenHistoryId,

            };

            await _dbcontext.UserAuthors.AddAsync(NewAuthor);
            var Count = await _dbcontext.SaveChangesAsync();
            return Count > 0 ? StatusCode(500, "Ошибка в сохранении данных") : Ok();
        }

        [HttpPost("/PostDefaultUser")]
        public async Task<IActionResult> PostDefaultUser([FromBody] CreateAuthorOrUserRequest createUserRequest)
        {

            if (_dbcontext.AllUsers.Any(u => u.Name == createUserRequest.Name))
            {
                return Conflict("Уже существует пользователь с данным именем");
            }
            var UserId = Guid.NewGuid();
            var ListenHistoryId = Guid.NewGuid();
            var ListenHistory = new ListenHistory()
            {
                Id = ListenHistoryId,
                UserId = UserId
            };
            var NewUser = new User()
            {
                Name = createUserRequest.Name,
                Password = createUserRequest.Password,
                Avatar = createUserRequest.Avatar,
                Id = UserId,
                ListenHistory = ListenHistory,
                ListenHistoryId = ListenHistoryId

            };

            await _dbcontext.Users.AddAsync(NewUser);
            var Count = await _dbcontext.SaveChangesAsync();
            return Count > 0 ? StatusCode(500, "Ошибка в сохранении данных") : Ok();
        }

        [HttpPost("/PostPlaylist")]
        public async Task<IActionResult> PostPlaylist([FromBody] CreatePlaylistRequest createPlaylistRequest)
        {

            var Creator = _dbcontext.AllUsers
                .FirstOrDefaultAsync(u => u.Id == createPlaylistRequest.CreatorId);

            if (Creator == null)
            {
                return NotFound("Пользователь не найден");
            }
            var playlist = new Playlist()
            {
                Id = Guid.NewGuid(),
                Name = createPlaylistRequest.Name,
                CreatorId = createPlaylistRequest.CreatorId,
                Avatar = createPlaylistRequest.Avatar
            };


            await _dbcontext.PlayLists.AddAsync(playlist);
            var Count = await _dbcontext.SaveChangesAsync();
            return Count > 0 ? StatusCode(500, "Ошибка в сохранении данных") : Ok();
        }

    }

}
