using System.Globalization;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Hat;

public sealed class RadialDecreaseCrown : IConstruction
{
    public const string ConstructionId = "radial-decrease";
    private const int Wedges = 8;

    public string Id => ConstructionId;
    public string DisplayName => "8-wedge radial decrease";
    public GarmentType GarmentType => GarmentType.Hat;
    public string SectionId => HatSections.Crown;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } = [];

    public ConstructionResult Generate(ConstructionContext c)
    {
        if (c.StitchesIn % Wedges != 0)
            throw new InvalidOperationException($"Crown needs a multiple of {Wedges} stitches, got {c.StitchesIn}.");

        var steps = new List<string>();
        var stitches = c.StitchesIn;

        while (stitches > Wedges)
        {
            var plain = stitches / Wedges - 2;
            var repeat = plain > 0 ? $"k{plain}, k2tog" : "k2tog";
            stitches -= Wedges;
            steps.Add($"Decrease round: [{repeat}] x{Wedges} ({stitches} sts left).");
            if (stitches > Wedges) steps.Add("Knit one plain round.");
        }

        steps.Add($"Cut yarn, thread through the remaining {stitches} stitches and pull tight.");

        return new ConstructionResult(steps, stitches,0);
    }
}
