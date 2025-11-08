using Microsoft.EntityFrameworkCore;
using NewsAI.Core.Entities;

namespace NewsAI.Data.Context;

public class NewsDbContext(DbContextOptions<NewsDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SavedNews> SavedNews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.EmailAddress).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Password).HasMaxLength(50).IsRequired();
            entity.Property(e => e.CardNumber).HasMaxLength(15).IsRequired();
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.DateOfBirth).IsRequired();
            entity.Property(e => e.IsActive).HasDefaultValue(true).IsRequired();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()").IsRequired();
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("now()")
                .IsRequired();
            
            entity.HasMany(e => e.SavedNews)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_saved_id_fkey");
            
            entity.ToTable("users");
        });

        //News
        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Title).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Url).IsRequired();
            entity.Property(e => e.Views);
            entity.Property(e => e.HotNews).HasDefaultValue(false).IsRequired();
            entity.Property(e => e.CategoryId);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()").IsRequired();
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("now()")
                .IsRequired();

            entity.HasOne(e => e.Category)
                .WithMany(e => e.News)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("news_category_id_fkey");

            entity.HasMany(e => e.SavedByUser)
                .WithOne(e => e.News)
                .HasForeignKey(e => e.NewsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("news_saved_id_fkey");

            entity.HasMany(e => e.NewsImages)
                .WithOne(e => e.News)
                .HasForeignKey(e => e.NewsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("news_images_id_fkey");
        });

        //Category
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(100).IsRequired();
            entity.Property(e => e.IsActive).HasDefaultValue(true).IsRequired();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()").IsRequired();
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("now()")
                .IsRequired();

            entity.HasMany(e => e.News)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("category_news_id_fkey");

            entity.ToTable("categories");
        });

        //SavedNews
        modelBuilder.Entity<SavedNews>(entity =>
        {
            entity.HasKey(e => new {e.NewsId, e.UserId});
            
            entity.Property(e => e.SavedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("now()")
                .IsRequired();
        
            entity.HasOne(e => e.News)
                .WithMany(e => e.SavedByUser)
                .HasForeignKey(e => e.NewsId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("saved_news_id_fkey");

            entity.HasOne(e => e.User)
                .WithMany(e => e.SavedNews)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("saved_news_user_id_fkey");
            
            entity.ToTable("savedNews");
        });

        //NewsImages
        modelBuilder.Entity<NewsImages>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.Url).IsRequired();
            entity.Property(e => e.IsPrimary).HasDefaultValue(false).IsRequired();
            entity.Property(e => e.NewsId).IsRequired();

            entity.HasOne(e => e.News)
            .WithMany(e => e.NewsImages)
            .HasForeignKey(e => e.NewsId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("news_images_news_id_fkey");
            
            entity.ToTable("newsImages");
        });
    }
}