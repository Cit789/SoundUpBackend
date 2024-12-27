using Microsoft.EntityFrameworkCore;
using SoundUp.Contracts;
using SoundUp.Dto;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;

namespace SoundUp.Repositories
{
    public class PlaylistRepository(ApplicationDbContext dbcontext, IUserRepository userRepository) : IPlaylistRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        private readonly IUserRepository _userRepository = userRepository;

        static public PlaylistDto ToPlaylistDto(Playlist playlist)
        {
            return new PlaylistDto(playlist.Id,playlist.Avatar,playlist.Name,playlist.CreatorId,playlist.CreatedAt,playlist.UpdatedAt);
        }
        public async Task<Playlist?> CreatePlaylist(CreatePlaylistRequest createPlaylistRequest)
        {
            var User = await _userRepository.GetUserById(createPlaylistRequest.CreatorId);
            if (User is null) return null;

            var playlist = new Playlist()
            {
                Id = Guid.NewGuid(),
                Name = createPlaylistRequest.Name,
                CreatorId = createPlaylistRequest.CreatorId,
                Avatar = createPlaylistRequest.Avatar
            };
            await _dbcontext.PlayLists.AddAsync(playlist);
            User.Playlists.Add(playlist);
            var Count = await _dbcontext.SaveChangesAsync();
            return Count != 0 ? playlist : null;
        }

        public async Task<Playlist?> GetPlayListById(Guid PlaylistId)
        {
            return await _dbcontext.PlayLists
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == PlaylistId);
        }

        public async Task<List<PlaylistDto>> GetUserPlaylists(int Page,int PageSize,Guid UserId)
        {
           var User = await _dbcontext.AllUsers
                .AsNoTracking()
                .Include(u => u.Playlists)
                .FirstOrDefaultAsync(u => u.Id == UserId);

            if (User == null) return [];

           return User.Playlists
                .Select(p => ToPlaylistDto(p))
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
        
    }
}