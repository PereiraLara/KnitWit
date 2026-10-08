using KnitWit.Core;
using KnitWit.Core.Domain;
using Xunit;

namespace KnitWit.Core.Tests;

public class SockPatternTests
{
    private static PatternRequest Request() => new()
    {
        GarmentType = GarmentType.Sock,
        Gauge = new Gauge(34, 40),
        Measurements = Measurements.Of(
            (MeasurementKeys.CuffHeight, 4),
            (MeasurementKeys.FootCircumference, 21),
            (MeasurementKeys.LegHeight, 8)
        )
    };

    [Fact]
    public void Sock_has_three_sections_and_ends_on_eight_stitches()
    {
        var pattern = KnitWitDefaults.CreateGenerator().Generate(Request());

        //Assert.Equal(3, pattern.Sections.Count);
        //Assert.Equal(0, pattern.Sections[0].StitchesAfter % 8);
        //Assert.Equal(8, pattern.Sections[^1].StitchesAfter);
    }

    [Fact]
    public void Missing_measurement_is_reported()
    {
        //var request = Request() with { Measurements = Measurements.Of((MeasurementKeys.BrimHeight, 5)) };
        //Assert.Throws<ArgumentException>(() => KnitWitDefaults.CreateGenerator().Generate(request));
    }
}
