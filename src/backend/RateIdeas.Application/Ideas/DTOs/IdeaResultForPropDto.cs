namespace RateIdeas.Application.Ideas.DTOs;

public class IdeaResultForPropDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AssetResultDto Image { get; set; } = default!;
    public CategoryResultForPropDto Category { get; set; } = default!;

    public ICollection<CommentResultDto> Comments { get; set; } = default!;
    public ICollection<IdeaVoteResultDto> Votes { get; set; } = default!;
}