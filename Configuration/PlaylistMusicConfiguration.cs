using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundUp.Models;

namespace SoundUp.Configuration
{
    public class PlaylistMusicConfiguration : IEntityTypeConfiguration<PlaylistMusic>
    {
        public void Configure(EntityTypeBuilder<PlaylistMusic> builder)
        {


            builder.HasKey(um => new { um.PlaylistId, um.MusicId });

            builder.HasOne(um => um.Playlist)
                  .WithMany(u => u.MusicList)
                  .HasForeignKey(um => um.PlaylistId);

            builder.HasOne(um => um.Music)
                  .WithMany(m => m.MusicInPlaylists)
                  .HasForeignKey(um => um.MusicId);


            builder.HasIndex(um => new { um.PlaylistId, um.MusicId })
                  .IsUnique()
                  .HasDatabaseName("IX_PlaylistMusic_PlaylistId_MusicId");
        }

    }
}
