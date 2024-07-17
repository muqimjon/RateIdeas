namespace RateIdeas.Application.SavedIdeas.DTOs;

public class SavedIdeaResultDto
{
    public long Id { get; set; }
    public UserResultDto User { get; set; } = default!;
    public IdeaResultDto Idea { get; set; } = default!;
}