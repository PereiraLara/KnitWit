using System.Globalization;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Sock.Cuff;

public sealed class TwistedRibbedCuff : IConstruction
{
    public const string ConstructionId = "twisted-ribbed-cuff";
    private const double NegativeEase = 0.9; // socks are knitted 10% smaller than the foot

    public string Id => ConstructionId;
    public string DisplayName => "Twisted ribbed cuff";
    public GarmentType GarmentType => GarmentType.Sock;
    public string SectionId => SockSections.Cuff;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } =
        [MeasurementKeys.FootCircumference, MeasurementKeys.CuffHeight];

    public ConstructionResult Generate(ConstructionContext c)
    {
        var foot = c.Measurements.Get(MeasurementKeys.FootCircumference);
        var rounds = c.Gauge.RowsFor(c.Measurements.Get(MeasurementKeys.CuffHeight));

        // Multiple of 2 for 1x1 ribing + 4 sts to compensate for twisted sts lack of stretch.
        var castOn = StitchMath.RoundToMultiple(c.Gauge.StitchesFor(foot * NegativeEase), 2) + 4;

        var steps = new List<string>
        {
            $"CO {castOn} stitches with preferred stretchy cast on method.",
            $"Join in the round making sure not to twist the stitches.",
            $"row 1-{rounds} - Work in twisted rib (k1-tbl, p1) to the end of round. [{castOn} sts.]",
            $"row {rounds + 1} - * k{(castOn-4)/4}, k2tog * repeat * - * to the end of round. [{castOn-4} sts.]"
        };

        return new ConstructionResult(steps, castOn - 4, 1);
    }
}
