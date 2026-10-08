# Adding things

## A. New construction for an existing section (e.g. a second hat crown)

1. Create `src/KnitWit.Core/Constructions/Hat/PleatedCrown.cs` implementing `IConstruction`:

```csharp
public sealed class PleatedCrown : IConstruction
{
    public const string ConstructionId = "pleated-crown";

    public string Id => ConstructionId;                       // stable, used in requests/saved data
    public string DisplayName => "Pleated crown";             // shown to users
    public GarmentType GarmentType => GarmentType.Hat;
    public string SectionId => HatSections.Crown;             // which slot it plugs into
    public IReadOnlyCollection<string> RequiredMeasurements { get; } = [];

    public ConstructionResult Generate(ConstructionContext c)
    {
        var steps = new List<string>();
        // c.Gauge, c.Measurements, c.StitchesIn are your inputs
        // ... do the maths, add steps ...
        var layer = new SketchLayer("hat-crown", 30, "<path d='...'/>"); // draw inside c.Slot
        return new ConstructionResult(steps, StitchesOut: 8, layer);
    }
}
```

2. Register it in `KnitWitDefaults.CreateGenerator()`:

```csharp
.Register(new PleatedCrown())
```

3. Use it by picking it in the request:

```csharp
ConstructionChoices = new Dictionary<string, string> { ["crown"] = "pleated-crown" }
```

Rules of thumb: take everything from `ConstructionContext` (no globals), list every measurement key you read in
`RequiredMeasurements`, and return the live stitch count so the next section can continue from it.

## B. New garment type (e.g. Sock)

1. Add a value to `GarmentType` if missing (Sock already exists).
2. Add `Constructions/Sock/SockSections.cs` with section id constants (`Cuff`, `Leg`, `Heel`, `Foot`, `Toe`).
3. Add measurement keys to `MeasurementKeys` (foot circumference, foot length, ...).
4. Add constructions per section (see A). For a section with several options, add several classes
   with the same `SectionId` and different `Id`s (`heel-flap`, `german-short-rows`).
5. Add `Garments/SockGarment.cs` implementing `IGarment`: list sections in knitting order, each with a
   default construction id and a `SketchSlot` on the 200x300 canvas.
6. Register the garment and its constructions in `KnitWitDefaults`.
7. Add a test in `tests/KnitWit.Core.Tests` that generates the pattern and checks stitch counts.

## C. Layered sweater sketches (when you get there)

Each construction returns one `SketchLayer` with its own `Id` and `ZOrder`. A raglan sleeve and a set-in
sleeve are just two constructions for the `sleeve` section that draw different fragments, and
`SvgSketchRenderer` stacks whatever was chosen. For a richer look, replace the fragment with an embedded
SVG asset per part, or implement a new `ISketchRenderer` (3D mesh) that consumes the same layers/slots.
