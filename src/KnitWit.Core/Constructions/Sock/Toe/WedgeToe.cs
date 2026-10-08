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
        if (c.StitchesIn < 16) throw new InvalidOperationException($"Wedge toe needs an even stitch count of at least 16, got {c.StitchesIn}.");
        int extra = c.StitchesIn % 4;                 // 0 or 2: stitches to remove so the 4 wedges divide evenly

        int rounds = c.Gauge.RowsFor(c.Measurements.ToeLength());
        if (rounds < 1)
            throw new InvalidOperationException("Gauge gives a toe length of 0 rounds. Check the gauge.");

        var steps = new List<string>();
        int row = 0;

        int PossDecr = (c.StitchesIn - 8) / 4;    // possible decreases, 4 decreased stitches per round, 8 stitches left for the last round

        int decreaseRounds = Math.Min(PossDecr, rounds);

        int neededPlainRounds = rounds - decreaseRounds; // the number of plain rounds needed to reach the desired length

        int leadingPlainRounds = Math.Max(0, neededPlainRounds - (decreaseRounds - 1));
        int pairedPlainRounds = neededPlainRounds - leadingPlainRounds;
        int tailRepeats = decreaseRounds - 1 - pairedPlainRounds;   // decrease-only rounds at the end

        if (leadingPlainRounds > 0)
        {
            steps.Add(leadingPlainRounds == 1
                ? $"row {++row} - k to the end of round."
                : $"row {row + 1}-{row += leadingPlainRounds} - k to the end of round.");
        }

        if (extra > 0)
        {
            steps.Add($"setup row - * k{c.StitchesIn / extra - 2}, k2tog; repeat from * {extra - 1} times. [{extra} sts decreased]");
            rounds--;
        }

        steps.Add($"setup row - * k1, place marker, ssk, k{c.StitchesIn/2 - 2}, k2tog, place marker, k1 *, repeat from * once more. [4 sts decreased]");
        const string decrease = "* k to marker, slip marker, ssk, k to 2 sts before the marker, k2tog, slip marker, repeat from * to once more. [4 sts decreased]";

        if (pairedPlainRounds > 0)
        {
            steps.Add($"row {++row} - k to the end of round.");
            steps.Add($"row {++row} - {decrease}");
            if (pairedPlainRounds > 1)
                steps.Add($"repeat rows {row - 1}-{row} {pairedPlainRounds - 1} more times. [{c.StitchesIn - 4 * (1 + pairedPlainRounds)} sts]");
        }
        else if (tailRepeats > 0)
        {
            steps.Add($"row {++row} - {decrease}");
            tailRepeats--;   // this row is the first decrease-only round
        }

        if (tailRepeats > 0)
            steps.Add($"repeat row {row} {tailRepeats} more times. [{c.StitchesIn - 4 * decreaseRounds} sts]");

        steps.Add( $"Fasten off and graft the remaining {c.StitchesIn - 4 * decreaseRounds} stitches with Kitchener Stitch.");

        return new ConstructionResult(steps, c.StitchesIn - 4 * decreaseRounds, 0);
    }
}