using System.Globalization;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Hat;

public sealed class StraightBody : IConstruction
{
    public const string ConstructionId = "straight-body";

    public string Id => ConstructionId;
    public string DisplayName => "Straight stockinette body";
    public GarmentType GarmentType => GarmentType.Hat;
    public string SectionId => HatSections.Body;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } = [MeasurementKeys.BodyHeight];

    public ConstructionResult Generate(ConstructionContext c)
    {
        var rounds = c.Gauge.RowsFor(c.Measurements.Get(MeasurementKeys.BodyHeight));
        var steps = new List<string> { $"Knit every round for {rounds} rounds ({c.StitchesIn} stitches)." };

        return new ConstructionResult(steps, c.StitchesIn,0);
    }
}
