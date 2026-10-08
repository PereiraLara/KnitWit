////using KnitWit.Core;
////using KnitWit.Core.Domain;

////var generator = KnitWitDefaults.CreateGenerator();

//////var pattern = generator.Generate(new PatternRequest
//////{
//////    GarmentType = GarmentType.Hat,
//////    Gauge = new Gauge(StitchesPer10Cm: 22, RowsPer10Cm: 30),
//////    Measurements = Measurements.Of(
//////        (MeasurementKeys.HeadCircumference, 56),
//////        (MeasurementKeys.BrimHeight, 5),
//////        (MeasurementKeys.BodyHeight, 12)),
//////    // ConstructionChoices = new Dictionary<string, string> { ["crown"] = "your-new-crown-id" }
//////});

//////Console.WriteLine($"=== {pattern.Title} ===");
//////foreach (var section in pattern.Sections)
//////{
//////    Console.WriteLine($"\n{section.Title} [{section.ConstructionId}]");
//////    foreach (var step in section.Steps) Console.WriteLine($"  - {step}");
//////}


////var pattern = generator.Generate(new PatternRequest
////{
////    GarmentType = GarmentType.Sock,
////    Gauge = new Gauge(StitchesPer10Cm: 34, RowsPer10Cm: 40),
////    Measurements = Measurements.Of(
////            (MeasurementKeys.CuffHeight, 4),
////            (MeasurementKeys.FootCircumference, 21),
////            (MeasurementKeys.LegHeight, 8))
////    // ConstructionChoices = new Dictionary<string, string> { ["crown"] = "your-new-crown-id" }
////});

////Console.WriteLine($"=== {pattern.Title} ===");
////foreach (var section in pattern.Sections)
////{
////    Console.WriteLine($"\n{section.Title} [{section.ConstructionId}]");
////    foreach (var step in section.Steps) Console.WriteLine($"  - {step}");
////}

//// src/KnitWit.Api/Program.cs  (csproj: Microsoft.NET.Sdk.Web + ProjectReference to KnitWit.Core)
//using KnitWit.Core;
//using KnitWit.Core.Domain;
//using KnitWit.Core.Patterns;

//var garments = KnitWitDefaults.CreateGarments();         // see KnitWitDefaults.cs change below
//var constructions = KnitWitDefaults.CreateConstructions();
//var generator = new PatternGenerator(garments, constructions);

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddCors(o => o.AddDefaultPolicy(p =>
//    p.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod()));

//var app = builder.Build();
//app.UseCors();
//app.UseStaticFiles();   // wwwroot/sketches/{garment}/{section}/{constructionId}.svg

//static string Slug(GarmentType t) => t.ToString().ToLowerInvariant();

//// Catalog for the Vue pickers: garment -> sections -> available constructions.
//app.MapGet("/api/garments", () =>
//    garments.All.Select(g => new
//    {
//        id = Slug(g.Type),
//        name = g.DisplayName,
//        sections = g.Sections.Select(s => new
//        {
//            id = s.Id,
//            name = s.DisplayName,
//            defaultConstructionId = s.DefaultConstructionId,
//            constructions = constructions.For(g.Type, s.Id).Select(c => new
//            {
//                id = c.Id,
//                name = c.DisplayName,
//                requires = c.RequiredMeasurements,
//                sketchUrl = $"/sketches/{Slug(g.Type)}/{s.Id}/{c.Id}.svg",
//            }),
//        }),
//    }));

//// Returns the existing Pattern record as-is (Title, Sections[SectionId, Title, ConstructionId, Steps, StitchesAfter]).
//app.MapPost("/api/patterns", (PatternDto dto) =>
//{
//    if (!Enum.TryParse<GarmentType>(dto.Garment, ignoreCase: true, out var type))
//        return Results.Problem($"Unknown garment '{dto.Garment}'.", statusCode: 400);

//    try
//    {
//        var pattern = generator.Generate(new PatternRequest
//        {
//            GarmentType = type,
//            Gauge = new Gauge(dto.Gauge.StitchesPer10Cm, dto.Gauge.RowsPer10Cm),
//            Measurements = new Measurements(dto.Measurements),
//            ConstructionChoices = dto.Selections ?? new Dictionary<string, string>(),
//        });
//        return Results.Ok(pattern);
//    }
//    // Missing measurement, unknown construction, toe/crown that can't be decreased evenly...
//    catch (Exception e) when (e is ArgumentException or InvalidOperationException or KeyNotFoundException)
//    {
//        return Results.Problem(e.Message, statusCode: 400);
//    }
//});

//app.Run();

//public sealed record GaugeDto(double StitchesPer10Cm, double RowsPer10Cm);
//public sealed record PatternDto(
//    string Garment,
//    GaugeDto Gauge,
//    Dictionary<string, double> Measurements,
//    Dictionary<string, string>? Selections);   // sectionId -> constructionId