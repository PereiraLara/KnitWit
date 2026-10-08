using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Sock.Toe;

public sealed class RoundToe : IConstruction
{
    public const string ConstructionId = "round-toe";

    public string Id => ConstructionId;
    public string DisplayName => "round toe";
    public GarmentType GarmentType => GarmentType.Sock;
    public string SectionId => SockSections.Toe;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } = [];

    public ConstructionResult Generate(ConstructionContext c)
    {
        int rounds = c.Gauge.RowsFor(4.5);
        //if (rounds < c.StitchesIn * 3 / 16 || rounds > c.StitchesIn * 3 / 8)
        //    throw new InvalidOperationException("Gauge and stitch count give a toe length that can't be decreased evenly. Check the gauge.");

        var steps = new List<string> ();

        int iniRounds = c.StitchesIn / 8 == rounds ? 0 : rounds - c.StitchesIn / 8;
        if(iniRounds > 0)
        {
            steps.Add($"row 1-{iniRounds} - k to the end of round.");
        }

        int iniDecr = c.StitchesIn % 8 == 0 ? 0 : c.StitchesIn % 8;
        int iniKs;
        if (iniDecr > 0)
        {
            iniKs = (c.StitchesIn - iniDecr * 2) / iniDecr;
            steps.Add($"row {iniRounds++} - * k{iniKs}, k2tog *, repeat *-* {iniDecr} times. ");
        }

        int Ks = c.StitchesIn / 8 - 2;
        for (int i = Ks; i > 0; i--)
        {
            steps.Add($"row {++iniRounds} - * k{i}, k2tog, repeat from * until beginning of round.");
        }

        steps.AddRange(new[]
        {
            $"row {++iniRounds} - k2tog to the end of round.",
            $"Break the yarn. Thread through a tapestry needle and draw through the remaining stitches. Pull tight, through to the wrong side and weave in end."
        });

        return new ConstructionResult(steps, 0, 0);
    }
}