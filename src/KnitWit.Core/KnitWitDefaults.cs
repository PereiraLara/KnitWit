using KnitWit.Core.Constructions.Hat;
using KnitWit.Core.Constructions.Sock;
using KnitWit.Core.Constructions.Sock.Cuff;
using KnitWit.Core.Constructions.Sock.Foot;
using KnitWit.Core.Constructions.Sock.Heel;
using KnitWit.Core.Constructions.Sock.Leg;
using KnitWit.Core.Constructions.Sock.Toe;
using KnitWit.Core.Garments;
using KnitWit.Core.Patterns;
using KnitWit.Core.Registry;

namespace KnitWit.Core;

/// <summary>
/// The single place where garments and constructions are wired up.
/// Adding something new to the app = one new line here (plus the class itself).
/// </summary>
public static class KnitWitDefaults
{
    public static GarmentRegistry CreateGarments() => new GarmentRegistry()
    .Register(new HatGarment())
    .Register(new SockGarment());

    public static ConstructionRegistry CreateConstructions() => new ConstructionRegistry()
        .Register(new RibbedBrim())
        .Register(new StraightBody())
        .Register(new RadialDecreaseCrown())
        .Register(new OnexOneRibbedCuff())
        .Register(new TwoxTwoRibbedCuff())
        .Register(new TwistedRibbedCuff())
        .Register(new StockinetteLeg())
        .Register(new OnexOneRibbedLeg())
        .Register(new ShortRowHeel())
        .Register(new HeelFlap_Gusset())
        .Register(new StockinetteFoot())
        .Register(new OnexOneRibbedFoot())
        .Register(new RoundToe())
        .Register(new WedgeToe())
        .Register(new StarToe());

    public static PatternGenerator CreateGenerator() =>
        new(CreateGarments(), CreateConstructions());

    //    public static PatternGenerator CreateGenerator()
    //    {
    //        var garments = new GarmentRegistry()
    //            .Register(new HatGarment())
    //            .Register(new SockGarment());

    //        var constructions = new ConstructionRegistry()
    //            .Register(new RibbedBrim())
    //            .Register(new StraightBody())
    //            .Register(new RadialDecreaseCrown())
    //            .Register(new OnexOneRibbedCuff())
    //            .Register(new TwoxTwoRibbedCuff())
    //            .Register(new TwistedRibbedCuff())
    //            .Register(new StockinetteLeg())
    //            .Register(new OnexOneRibbedLeg())
    //            .Register(new ShortRowHeel())
    //            .Register(new HeelFlap_Gusset())
    //            .Register(new StockinetteFoot())
    //            .Register(new OnexOneRibbedFoot())
    //            .Register(new RoundToe())
    //            .Register(new WedgeToe())
    //            .Register(new StarToe())
    //            //.Register(new ShortRowToe())
    //            ;

    //        return new PatternGenerator(garments, constructions);
    //    }
}
