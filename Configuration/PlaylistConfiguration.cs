using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundUpRes.Models;

namespace SoundUpRes.Configuration
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
