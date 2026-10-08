namespace KnitWit.Core.Domain;

/// <summary>Swatch gauge, expressed per 10 cm (the usual yarn-label convention).</summary>
public readonly record struct Gauge(double StitchesPer10Cm, double RowsPer10Cm)
{
    public double StitchesPerCm => StitchesPer10Cm / 10.0;
    public double RowsPerCm => RowsPer10Cm / 10.0;

    public int StitchesFor(double cm) => (int)Math.Round(cm * StitchesPerCm);
    public int RowsFor(double cm) => (int)Math.Round(cm * RowsPerCm);
}
