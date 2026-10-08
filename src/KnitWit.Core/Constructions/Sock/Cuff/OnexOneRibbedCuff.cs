using System.Globalization;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Sock.Cuff;

public sealed class OnexOneRibbedCuff : IConstruction
{
    public const string ConstructionId = "1x1-ribbed-cuff";
    private const double NegativeEase = 0.9; // socks are knitted 10% smaller than the foot

    public string Id => ConstructionId;
    public string DisplayName => "1x1 ribbed cuff";
    public GarmentType GarmentType => GarmentType.Sock;
    public string SectionId => SockSections.Cuff;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } = [MeasurementKeys.FootCircumference, MeasurementKeys.CuffHeight];

    public ConstructionResult Generate(ConstructionContext c)
    {
        var foot = c.Measurements.Get(MeasurementKeys.FootCircumference);
        var rounds = c.Gauge.RowsFor(c.Measurements.Get(MeasurementKeys.CuffHeight));

        // Multiple of 2 for 1x1 ribing.
        var castOn = StitchMath.RoundToMultiple(c.Gauge.StitchesFor(foot * NegativeEase), 2);

        var steps = new List<string>
        {
            $"CO {castOn} stitches with preferred stretchy cast on method.",
            $"Join in the round making sure not to twist the stitches.",
            $"row 1-{rounds} - Work in 1x1 rib (k1, p1) to the end of round."
        };

        return new ConstructionResult(steps, castOn, 0);
    }
}
