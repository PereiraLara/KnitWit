using System.Globalization;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Sock.Heel;

public sealed class ShortRowHeel : IConstruction
{
    public const string ConstructionId = "short-row-heel";
    private const double NegativeEase = 0.9; // socks are knitted 10% smaller than the foot

    public string Id => ConstructionId;
    public string DisplayName => "Short Row Heel";
    public GarmentType GarmentType => GarmentType.Sock;
    public string SectionId => SockSections.Heel;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } = [];

    public ConstructionResult Generate(ConstructionContext c)
    {
        var foot = c.Measurements.Get(MeasurementKeys.FootCircumference);


        int halfCastOn = c.StitchesIn / 2;

        int sideStitches = halfCastOn / 3;
        int centerStitches = halfCastOn - (2 * sideStitches); // middle absorbs the remainder


        var steps = new List<string>
        {
            $"Setup row - k across {halfCastOn} sts.",
            $"Note from now until specified only half the stitches will be worked.",
            
            $"",
            $"== 1st Part ==.",
            $"row 1 (RS) - k{halfCastOn}, turn.",
            $"row 2 (WS) - GSR, p to the end, turn.",
            $"row 3 (RS) - GSR, k to the first double stitch, turn.",
            $"row 4 (WS) - GSR, p to the first double stitch, turn.",
            $"repeat rows 3-4 a total of {sideStitches - 1} times.",
            $"Note there should be {sideStitches * 2} double stitches in total.",
            
            $"",
            $"== Intersection ==.",
            $"row 1 - GSR, k to the beginning of the round.",
            $"row 2,3 - k{c.StitchesIn} sts.",
            $"row 4 - k{halfCastOn}",
            $"note from now until specified only half the stitches will be worked.",
            
            $"",
            $"== 2nd Part ==.",
            $"row 1 (RS) - k{centerStitches + sideStitches + 1}, turn.",
            $"row 2 (WS) - GSR, p{centerStitches + 1}, turn.",
            $"row 3 (RS) - GSR, k to the first double stitch, k the double stitch, turn.",
            $"row 4 (WS) - GSR, p to the first double stitch, p the double stitch, turn.",
            $"repeat rows 3-4 a total of {sideStitches - 1} times. (until you reached the outer edges on both sides )",
            $"last step - the last row is WS: make one more GSR as you turn, then continue in the round.",
        };

        return new ConstructionResult(steps, c.StitchesIn, sideStitches * 2 + 1);
    }
}
