using Microsoft.EntityFrameworkCore;
using ShortUrl.Entities;

namespace ShortUrl.Repository
{
    public class ShortUrlContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }

        public ShortUrlContext(DbContextOptions<ShortUrlContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>().ToTable("urls");

            modelBuilder.Entity<Url>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("url_id");
                entity.Property(e => e.OriginalUrl).HasColumnName("original_url");
                entity.Property(e => e.ShortUrl).HasColumnName("short_url");
                entity.Property(e => e.Clicks).HasColumnName("clicks");
                entity.Property(e => e.LastClick).HasColumnName("last_click");
                entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            });
        }
    }
}