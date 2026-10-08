using System.Globalization;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Sock.Heel;

public sealed class HeelFlap_Gusset : IConstruction
{
    public const string ConstructionId = "heel-flap-Gusset";
    private const double NegativeEase = 0.9; // socks are knitted 10% smaller than the foot

    public string Id => ConstructionId;
    public string DisplayName => "Heel Flap and Gusset";
    public GarmentType GarmentType => GarmentType.Sock;
    public string SectionId => SockSections.Heel;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } = [];

    public ConstructionResult Generate(ConstructionContext c)
    {
        var rounds = c.Gauge.RowsFor(5) * 2;


        int halfCastOn = c.StitchesIn / 2;

        int sideStitches = halfCastOn / 3;
        int centerStitches = halfCastOn - (2 * sideStitches); // middle absorbs the remainder


        var steps = new List<string>
        {
            $"Setup row - k across {halfCastOn} sts.",
            $"Note from now until specified only half the stitches will be worked.",

            $"",
            $"== Heel Flap ==.",
            $"row 1 (RS) - *sl1, k1*, repeat * - * to end of row, turn.",
            $"row 2 (WS) - sl1, p to the end, turn.",
            $"repeat rows 1-2 a total of {rounds} times.",
             
            $"",
            $"== Turn Heel ==.",
            $"row 1 (RS) - k{centerStitches + sideStitches - 2}, ssk, k1, turn.",
            $"row 2 (WS) - slp1 purlwise, p{centerStitches - 1}, p2tog, p1, turn.",
            $"row 3 (RS) - slp1 purlwise, k to 1sts before the gap, ssk, k1, turn.",
            $"row 4 (WS) - slp1 purlwise, p to 1sts before the gap, p2tog, p1, turn.",
            $"repeat rows 3-4 a total of {sideStitches - 1} times. (until you reached the outer edges on both sides )",
            $"Note from now on all stitches will be worked again.",

            $"",
            $"== Gusset ==.",
            $"Setup row - k across remaining heel sts, pick up {rounds/2 + 1} sts along the side of the heel flap, k{halfCastOn}, pick up {rounds/2 + 1} along the side of the heel flap, k to begining of round",
            $"row 1 - k{halfCastOn + 1}, ssk, k to 3 sts before end of round, k2tog, k1.",
            $"row 2 - k all sts.",
            $"repeat rows 1-2 a total of {rounds/4 + 1} times. (until you reached a total of {c.StitchesIn} sts)",
        };

        int overlapping = 2 + sideStitches-1 + 1 + rounds/2 + 1;

        return new ConstructionResult(steps, c.StitchesIn, overlapping);
    }
}
