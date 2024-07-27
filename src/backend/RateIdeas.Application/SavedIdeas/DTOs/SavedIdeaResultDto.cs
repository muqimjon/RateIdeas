namespace RateIdeas.Application.SavedIdeas.DTOs;

public class SavedIdeaResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; } = default!;
    public IdeaResultForPropDto Idea { get; set; } = default!;
}