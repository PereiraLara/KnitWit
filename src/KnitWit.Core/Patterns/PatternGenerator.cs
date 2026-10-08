using KnitWit.Core.Constructions;
using KnitWit.Core.Domain;
using KnitWit.Core.Registry;

namespace KnitWit.Core.Patterns;

public sealed class PatternGenerator(GarmentRegistry garments, ConstructionRegistry constructions)
{
    public Pattern Generate(PatternRequest request)
    {
        var garment = garments.Get(request.GarmentType);
        var sections = new List<PatternSection>();
        int stitches = 0;
        int overlappingRounds = 0;

        foreach (var section in garment.Sections)
        {
            var constructionId = request.ConstructionChoices.TryGetValue(section.Id, out var chosen)
                ? chosen
                : section.DefaultConstructionId;

            var construction = constructions.Get(garment.Type, section.Id, constructionId);

            foreach (var key in construction.RequiredMeasurements)
                if (!request.Measurements.Has(key))
                    throw new ArgumentException($"'{construction.DisplayName}' needs measurement '{key}'.");

            var result = construction.Generate(
                new ConstructionContext(request.Gauge, request.Measurements, stitches, overlappingRounds));

            sections.Add(new PatternSection(
                section.Id, section.DisplayName, construction.Id,
                result.Steps, result.StitchesOut));

            stitches = result.StitchesOut;
            overlappingRounds = result.OverlappingRounds;
        }

        return new Pattern(garment.DisplayName, sections);
    }
}
