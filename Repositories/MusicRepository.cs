using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using SoundUp.Dto;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;


namespace SoundUp.Repository
{
    public class MusicRepository(ApplicationDbContext dbcontext, IUserRepository usersRepository) : IMusicRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        private readonly IUserRepository _usersRepository = usersRepository;
        public static MusicDto ToMusicDto(Music m, Guid UserId)
        {
            var result =
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
            return result;
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
                     .Select((m) => ToMusicDto(m,UserId))
                     .ToListAsync();
        }

        public async Task<Music?> GetMusicById(Guid MusicId)
        {
            return await _dbcontext.Music.FirstOrDefaultAsync(m => m.Id == MusicId);
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
            
            var Author = await _usersRepository.GetAuthorWithAlbumAndCreatedMusics(AuthorId);
            
            if (Author == null) return [];

            return Author
                .CreatedMusics
                .Select((m) => ToMusicDto(m,UserId))
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}
