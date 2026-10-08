using System.Text.RegularExpressions;
using KnitWit.Core.Domain;
using Xunit;

namespace KnitWit.Core.Tests;

public class ToeTests
{
    private static readonly string[] Toes = ["star-toe", "round-toe", "wedge-toe"];

    [Fact]
    public void Every_toe_closes_correctly_across_gauges_and_foot_sizes()
    {
        var generator = KnitWitDefaults.CreateGenerator();
        var failures = new List<string>();

        for (int stitches = 18; stitches <= 40; stitches++)
            foreach (var rowRatio in new[] { 1.15, 1.3, 1.5 })
                for (int foot = 18; foot <= 26; foot++)
                    foreach (var toe in Toes)
                    {
                        var gauge = new Gauge(stitches, Math.Round(stitches * rowRatio));
                        var label = $"{toe} @ {gauge.StitchesPer10Cm}/{gauge.RowsPer10Cm} sts/rows per 10 cm, foot {foot} cm";

                        try
                        {
                            var pattern = generator.Generate(new PatternRequest
                            {
                                GarmentType = GarmentType.Sock,
                                Gauge = gauge,
                                Measurements = Measurements.Of(
                                    (MeasurementKeys.CuffHeight, 4),
                                    (MeasurementKeys.FootCircumference, foot),
                                    (MeasurementKeys.LegHeight, 8)),
                                ConstructionChoices = new Dictionary<string, string> { ["toe"] = toe },
                            });

                            var section = pattern.Sections[^1];

                            if (toe == RoundToeId)
                            {
                                // Round toe closes completely.
                                if (section.StitchesAfter != 0)
                                    failures.Add($"{label}: round toe should end on 0 sts, got {section.StitchesAfter}");
                            }
                            else
                            {
                                // Star and wedge toes must never decrease below 8 stitches...
                                if (section.StitchesAfter < 8)
                                    failures.Add($"{label}: toe ends on {section.StitchesAfter} sts (minimum 8)");

                                // ...and the last "[n sts]" printed in the instructions must match what the section returns.
                                var printed = section.Steps
                                    .SelectMany(s => Regex.Matches(s, @"\[(\d+) sts\]").Cast<Match>())
                                    .Select(m => int.Parse(m.Groups[1].Value))
                                    .DefaultIfEmpty(-1)
                                    .Last();
                                if (printed != -1 && printed != section.StitchesAfter)
                                    failures.Add($"{label}: text says {printed} sts, section returns {section.StitchesAfter}");
                            }

                            // A negative number in the text (e.g. "row 1--3") means a round/stitch calculation went wrong.
                            foreach (var step in pattern.Sections.SelectMany(s => s.Steps))
                                if (Regex.IsMatch(step, @"(?<!\w)-\d"))
                                    failures.Add($"{label}: negative number in \"{step}\"");
                        }
                        catch (Exception e)
                        {
                            failures.Add($"{label}: {e.GetType().Name} - {e.Message}");
                        }
                    }

        Assert.True(failures.Count == 0,
            $"{failures.Count} failures:{Environment.NewLine}" + string.Join(Environment.NewLine, failures.Take(20)));
    }

    private const string RoundToeId = "round-toe";
}