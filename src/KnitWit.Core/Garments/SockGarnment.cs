using KnitWit.Core.Constructions.Sock;
using KnitWit.Core.Constructions.Sock.Cuff;
using KnitWit.Core.Constructions.Sock.Leg;
using KnitWit.Core.Constructions.Sock.Heel;
using KnitWit.Core.Constructions.Sock.Foot;
using KnitWit.Core.Constructions.Sock.Toe;
using KnitWit.Core.Domain;

namespace KnitWit.Core.Garments;

public sealed class SockGarment : IGarment
{
    public GarmentType Type => GarmentType.Sock;
    public string DisplayName => "Sock";

    // Knit bottom-up, so sections are listed brim -> crown. Slots place each one on the sketch canvas.
    public IReadOnlyList<SectionDefinition> Sections { get; } =
    [
        //new(SockSections.Cuff,  "Cuff",  OnexOneRibbedCuff.ConstructionId),
        //new(SockSections.Cuff,  "Cuff",  TwoxTwoRibbedCuff.ConstructionId),
        new(SockSections.Cuff,  "Cuff",  TwistedRibbedCuff.ConstructionId),

        new(SockSections.Leg,  "Leg",  StockinetteLeg.ConstructionId),

        //new(SockSections.Heel, "Heel", ShortRowHeel.ConstructionId),
        new(SockSections.Heel, "Heel", HeelFlap_Gusset.ConstructionId),

        new(SockSections.Foot, "Foot", StockinetteFoot.ConstructionId),

        //new(SockSections.Toe, "Toe", RoundToe.ConstructionId),
        //new(SockSections.Toe, "Toe", WedgeToe.ConstructionId),
        new(SockSections.Toe, "Toe", StarToe.ConstructionId),
        //new(SockSections.Toe, "Toe", ShortRowToe.ConstructionId),
    ];
}
