using KnitWit.Core.Domain;

namespace KnitWit.Core.Constructions;

/// <summary>
/// One way of knitting one section of one garment (e.g. Sock / Heel / "heel-flap").
/// This is the main extension point of the app.
/// </summary>
public interface IConstruction
{
    string Id { get; }
    string DisplayName { get; }
    GarmentType GarmentType { get; }
    string SectionId { get; }

    /// <summary>Measurement keys this construction reads. Validated before Generate is called.</summary>
    IReadOnlyCollection<string> RequiredMeasurements { get; }

    ConstructionResult Generate(ConstructionContext context);
}

/// <summary>Everything a construction gets to work with.</summary>
public sealed record ConstructionContext(
    Gauge Gauge,
    Measurements Measurements,
    int StitchesIn,          // live stitches handed over by the previous section (0 for the first)
    int OverlappingRounds   // where rounds from the previous section overlap this section (null if no overlap)
    );        

public sealed record ConstructionResult(
    IReadOnlyList<string> Steps,
    int StitchesOut,         // live stitches handed to the next section
    int OverlappingRounds   // where rounds from the previous section overlap this section (null if no overlap)
    );
