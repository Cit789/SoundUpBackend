using Microsoft.EntityFrameworkCore;
using SoundUp.Models;
using SoundUp.Configuration;


namespace SoundUp
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContext) : DbContext(dbContext)
    {
        
        public DbSet<BaseUser> AllUsers { get; set; }
        public DbSet<Music> Music { get; set; }
        public DbSet<MusicAudio> MusicAudio { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Playlist> PlayLists { get; set; }
        public DbSet<UserAuthor> UserAuthors { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new MusicConfiguration());
            modelBuilder.ApplyConfiguration(new BaseUserConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new PlaylistConfiguration());
            modelBuilder.ApplyConfiguration(new MusicAudioConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
