using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundUp.Models;

namespace SoundUp.Configuration
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
                .HasOne(u => u.RefreshToken)
                .WithOne(t => t.User)
                .HasForeignKey<BaseUser>(t => t.RefreshTokenId);

            builder
                .HasMany(b => b.Favorites)
                .WithMany(m => m.WhoFavorited);

            builder
                .HasOne(b => b.ListenHistory)
                .WithOne(h => h.User)
                .HasForeignKey<BaseUser>(b => b.ListenHistoryId);

            builder
                .HasDiscriminator<string>("UserType")
                .HasValue<User>("DefaultUser")
                .HasValue<UserAuthor>("AuthorForMusic");



        }
    }
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(t => t.Id);

            builder
                .HasOne(t => t.User)
                .WithOne(u => u.RefreshToken)
                .HasForeignKey<RefreshToken>(t => t.UserId);
        }
    }
}
