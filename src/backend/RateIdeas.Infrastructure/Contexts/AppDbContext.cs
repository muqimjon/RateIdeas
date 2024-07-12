using RateIdeas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RateIdeas.Domain.Entities.Ideas;
using RateIdeas.Domain.Entities.Comments;

namespace RateIdeas.Infrastructure.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) 
{
    public DbSet<User> Users { get; set; }
    public DbSet<Idea> Ideas { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<IdeaVote> IdeaVotes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SavedIdea> SavedIdeas { get; set; }
    public DbSet<CommentVote> CommentVotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // CommentVote
        modelBuilder.Entity<CommentVote>()
            .HasOne(cv => cv.User)
            .WithMany(u => u.CommentVotes)
            .HasForeignKey(cv => cv.UserId);

        modelBuilder.Entity<CommentVote>()
            .HasOne(cv => cv.Idea)
            .WithMany(c => c.Votes)
            .HasForeignKey(cv => cv.IdeaId);

        // Comment
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Idea)
            .WithMany(i => i.Comments)
            .HasForeignKey(c => c.IdeaId);

        // Idea
        modelBuilder.Entity<Idea>()
            .HasOne(i => i.User)
            .WithMany(u => u.Users)
            .HasForeignKey(i => i.UserId);

        modelBuilder.Entity<Idea>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Ideas)
            .HasForeignKey(i => i.CategoryId);

        modelBuilder.Entity<Idea>()
            .HasOne(i => i.Image)
            .WithMany()
            .HasForeignKey(i => i.ImageId);

        // IdeaVote
        modelBuilder.Entity<IdeaVote>()
            .HasOne(iv => iv.User)
            .WithMany(u => u.Votes)
            .HasForeignKey(iv => iv.UserId);

        modelBuilder.Entity<IdeaVote>()
            .HasOne(iv => iv.Idea)
            .WithMany(i => i.Votes)
            .HasForeignKey(iv => iv.IdeaId);

        // SavedIdea
        modelBuilder.Entity<SavedIdea>()
            .HasOne(si => si.User)
            .WithMany(u => u.SavedIdeas)
            .HasForeignKey(si => si.UserId);

        modelBuilder.Entity<SavedIdea>()
            .HasOne(si => si.Idea)
            .WithMany()
            .HasForeignKey(si => si.IdeaId);

        // Category
        modelBuilder.Entity<Category>()
            .HasOne(c => c.Image)
            .WithMany()
            .HasForeignKey(c => c.ImageId);

        // User
        modelBuilder.Entity<User>()
            .HasOne(u => u.Image)
            .WithMany()
            .HasForeignKey(u => u.ImageId);
    }
}