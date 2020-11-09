using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.DataAccess.Entities;

namespace Shop.DataAccess.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Comment).IsRequired();
            builder.Property(r => r.Value).IsRequired();
            builder.Property(r => r.UserId).IsRequired();
            builder.HasOne(r => r.User);
            builder.Property(r => r.ProductId).IsRequired();
            builder.HasOne(r => r.Product);
        }
    }
}