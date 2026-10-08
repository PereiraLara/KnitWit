using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Sock.Toe;

public sealed class WedgeToe : IConstruction
{
    public const string ConstructionId = "wedge-toe";

    public string Id => ConstructionId;
    public string DisplayName => "wedge toe";
    public GarmentType GarmentType => GarmentType.Sock;
    public string SectionId => SockSections.Toe;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } = [];

    public ConstructionResult Generate(ConstructionContext c)
    {
        int rounds = c.Gauge.RowsFor(4.5);
        if (rounds < c.StitchesIn * 3 / 16 || rounds > c.StitchesIn * 3 / 8)
            throw new InvalidOperationException("Gauge and stitch count give a toe length that can't be decreased evenly. Check the gauge.");

        var steps = new List<string>
        {
            $"setup row - * k1, place marker, k{c.StitchesIn/2 - 2}, place marker, k1 *, repeat from * once more.",
            $"row 1 - * k1, slip marker, ssk,knit to 3 sts before the marker, k2tog, slip marker, k1 *, repeat from * once more. (4 sts decreased)",
            $"row 2 - k to the end of round.",
            $"repeat row 1 and 2 a total of {rounds/4} times. [{c.StitchesIn - rounds/4 * 4} sts]",
            $"repeat row 1 a total of {rounds - 2 * (rounds/4)} times. [{c.StitchesIn - (rounds - rounds/4) * 4} sts]",
            $"Fasten off with Kitchener Stitch."
        };

        return new ConstructionResult(steps, c.StitchesIn - (rounds - rounds / 4) * 4, 0);
    }
}