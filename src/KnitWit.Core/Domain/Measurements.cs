namespace KnitWit.Core.Domain;

/// <summary>Well-known measurement keys. Add new ones here as garments need them.</summary>
public static class MeasurementKeys
{
    public const string HeadCircumference = "head-circumference";
    public const string BrimHeight = "brim-height";
    public const string BodyHeight = "body-height";
    public const string FootCircumference = "foot-circumference";
    public const string FootLength = "foot-length";
    public const string FootLengthDefault = "foot-circumference";
    //public const string FootLengthDefault = FootCircumference;
    public const string CuffHeight = "cuff-height";

    // If you want a default value for cuff height (in cm  ), use a separate constant:
    public const double CuffHeightDefaultCm = 4;
    public const double ToeLengthDefaultCm = 4.5;
    public const string ToeLength = "toe-length";

    public const string LegHeight = "leg-height";
}

/// <summary>A bag of named measurements in centimetres. Constructions declare which keys they need.</summary>
public sealed class Measurements
{
    private readonly Dictionary<string, double> _values;

    public Measurements(IDictionary<string, double> values) =>
        _values = new Dictionary<string, double>(values);

    public static Measurements Of(params (string Key, double Cm)[] values) =>
        new(values.ToDictionary(v => v.Key, v => v.Cm));

    public bool Has(string key) => _values.ContainsKey(key);

    public double Get(string key) =>
        _values.TryGetValue(key, out var cm)
            ? cm
            : throw new KeyNotFoundException($"Missing measurement '{key}'.");
    public double GetOrDefault(string key, double defaultCm) =>
        _values.TryGetValue(key, out var cm) ? cm : defaultCm;
}

public static class MeasurementsExtensions
{
    /// <summary>User-supplied toe length, or the default. Toe and foot must both use this.</summary>
    public static double ToeLength(this Measurements m) =>
        m.GetOrDefault(MeasurementKeys.ToeLength, MeasurementKeys.ToeLengthDefaultCm);
    public static double FootLength(this Measurements m) =>
        m.GetOrDefault(MeasurementKeys.FootLength, MeasurementKeys.FootLengthDefault);
}
