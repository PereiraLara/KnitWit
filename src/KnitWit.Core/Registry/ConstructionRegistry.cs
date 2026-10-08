using KnitWit.Core.Constructions;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Registry;

public sealed class ConstructionRegistry
{
    private readonly List<IConstruction> _constructions = [];

    public ConstructionRegistry Register(IConstruction construction)
    {
        _constructions.Add(construction);
        return this;
    }

    /// <summary>All constructions the user can pick for one section (handy for a UI dropdown).</summary>
    public IReadOnlyList<IConstruction> For(GarmentType garment, string sectionId) =>
        _constructions.Where(c => c.GarmentType == garment && c.SectionId == sectionId).ToList();

    public IConstruction Get(GarmentType garment, string sectionId, string constructionId) =>
        For(garment, sectionId).FirstOrDefault(c => c.Id == constructionId)
        ?? throw new InvalidOperationException(
            $"No construction '{constructionId}' for {garment}/{sectionId}. " +
            $"Available: {string.Join(", ", For(garment, sectionId).Select(c => c.Id))}");
}
