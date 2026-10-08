namespace KnitWit.Core.Constructions;

public static class StitchMath
{
    /// <summary>Round a stitch count to the nearest multiple (minimum one multiple).</summary>
    public static int RoundToMultiple(int stitches, int multiple) =>
        Math.Max(multiple, (int)Math.Round(stitches / (double)multiple) * multiple);
}
