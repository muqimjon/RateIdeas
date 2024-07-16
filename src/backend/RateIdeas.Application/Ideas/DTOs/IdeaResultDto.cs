namespace RateIdeas.Application.Ideas.DTOs;

public class IdeaResultDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AssetResultDto Image { get; set; } = default!;
    public CategoryResultDto Category { get; set; } = default!;
    public UserResultDto User { get; set; } = default!;

    public ICollection<CommentResultDto> Comments { get; set; } = default!;
    public ICollection<IdeaVoteResultDto> Votes { get; set; } = default!;
}