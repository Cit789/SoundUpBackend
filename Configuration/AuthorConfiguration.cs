using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundUpRes.Models;

namespace SoundUpRes.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<UserAuthor>
    {
        public void Configure(EntityTypeBuilder<UserAuthor> builder)
        {


            builder
                .HasMany(a => a.CreatedMusics)
                .WithOne(m => m.Author)
                .HasForeignKey(m => m.AuthorId);

            builder
                .HasMany(m => m.Albums)
                .WithOne(al => al.Author)
                .HasForeignKey(al => al.AuthorId);

        }
    }
}
