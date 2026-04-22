using System.Collections.Generic;
using System.Linq;
using PrimeFuncPack;

namespace GarageGroup.Infra;

internal static class SourceBuilderCompatibility
{
    internal static SourceBuilder AppendCodeLine(this SourceBuilder sourceBuilder, params string[] lines)
        =>
        sourceBuilder.AppendCodeLines(lines);

    internal static SourceBuilder AddUsings(this SourceBuilder sourceBuilder, IEnumerable<string> namespaces)
        =>
        sourceBuilder.AddUsing(namespaces.ToArray());

    internal static SourceBuilder AddUsing(this SourceBuilder sourceBuilder, params string[] namespaces)
        =>
        sourceBuilder.AddUsing(namespaces);

    internal static SourceBuilder EndCodeBlock(this SourceBuilder sourceBuilder, char finalSymbol)
        =>
        sourceBuilder.EndCodeBlock(finalSymbol.ToString());

    internal static SourceBuilder EndCollectionExpression(this SourceBuilder sourceBuilder, char finalSymbol)
        =>
        sourceBuilder.EndCollectionExpression(finalSymbol.ToString());
}
