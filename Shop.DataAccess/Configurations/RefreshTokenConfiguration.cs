using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.Token);
            builder.Property(r => r.Token).ValueGeneratedOnAdd();
            builder.HasOne(r => r.User);
        }
    }
}