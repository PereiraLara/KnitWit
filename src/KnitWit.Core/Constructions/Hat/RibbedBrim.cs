using System.Globalization;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions.Hat;

public sealed class RibbedBrim : IConstruction
{
    public const string ConstructionId = "ribbed-brim";
    private const double NegativeEase = 0.9; // hat is knit 10% smaller than the head

    public string Id => ConstructionId;
    public string DisplayName => "2x2 ribbed brim";
    public GarmentType GarmentType => GarmentType.Hat;
    public string SectionId => HatSections.Brim;

    public IReadOnlyCollection<string> RequiredMeasurements { get; } =
        [MeasurementKeys.HeadCircumference, MeasurementKeys.BrimHeight];

    public ConstructionResult Generate(ConstructionContext c)
    {
        var head = c.Measurements.Get(MeasurementKeys.HeadCircumference);
        var rounds = c.Gauge.RowsFor(c.Measurements.Get(MeasurementKeys.BrimHeight));

        // Multiple of 8 so the crown can decrease evenly later (8 is also a multiple of 4 for 2x2 rib).
        var castOn = StitchMath.RoundToMultiple(c.Gauge.StitchesFor(head * NegativeEase), 8);

        var steps = new List<string>
        {
            $"Cast on {castOn} stitches and join in the round.",
            $"Work k2, p2 ribbing for {rounds} rounds."
        };   

        return new ConstructionResult(steps, castOn,0);
    }
}
