using KnitWit.Core.Domain;
using KnitWit.Core.Garments;

namespace KnitWit.Core.Registry;

public sealed class GarmentRegistry
{
    private readonly Dictionary<GarmentType, IGarment> _garments = new();

    public GarmentRegistry Register(IGarment garment)
    {
        _garments[garment.Type] = garment;
        return this;
    }

    public IGarment Get(GarmentType type) =>
        _garments.TryGetValue(type, out var g)
            ? g
            : throw new InvalidOperationException($"No garment registered for {type}.");

    public IEnumerable<IGarment> All => _garments.Values;
}
