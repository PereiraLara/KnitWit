// tests/KnitWit.Core.Tests/AllCombinationsTests.cs
using System.Text.RegularExpressions;
using KnitWit.Core.Domain;
using Xunit;

namespace KnitWit.Core.Tests;

public class AllCombinationsTests
{
    [Fact]
    public void Every_sock_construction_combination_generates()
    {
        var constructions = KnitWitDefaults.CreateConstructions();
        var generator = KnitWitDefaults.CreateGenerator();
        var sock = KnitWitDefaults.CreateGarments().Get(GarmentType.Sock);

        // Cartesian product of every construction in every section.
        IEnumerable<Dictionary<string, string>> combos = [new()];
        foreach (var section in sock.Sections)
            combos = combos.SelectMany(c => constructions.For(GarmentType.Sock, section.Id)
                .Select(k => new Dictionary<string, string>(c) { [section.Id] = k.Id })).ToList();

        var failures = new List<string>();
        foreach (var choices in combos)
        {
            var label = string.Join(" + ", choices.Values);
            try
            {
                var pattern = generator.Generate(new PatternRequest
                {
                    GarmentType = GarmentType.Sock,
                    Gauge = new Gauge(34, 40),
                    Measurements = Measurements.Of(
                        (MeasurementKeys.CuffHeight, 4),
                        (MeasurementKeys.FootCircumference, 21),
                        (MeasurementKeys.LegHeight, 8)),
                    ConstructionChoices = choices,
                });

                // A negative count in the text (e.g. "row 1--3") means a round/stitch calculation went wrong.
                foreach (var step in pattern.Sections.SelectMany(s => s.Steps))
                    if (Regex.IsMatch(step, @"(?<!\w)-\d"))
                        failures.Add($"{label}: negative number in \"{step}\"");
            }
            catch (Exception e)
            {
                failures.Add($"{label}: {e.GetType().Name} - {e.Message}");
            }
        }

        Assert.True(failures.Count == 0, string.Join(Environment.NewLine, failures));
    }
}