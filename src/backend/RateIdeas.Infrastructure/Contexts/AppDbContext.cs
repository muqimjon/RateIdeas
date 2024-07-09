using RateIdeas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using RateIdeas.Domain.Entities.Votes;

namespace RateIdeas.Infrastructure.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) 
{
    public DbSet<User> Users { get; set; }
    public DbSet<Idea> Ideas { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<IdeaVote> IdeaVotes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CommentVote> CommentVotes { get; set; }
}