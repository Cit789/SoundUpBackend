using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoundUp.Models;

namespace SoundUp.Configuration
{
    public class UserMusicFavoritesConfiguration : IEntityTypeConfiguration<UserMusicFavorites>
    {
        public void Configure(EntityTypeBuilder<UserMusicFavorites> builder)
        {


            builder.HasKey(um => new { um.UserId, um.MusicId }); 

            builder.HasOne(um => um.User)
                  .WithMany(u => u.Favorites)
                  .HasForeignKey(um => um.UserId);

            builder.HasOne(um => um.Music)
                  .WithMany(m => m.WhoFavorited)
                  .HasForeignKey(um => um.MusicId);

            
            builder.HasIndex(um => new { um.UserId, um.MusicId })
                  .IsUnique()
                  .HasDatabaseName("IX_UserMusicFavorite_UserId_MusicId");
        }

    }
}
