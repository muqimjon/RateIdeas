using RateIdeas.Domain.Common;

namespace RateIdeas.Domain.Entities;

public class Asset : Auditable
{
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}