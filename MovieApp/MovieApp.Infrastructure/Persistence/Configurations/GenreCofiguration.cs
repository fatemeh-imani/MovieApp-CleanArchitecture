using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Domain.Entitys.Genres;

namespace MovieApp.Infrastructure.Persistence.Configurations
{
    public sealed class GenreConfiguration
        : IEntityTypeConfiguration<Genre>
    {
        public void Configure(
            EntityTypeBuilder<Genre> builder)
        {
           builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);    
        }
    }
}
