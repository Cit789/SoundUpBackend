using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundUpRes.Models;

namespace SoundUpRes.Configuration
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.HasKey(x => x.Id);


            builder
                .HasOne(al => al.Author)
                .WithMany(a => a.Albums)
                .HasForeignKey(al => al.AuthorId);

            builder.
               HasMany(al => al.AlbumMusic)
               .WithOne(m => m.Album)
               .HasForeignKey(m => m.AlbumId);

        }

    }
}
