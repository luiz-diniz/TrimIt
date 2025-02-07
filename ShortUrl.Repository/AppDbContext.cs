using Microsoft.EntityFrameworkCore;
using ShortUrl.Entities;

namespace ShortUrl.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }
        public DbSet<User> User { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>().ToTable("urls");

            modelBuilder.Entity<Url>(entity =>
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

            modelBuilder.Entity<User>().ToTable("users");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Password).HasColumnName("password");
            });
        }
    }
}