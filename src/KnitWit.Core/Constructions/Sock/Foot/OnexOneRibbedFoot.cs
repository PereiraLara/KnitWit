using System.Globalization;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Sock.Foot;

public sealed class OnexOneRibbedFoot : IConstruction
{
    public const string ConstructionId = "1x1-ribbed-foot";

    public string Id => ConstructionId;
    public string DisplayName => "1x1-ribbed-foot";
    public GarmentType GarmentType => GarmentType.Sock;
    public string SectionId => SockSections.Foot;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } = [];
    public ConstructionResult Generate(ConstructionContext c)
    {
        // subtract Toe length from the measured foot length before converting to rows
        var lengthToKnit = c.Measurements.Get(MeasurementKeys.FootLength) - c.Measurements.ToeLength();
        var rounds = c.Gauge.RowsFor(lengthToKnit) - c.OverlappingRounds;
        if (rounds < 1)
            throw new InvalidOperationException("Foot length minus toe length leaves no rounds for the foot. Check the measurements.");

        var steps = new List<string>
        {
            $"row 1-{rounds} -  Work in 1x1 rib (k1, p1) to the end of round [{c.StitchesIn} sts]."
        };

        return new ConstructionResult(steps, c.StitchesIn, 0);
    }
}
