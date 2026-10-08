using KnitWit.Core.Constructions.Hat;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Garments;

public sealed class HatGarment : IGarment
{
    public GarmentType Type => GarmentType.Hat;
    public string DisplayName => "Hat";

    // Knit bottom-up, so sections are listed brim -> crown. Slots place each one on the sketch canvas.
    public IReadOnlyList<SectionDefinition> Sections { get; } =
    [
        new(HatSections.Brim,  "Brim",  RibbedBrim.ConstructionId),
        new(HatSections.Body,  "Body",  StraightBody.ConstructionId),
        new(HatSections.Crown, "Crown", RadialDecreaseCrown.ConstructionId)
    ];
}
