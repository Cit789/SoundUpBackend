using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundUp.Models;

namespace SoundUp.Configuration
{
    public class MusicConfiguration : IEntityTypeConfiguration<Music>
    {
        public void Configure(EntityTypeBuilder<Music> builder)
        {
            builder.HasKey(m => m.Id);

            builder
                .HasIndex(m => new { m.AuthorId ,m.Name})
                .IsUnique();

            builder.HasOne(m => m.Author)
                .WithMany(a => a.CreatedMusics)
                .HasForeignKey(m => m.AuthorId);

            builder
                .HasMany(m => m.MusicInPlaylists)
                .WithOne(m => m.Music)
                .HasForeignKey(pm => pm.MusicId);

            builder
                .HasOne(m => m.Album)
                .WithMany(a => a.AlbumMusic)
                .HasForeignKey(m => m.AlbumId);


            builder.HasMany(u => u.WhoFavorited)
                  .WithOne(um => um.Music)
                  .HasForeignKey(u => u.UserId);

            builder
                .HasOne(m => m.MusicAudio)
                .WithOne(ma => ma.Music)
                .HasForeignKey<Music>(x => x.MusicAudioId);
                

        }
    }
}
