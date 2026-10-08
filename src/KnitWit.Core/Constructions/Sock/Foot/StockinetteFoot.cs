using System.Globalization;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Sock.Foot;

public sealed class StockinetteFoot : IConstruction
{
    public const string ConstructionId = "stockinette-foot";

    public string Id => ConstructionId;
    public string DisplayName => "stockinette foot";
    public GarmentType GarmentType => GarmentType.Sock;
    public string SectionId => SockSections.Foot;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } = new[] { MeasurementKeys.FootCircumference };
    public ConstructionResult Generate(ConstructionContext c)
    {
        // subtract 4.5 cm from the measured foot circumference before converting to rows
        var footCircumference = c.Measurements.Get(MeasurementKeys.FootCircumference) - 4.5;
        var rounds = c.Gauge.RowsFor(footCircumference) - c.OverlappingRounds;
        var steps = new List<string>
        {
            $"row 1-{rounds} - k to the end of round [{c.StitchesIn} sts]."
        };

        return new ConstructionResult(steps, c.StitchesIn, 0);
    }
}
