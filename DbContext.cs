using Microsoft.EntityFrameworkCore;
using SoundUp.Models;
using SoundUp.Configuration;


namespace SoundUp
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : DbContext(dbContext)
    {
        
        public DbSet<BaseUser> AllUsers { get; set; } = null!;
        public DbSet<Music> Music { get; set; } = null!;
        public DbSet<MusicAudio> MusicAudio { get; set; } = null!;
        public DbSet<Album> Albums { get; set; } = null!;
        public DbSet<Playlist> PlayLists { get; set; } = null!;
        public DbSet<UserAuthor> UserAuthors { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ListenHistory> ListenHistories { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new MusicConfiguration());
            modelBuilder.ApplyConfiguration(new BaseUserConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new MusicAudioConfiguration());
            modelBuilder.ApplyConfiguration(new ListenHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
