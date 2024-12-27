using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundUp.Models;

namespace SoundUp.Configuration
{
    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasOne(x => x.Creator)
                .WithMany()
                .HasForeignKey(x => x.CreatorId);

            builder
                .HasMany(x => x.MusicList)
                .WithOne(x => x.Playlist)
                .HasForeignKey(pm => pm.PlaylistId);

            builder
                .HasMany(p => p.Users)
                .WithMany(x => x.Playlists)
                .UsingEntity<UserPlaylists>(
                a => a.HasOne<BaseUser>().WithMany().HasForeignKey(a => a.UserId),
                l => l.HasOne<Playlist>().WithMany().HasForeignKey(a => a.PlaylistId)
                );
        }

    }
}
