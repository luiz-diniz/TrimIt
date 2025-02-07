using Microsoft.EntityFrameworkCore;
using ShortUrl.Entities;

namespace ShortUrl.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<UrlEntity> Urls { get; set; }
        public DbSet<UserEntity> User { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrlEntity>().ToTable("urls");

            modelBuilder.Entity<UrlEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdUser).HasColumnName("id_user");
                entity.Property(e => e.OriginalUrl).HasColumnName("original_url");
                entity.Property(e => e.ShortUrl).HasColumnName("short_url");
                entity.Property(e => e.Clicks).HasColumnName("clicks");
                entity.Property(e => e.LastClick).HasColumnName("last_click");
                entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Urls)
                      .HasForeignKey(e => e.IdUser)  
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<UserEntity>().ToTable("users");

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.Role).HasColumnName("role");
            });
        }
    }
}