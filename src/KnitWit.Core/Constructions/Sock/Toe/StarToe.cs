using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Sock.Toe;

public sealed class StarToe : IConstruction
{
    public const string ConstructionId = "star-toe";

    public string Id => ConstructionId;
    public string DisplayName => "star toe";
    public GarmentType GarmentType => GarmentType.Sock;
    public string SectionId => SockSections.Toe;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } = [];

    public ConstructionResult Generate(ConstructionContext c)
    {
        int rounds = c.Gauge.RowsFor(4.5);
        var steps = new List<string> ();
        int row = 0;

        int PossDecr = (c.StitchesIn - 8) / 4;    // possible decreases, 4 decreased stitches per round, 8 stitches left for the last round
        bool Kitchner = rounds < PossDecr;  // if the number of rounds is less than the possible decreases, we can kitchner stitch the last round as there'll be more than 8 stitches left.

        steps.Add($"setup row - * k{c.StitchesIn/4 - 2}, place marker, k2tog, repeat from * to beginning of round. [4 sts decreased]");

        if(Kitchner)
        {
            steps.AddRange(new List<string>
            {
                $"row {++row} - * k to 2 sts before the marker, slip marker, k2tog, repeat from * to beginning of round.",
                $"repeat row {row} a total of {rounds - 1} times. [{c.StitchesIn - rounds / 4} sts]",
                $"Fasten off and Kitchener Stitch."
            });
        }

        int neededPlainRounds = rounds - PossDecr; // the number of plain rounds needed to reach the desired length
        if(neededPlainRounds > 0)
        {
            steps.Add( $"row {++row} - k to the end of round.");
        }

        steps.Add($"row {++row} - * k to 2 sts before the marker, slip marker, k2tog, repeat from * to beginning of round. [4 sts decreased]");

        if (neededPlainRounds > 0) steps.Add($"repeat row {row - 1} and {row} a total of {neededPlainRounds} times. [{c.StitchesIn - neededPlainRounds * 4} sts]");
        steps.Add($"repeat row {row} a total of {rounds - neededPlainRounds * 2 - 1} times. [{c.StitchesIn - neededPlainRounds * 4 - (rounds - neededPlainRounds * 2 - 1) * 4} sts]");

        steps.AddRange(new List<string>
        {
            $"Thread yarn through remaining stitches, draw closed, and secure."
        });

        return new ConstructionResult(steps, c.StitchesIn - (rounds - rounds / 4) * 4, 0);
    }
}