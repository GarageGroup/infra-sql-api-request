using System.Collections.Generic;

namespace GarageGroup.Infra;

internal sealed record class DisplayedMethodData
{
    public DisplayedMethodData(IReadOnlyCollection<string> allNamespaces, string sourceCode)
    {
        AllNamespaces = allNamespaces ?? [];
        SourceCode = sourceCode ?? string.Empty;
    }

    public IReadOnlyCollection<string> AllNamespaces { get; }

    public string SourceCode { get; }
}