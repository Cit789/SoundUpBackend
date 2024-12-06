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
                .HasIndex(b => b.Name)
                .IsUnique();
           

            builder
                .HasOne(u => u.RefreshToken)
                .WithOne(t => t.User)
                .HasForeignKey<BaseUser>(t => t.RefreshTokenId);


            builder.HasMany(u => u.Favorites)
                    .WithOne(um => um.User)
                    .HasForeignKey(u => u.UserId);
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
}
