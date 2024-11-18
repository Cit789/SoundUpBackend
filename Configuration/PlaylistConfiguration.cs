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
                .WithMany(x => x.Playlists)
                .HasForeignKey(x => x.CreatorId);

            builder
                .HasMany(x => x.MusicList)
                .WithMany(x => x.MusicInPlaylists);

        }

    }
}
