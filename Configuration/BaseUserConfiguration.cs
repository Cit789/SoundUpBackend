using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundUpRes.Models;

namespace SoundUpRes.Configuration
{

    public class BaseUserConfiguration : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.HasKey(b => b.Id);
            builder
                .HasMany(b => b.Favorites)
                .WithMany(m => m.WhoFavorited);

            builder
                .HasMany(b => b.Favorites)
                .WithMany(m => m.WhoFavorited);

            builder
                .HasDiscriminator<string>("UserType")
                .HasValue<User>("DefaultUser")
                .HasValue<UserAuthor>("AuthorForMusic");



        }
    }
}
