using System.Globalization;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Sock.Leg;

public sealed class OnexOneRibbedLeg : IConstruction
{
    public const string ConstructionId = "stockinette-leg";

    public string Id => ConstructionId;
    public string DisplayName => "stockinette leg";
    public GarmentType GarmentType => GarmentType.Sock;
    public string SectionId => SockSections.Leg;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } = [MeasurementKeys.LegHeight];

    public ConstructionResult Generate(ConstructionContext c)
    {
        var rounds = c.Gauge.RowsFor(c.Measurements.Get(MeasurementKeys.LegHeight)) - c.OverlappingRounds;
        var steps = new List<string> 
        {
            $"row 1-{rounds} - Work in 1x1 rib (k1, p1) to the end of round [{c.StitchesIn} sts]." 
        };

        return new ConstructionResult(steps, c.StitchesIn, 0);
    }
}
