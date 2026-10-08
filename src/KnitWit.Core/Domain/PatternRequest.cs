namespace KnitWit.Core.Domain;

public sealed record PatternRequest
{
    public required GarmentType GarmentType { get; init; }
    public required Gauge Gauge { get; init; }
    public required Measurements Measurements { get; init; }

    /// <summary>sectionId -> constructionId. Sections not listed use the garment's default construction.</summary>
    public IReadOnlyDictionary<string, string> ConstructionChoices { get; init; } =
        new Dictionary<string, string>();
}
