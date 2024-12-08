using Microsoft.EntityFrameworkCore;
using SoundUp.Contracts;
using SoundUp.Dto;
using SoundUp.Interfaces.Repository;
using SoundUp.Models;

namespace SoundUp.Repositories
{
    public class AlbumRepository(ApplicationDbContext dbcontext, IUserRepository usersRepository) : IAlbumRepository
    {
        private readonly ApplicationDbContext _dbcontext = dbcontext;
        private readonly IUserRepository _usersRepository = usersRepository;
        
        static public AlbumDto ToAlbumDto(Album album)
        {
            return new AlbumDto(album.Id,album.AuthorId,album.Name,album.Description,album.CreatedAt,album.UpdatedAt);
        }
        public async Task<Album?> PostNewAlbum(CreateAlbumRequest RequestAlbum)
        {
            var NewAlbum = new Album()
            {
                Id = Guid.NewGuid(),
                Name = RequestAlbum.AlbumName,
                Avatar = RequestAlbum.Avatar,
                Description = RequestAlbum.Description,
                AuthorId = RequestAlbum.AuthorId,

            };
            await _dbcontext.Albums.AddAsync(NewAlbum);
            var Count = await _dbcontext.SaveChangesAsync();

            return Count != 0 ? NewAlbum : null;
        }
        public async Task<Album?> GetAlbumById(Guid AlbumId)
        {
            return await _dbcontext.Albums
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == AlbumId);
        }
        public async Task<List<AlbumDto>> GetAllAlbums(int Page,int PageSize)
        {
            return await _dbcontext.Albums
                .AsNoTracking()
                .Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .Select(a => ToAlbumDto(a))
                .ToListAsync();
        }
        public async Task<List<AlbumDto>> GetAlbumsFromAuthor(int Page, int PageSize,Guid AuthorId)
        {
            var Author = await _dbcontext.UserAuthors
                .AsNoTracking()
                .Include(m  => m.Albums)
                .FirstOrDefaultAsync(u => u.Id == AuthorId);
            if (Author == null)
                return [];
            return Author.Albums.Select(a => ToAlbumDto(a)).ToList();
        }
    }
}