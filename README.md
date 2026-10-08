# KnitWit

Knitting pattern generator skeleton: **gauge + measurements + garment + construction -> pattern steps + layered 2D sketch.**
This is a baseline, not a finished app. One garment (Hat) is implemented end to end so you can see the shape of things.

## Run it

```bash
dotnet run --project src/KnitWit.Cli      # prints a hat pattern, writes output/hat.svg
dotnet test                               # two smoke tests
```

Target framework lives in one place: `Directory.Build.props`.

## Layout

```
KnitWit.slnx
Directory.Build.props              shared settings (TFM, nullable, implicit usings)
src/
  KnitWit.Core/                    all the logic, no UI, no I/O
    Domain/                        Gauge, Measurements, GarmentType, PatternRequest
    Garments/                      IGarment (ordered sections) + HatGarment
    Constructions/                 IConstruction  <- main extension point
      Hat/                         RibbedBrim, StraightBody, RadialDecreaseCrown
    Patterns/                      Pattern output model + PatternGenerator (the orchestrator)
    Sketching/                     SketchLayer, SketchSlot, ISketchRenderer, SvgSketchRenderer
    Registry/                      GarmentRegistry, ConstructionRegistry
    KnitWitDefaults.cs             the ONE place things get registered
  KnitWit.Cli/                     throwaway console front-end (swap for Blazor/web/API later)
tests/KnitWit.Core.Tests/          xUnit
docs/ADDING_CONSTRUCTIONS.md       how to extend it
```

## Core ideas

- A **garment** is an ordered list of **sections** (hat: brim, body, crown).
- Each section has several **constructions** to choose from (sock heel: heel flap / German short rows).
- A construction receives gauge, measurements, and the stitch count from the previous section, and returns
  steps and the stitch count it hands on.

## Not here yet (deliberately)

Colorwork charts, 3D rendering, persistence, UI, units/ease options, shaping math beyond the hat crown.
