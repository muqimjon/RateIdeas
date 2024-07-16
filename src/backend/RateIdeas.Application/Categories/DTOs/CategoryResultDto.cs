namespace RateIdeas.Application.Categories.DTOs;

public class CategoryResultDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public AssetResultDto Image { get; set; } = default!;

    public ICollection<IdeaResultDto> Ideas { get; set; } = default!;
}