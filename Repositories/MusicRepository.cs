using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundUp.Contracts;
using SoundUp.Dto;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;


namespace SoundUp.Repositories
{
    public class MusicRepository(ApplicationDbContext dbcontext, IAuthorRepository authorRepository) : IMusicRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        private readonly IAuthorRepository _authorRepository = authorRepository;
        public static MusicDto ToMusicDto(Music m, Guid UserId)
        {
            return
                     new MusicDto(m.Id,
                     m.AuthorId,
                     m.MusicAudioId,
                     m.AlbumId,
                     m.Album!.Name,
                     m.Name,
                     m.Avatar,
                     m.Category,
                     m.WhoFavorited.Any(f => f.Id == UserId),
                     m.CreatedAt,
                     m.UpdatedAt,
                     [m.Author?.Name]);

        }

        public async Task<List<MusicDto>> GetAllMusicWithPagination(int Page, int PageSize, Guid UserId)
        {
            return await _dbcontext.Music
                 .AsNoTracking()
                 .Include(m => m.Author)
                 .Include(m => m.Album)
                 .Include(m => m.WhoFavorited)
                 .Skip((Page - 1) * PageSize)
                 .Take(PageSize)
                 .Select((m) => ToMusicDto(m, UserId))
                 .ToListAsync();
        }

        public async Task<Music?> GetMusicById(Guid MusicId)
        {
            return await _dbcontext.Music
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == MusicId);
        }
        public async Task<MusicDto?> GetMusicByIdInDto(Guid MusicId,Guid UserId)
        {
            var Music = await _dbcontext.Music
                .Include(m => m.Album)
                .Include (m => m.WhoFavorited)
                .Include(m => m.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == MusicId);
            if (Music == null) return null;
            return ToMusicDto(Music, UserId);
   
        }
        public async Task<string> GetMusicAudio(Guid MusicId) 
        {
            var Audio = await _dbcontext.MusicAudio.FirstOrDefaultAsync(a => a.MusicId == MusicId);
            return Audio == null ? string.Empty : Audio.Audio;
        }
        public async Task<List<MusicDto>> GetMusicInPlaylist(int Page, int PageSize, Guid PlaylistId, Guid UserId)
        {
            var PlayList = await _dbcontext.PlayLists
                .AsNoTracking()
                .Include(p => p.MusicList)
                .ThenInclude(m => m.WhoFavorited)
                .Include(p => p.MusicList)
                .ThenInclude(m => m.Album)
                .Include(p => p.MusicList)
                .ThenInclude(m => m.Author)
                .FirstOrDefaultAsync(p => p.Id == PlaylistId);


            if (PlayList == null) return [];

            return PlayList.MusicList
                 .Select((m) => ToMusicDto(m, UserId))
                 .Skip((Page - 1) * PageSize)
                 .Take(PageSize)
                .ToList();

        }

        public async Task<List<MusicDto>> GetCreatedAuthorMusic(int Page, int PageSize, Guid AuthorId, Guid UserId)
        {

            var Author = await _authorRepository.GetAuthorWithAlbumAndCreatedMusics(AuthorId);

            if (Author == null) return [];

            return Author
                .CreatedMusics
                .Select((m) => ToMusicDto(m, UserId))
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }

        public async Task<bool> CreateMusic(CreateMusicRequest RequestMusic)
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
            var Count = await _dbcontext.SaveChangesAsync();
            return Count != 0;
        }

        public async Task<List<MusicDto>> GetFavouriteMusicByUserId(int Page, int PageSize, Guid UserId)
        {
           var User = await _dbcontext.AllUsers
                .AsNoTracking()
                .Include(u => u.Favorites)
                .ThenInclude(m => m.Album)
                .Include(u => u.Favorites)
                .ThenInclude(m => m.Author)
                .FirstOrDefaultAsync(u => u.Id == UserId);
            if (User == null) return [];

           

            return User.Favorites
                .Select(m => ToMusicDto(m,UserId))
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }

        public async Task<List<MusicDto>> GetAlbumMusic(int Page,int PageSize,Guid AlbumId,Guid UserId)
        {
            var Album = await _dbcontext.Albums
                .Include(a => a.AlbumMusic)
                .ThenInclude(m => m.Author)
                .Include(a => a.AlbumMusic)
                .ThenInclude(m => m.WhoFavorited)
                .FirstOrDefaultAsync(a => a.Id == AlbumId);

            if (Album == null) return [];
            return Album
                .AlbumMusic
                .Select((m) => ToMusicDto(m, UserId))
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}
