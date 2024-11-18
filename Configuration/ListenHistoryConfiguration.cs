using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoundUp.Models;

namespace SoundUp.Configuration
{
    public class ListenHistoryConfiguration : IEntityTypeConfiguration<ListenHistory>
    {
        public void Configure(EntityTypeBuilder<ListenHistory> builder)
        {
            builder.HasKey(b => b.Id);
            builder
                .HasOne(h => h.User)
                .WithOne(u => u.ListenHistory)
                .HasForeignKey<ListenHistory>(h => h.UserId);

            builder
                .HasMany(h => h.MusicHistory)
                .WithMany(m => m.ListenHistories);

        }
    }
}
