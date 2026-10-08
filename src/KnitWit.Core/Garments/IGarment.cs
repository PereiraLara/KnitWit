using KnitWit.Core.Domain;

namespace KnitWit.Core.Garments;

/// <summary>A garment is just an ordered list of sections; each section picks a construction.</summary>
public interface IGarment
{
    GarmentType Type { get; }
    string DisplayName { get; }
    IReadOnlyList<SectionDefinition> Sections { get; }
}

public sealed record SectionDefinition(
    string Id,
    string DisplayName,
    string DefaultConstructionId
    );
