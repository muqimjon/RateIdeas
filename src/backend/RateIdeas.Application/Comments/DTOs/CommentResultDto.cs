namespace RateIdeas.Application.Comments.DTOs;

public class CommentResultDto
{
    public long Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public IdeaResultDto Idea { get; set; } = default!;
    public UserResultDto User { get; set; } = default!;

    public ICollection<CommentVoteResultDto> Votes { get; set; } = default!;
}