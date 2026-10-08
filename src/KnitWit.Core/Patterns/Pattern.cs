
namespace KnitWit.Core.Patterns;

public sealed record Pattern(string Title, IReadOnlyList<PatternSection> Sections)
{
}

public sealed record PatternSection(
    string SectionId,
    string Title,
    string ConstructionId,
    IReadOnlyList<string> Steps,
    int StitchesAfter
    );
