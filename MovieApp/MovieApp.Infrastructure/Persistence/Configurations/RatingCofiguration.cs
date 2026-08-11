
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Domain.Entitys.Ratings;

namespace MovieApp.Infrastructure.Persistence.Configurations
{
    public sealed class RatingConfiguration
        : IEntityTypeConfiguration<Rating>
    {
        public void Configure(
            EntityTypeBuilder<Rating> builder)
        {
           builder.HasKey(x => x.Id);

            builder.Property(x => x.Score)
                .IsRequired();

            builder.Property(x=> x.UserId)
                .IsRequired();

            builder.Property(x=> x.MovieId)
                .IsRequired();
                
        }
    }
}
