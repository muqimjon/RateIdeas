namespace RateIdeas.Application.SavedIdeas.DTOs;

public class SavedIdeaResultDto
{
    public long Id { get; set; }
    public User User { get; set; } = default!;
    public Idea Idea { get; set; } = default!;
}