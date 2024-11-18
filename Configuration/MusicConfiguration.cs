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


            builder.HasOne(m => m.Author)
                .WithMany(a => a.CreatedMusics)
                .HasForeignKey(m => m.AuthorId);

            builder
                .HasMany(m => m.MusicInPlaylists)
                .WithMany(m => m.MusicList);

            builder
                .HasOne(m => m.Album)
                .WithMany(a => a.AlbumMusic)
                .HasForeignKey(m => m.AlbumId);

            builder
                .HasOne(m => m.MusicAudio)
                .WithOne(ma => ma.Music)
                .HasForeignKey<Music>(x => x.MusicAudioId);
                

        }

    }
}
