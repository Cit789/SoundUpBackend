using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundUp.Models;

namespace SoundUp.Configuration
{
    public class MusicAudioConfiguration : IEntityTypeConfiguration<MusicAudio>
    {
        public void Configure(EntityTypeBuilder<MusicAudio> builder)
        {
            builder.HasKey(m => m.Id);

            builder
                .HasOne(m => m.Music)
                .WithOne(ma => ma.MusicAudio)
                .HasForeignKey<MusicAudio>(x => x.MusicId);

        }

    }
}
