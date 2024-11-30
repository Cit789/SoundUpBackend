using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundUp.Models;

namespace SoundUp.Configuration
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
