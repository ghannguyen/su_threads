using Archive.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Archive.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Topic> Topics => Set<Topic>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<PostImage> PostImages => Set<PostImage>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Like> Likes => Set<Like>();
    public DbSet<Follow> Follows => Set<Follow>();
    public DbSet<Repost> Reposts => Set<Repost>();
    public DbSet<Hashtag> Hashtags => Set<Hashtag>();
    public DbSet<PostHashtag> PostHashtags => Set<PostHashtag>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<AdminLog> AdminLogs => Set<AdminLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>()
            .HasIndex(x => x.Name)
            .IsUnique();

        modelBuilder.Entity<AppUser>()
            .HasIndex(x => x.UserName)
            .IsUnique();

        modelBuilder.Entity<AppUser>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Topic>()
            .HasIndex(x => x.Slug)
            .IsUnique();

        modelBuilder.Entity<Hashtag>()
            .HasIndex(x => x.Slug)
            .IsUnique();

        modelBuilder.Entity<PostHashtag>()
            .HasKey(x => new { x.PostId, x.HashtagId });

        modelBuilder.Entity<Like>()
            .HasIndex(x => new { x.PostId, x.UserId })
            .IsUnique();

        modelBuilder.Entity<Follow>()
            .HasIndex(x => new { x.FollowerId, x.FollowingId })
            .IsUnique();

        modelBuilder.Entity<Repost>()
            .HasIndex(x => new { x.PostId, x.UserId })
            .IsUnique();

        modelBuilder.Entity<Follow>()
            .HasOne(x => x.Follower)
            .WithMany(x => x.Following)
            .HasForeignKey(x => x.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Follow>()
            .HasOne(x => x.Following)
            .WithMany(x => x.Followers)
            .HasForeignKey(x => x.FollowingId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Post>()
            .HasOne(x => x.QuotePost)
            .WithMany(x => x.QuotedByPosts)
            .HasForeignKey(x => x.QuotePostId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(x => x.ParentComment)
            .WithMany(x => x.Replies)
            .HasForeignKey(x => x.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Report>()
            .HasOne(x => x.ReporterUser)
            .WithMany(x => x.CreatedReports)
            .HasForeignKey(x => x.ReporterUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Report>()
            .HasOne(x => x.TargetUser)
            .WithMany(x => x.TargetedReports)
            .HasForeignKey(x => x.TargetUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Report>()
            .HasOne(x => x.ReviewedByUser)
            .WithMany(x => x.ReviewedReports)
            .HasForeignKey(x => x.ReviewedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AdminLog>()
            .HasOne(x => x.AdminUser)
            .WithMany(x => x.AdminLogs)
            .HasForeignKey(x => x.AdminUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
