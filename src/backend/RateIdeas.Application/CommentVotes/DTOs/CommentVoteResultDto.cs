﻿namespace RateIdeas.Application.CommentVotes.DTOs;

public class CommentVoteResultDto
{
    public long Id { get; set; }
    public bool IsUpvote { get; set; }
    public UserResultDto User { get; set; } = default!;
    public IdeaResultForPropDto Idea { get; set; } = default!;
}