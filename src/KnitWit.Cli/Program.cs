using KnitWit.Core;
using KnitWit.Core.Domain;

var generator = KnitWitDefaults.CreateGenerator();

var pattern = generator.Generate(new PatternRequest
{
    GarmentType = GarmentType.Sock,
    Gauge = new Gauge(StitchesPer10Cm: 34, RowsPer10Cm: 40),
    Measurements = Measurements.Of(
        (MeasurementKeys.CuffHeight, 4),
        (MeasurementKeys.FootCircumference, 21),
        (MeasurementKeys.LegHeight, 8)),
    // ConstructionChoices = new Dictionary<string, string> { ["toe"] = "wedge-toe" }
});

Console.WriteLine($"=== {pattern.Title} ===");
foreach (var section in pattern.Sections)
{
    Console.WriteLine($"\n{section.Title} [{section.ConstructionId}]");
    foreach (var step in section.Steps) Console.WriteLine($"  - {step}");
}