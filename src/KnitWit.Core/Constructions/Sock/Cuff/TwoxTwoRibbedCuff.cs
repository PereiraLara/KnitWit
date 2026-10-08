using System.Globalization;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Sock.Cuff;

public sealed class TwoxTwoRibbedCuff : IConstruction
{
    public const string ConstructionId = "2x2-ribbed-cuff";
    private const double NegativeEase = 0.9; // socks are knitted 10% smaller than the foot

    public string Id => ConstructionId;
    public string DisplayName => "2x2 ribbed cuff";
    public GarmentType GarmentType => GarmentType.Sock;
    public string SectionId => SockSections.Cuff;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } =
        [MeasurementKeys.FootCircumference, MeasurementKeys.CuffHeight];

    public ConstructionResult Generate(ConstructionContext c)
    {
        var foot = c.Measurements.Get(MeasurementKeys.FootCircumference);
        var rounds = c.Gauge.RowsFor(c.Measurements.Get(MeasurementKeys.CuffHeight));

        // Multiple of 4 for 2x2 ribing.
        var castOn = StitchMath.RoundToMultiple(c.Gauge.StitchesFor(foot * NegativeEase), 4);

        var steps = new List<string>
        {
            $"CO {castOn} stitches with preferred stretchy cast on method.",
            $"Join in the round making sure not to twist the stitches.",
            $"row 1-{rounds} - Work in 2x2 rib (k2, p2) to the end of round. [{castOn} sts.]"
        };

        return new ConstructionResult(steps, castOn, 0);
    }
}
