

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Domain.Entitys.Movies;

namespace MovieApp.Infrastructure.Persistence.Configurations
{
    public sealed class MovieCofiguration
        : IEntityTypeConfiguration<Movie>
    {
        public void Configure(
            EntityTypeBuilder<Movie> builder)
        {
           builder.HasKey(x => x.Id);

           builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

           builder.Property(x => x.YearOfRelease)
                 .IsRequired();

            builder.HasMany(x => x.Genres)
                .WithMany(x => x.Movies);

            builder.HasMany(x => x.Ratings)
                .WithOne()
                .HasForeignKey(x => x.MovieId);

            builder.HasQueryFilter(x=> !x.IsDelete);

        }
    }
}
