using RateIdeas.Domain.Common;
using RateIdeas.Domain.Entities.Ideas;

namespace RateIdeas.Domain.Entities;

public class Category : Auditable
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public long? ImageId { get; set; }
    public Asset Image { get; set; } = default!;

    public ICollection<Idea> Ideas { get; set; } = default!;
}