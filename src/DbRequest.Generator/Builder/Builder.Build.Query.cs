using System.Linq;
using System.Text;

namespace GarageGroup.Infra;

partial class DbEntityBuilder
{
    internal static string BuildQuerySourceCode(this DbEntityMetadata metadata)
        =>
        new SourceBuilder(
            metadata.EntityType.DisplayedData.AllNamespaces.FirstOrDefault())
        .AddUsings(
            metadata.EntityType.DisplayedData.AllNamespaces)
        .AddUsing(
            "System",
            "GarageGroup.Infra")
        .AppendCodeLine(
            metadata.BuildHeaderLine())
        .BeginCodeBlock()
        .AppendQueries(metadata)
        .EndCodeBlock()
        .AppendEmptyLine()
        .AppendCodeLine(
            $"file static class {InnerQueryBuilderClassName}")
        .AppendInnerQueryBuilderBody(metadata)
        .Build();

    private static SourceBuilder AppendQueries(this SourceBuilder builder, DbEntityMetadata metadata)
    {
        if (metadata.SelectQueries.Any() is false)
        {
            return builder;
        }

        var codeLines = metadata.SelectQueries.Select(BuildCodeLine).ToArray();
        for (var i = 0; i < codeLines.Length; i++)
        {
            builder = builder.AppendCodeLine(codeLines[i]);

            if (i != codeLines.Length - 1)
            {
                builder = builder.AppendEmptyLine();
            }
        }

        return builder;

        static string BuildCodeLine(DbSelectQueryData query)
            =>
            new StringBuilder("internal static DbSelectQuery ")
            .Append(query.QueryName)
            .Append(" { get; } = ")
            .Append(InnerQueryBuilderClassName)
            .Append('.')
            .Append(
                query.GetQueryBuildMethodName())
            .Append("();")
            .ToString();
    }

    private static SourceBuilder AppendInnerQueryBuilderBody(this SourceBuilder builder, DbEntityMetadata metadata)
    {
        builder = builder.BeginCodeBlock();

        for (var i = 0; i < metadata.SelectQueries.Count; i++)
        {
            if (i > 0)
            {
                builder = builder.AppendEmptyLine();
            }

            var query = metadata.SelectQueries[i];
            builder = builder.AppendQueryBuildMethod(query);
        }

        return builder.EndCodeBlock();
    }

    private static SourceBuilder AppendQueryBuildMethod(this SourceBuilder builder, DbSelectQueryData queryData)
    {
        return builder = builder
            .AppendCodeLine($"public static DbSelectQuery {queryData.GetQueryBuildMethodName()}()")
            .BeginLambda()
            .AppendCodeLine($"new({queryData.BuildDbSelectQueryArguments()})")
            .BeginCodeBlock()
            .AppendSelectQueryPropertyInitialization("SelectedFields", queryData.FieldNames.Select(InnerAsStringSourceCode).ToArray(), true)
            .AppendSelectQueryPropertyInitialization("JoinedTables", queryData.JoinedTables.Select(InnerBuildSourceCode).ToArray(), false)
            .AppendSelectQueryPropertyInitialization("GroupByFields", queryData.GroupByFields.Select(InnerAsStringSourceCode).ToArray(), true)
            .EndCodeBlock(';')
            .EndLambda();

        static string InnerAsStringSourceCode(string fieldName)
            =>
            fieldName.AsStringSourceCodeOrStringEmpty();

        string InnerBuildSourceCode(DbJoinData dbJoinData)
            =>
            dbJoinData.BuildDbJoinedTableSourceCode(withTypeName: queryData.JoinedTables.Count is 1);
    }

    private static SourceBuilder AppendSelectQueryPropertyInitialization(
        this SourceBuilder builder, string propertyName, string[] codeLines, bool isStringType)
    {
        if (codeLines.Length is 0)
        {
            return builder;
        }

        if (codeLines.Length is 1)
        {
            return isStringType switch
            {
                true => builder.AppendCodeLine($"{propertyName} = new({codeLines[0]}),"),
                _ => builder.AppendCodeLine($"{propertyName} = {codeLines[0]}.AsFlatArray(),")
            };
        }

        var lastLineIndex = codeLines.Length - 1;

        if (codeLines.Length <= 16)
        {
            return builder.AppendSelectQueryPropertyInitializationWithFlatArrayConstructor(propertyName, codeLines);
        }

        builder = builder
            .AppendDirective("#if NET8_0_OR_GREATER")
            .AppendCodeLine($"{propertyName} =")
            .BeginCollectionExpression();

        for (var i = 0; i < lastLineIndex; i++)
        {
            builder = builder.AppendCodeLine(codeLines[i] + ",");
        }

        return  builder
            .AppendCodeLine(codeLines[lastLineIndex])
            .EndCollectionExpression(',')
            .AppendDirective("#else")
            .AppendSelectQueryPropertyInitializationWithFlatArrayConstructor(propertyName, codeLines)
            .AppendDirective("#endif");
    }

    private static SourceBuilder AppendSelectQueryPropertyInitializationWithFlatArrayConstructor(
        this SourceBuilder builder, string propertyName, string[] codeLines)
    {
        var lastLineIndex = codeLines.Length - 1;
        builder = builder.AppendCodeLine($"{propertyName} = new(").BeginArguments();

        for (var i = 0; i < codeLines.Length; i++)
        {
            var lastSymbol = i < lastLineIndex ? "," : "),";
            builder = builder.AppendCodeLine(codeLines[i] + lastSymbol);
        }

        return builder.EndArguments();
    }

    private static string BuildDbJoinedTableSourceCode(this DbJoinData dbJoinData, bool withTypeName)
    {
        var builder = withTypeName ? new StringBuilder("new DbJoinedTable(") : new StringBuilder("new(");

        builder = dbJoinData.JoinType switch
        {
            0 => builder.Append("DbJoinType.Inner"),
            1 => builder.Append("DbJoinType.Left"),
            2 => builder.Append("DbJoinType.Right"),
            var joinType => builder.Append($"(DbJoinType){joinType}")
        };

        return builder
            .Append(", ")
            .Append(dbJoinData.TableName.AsStringSourceCodeOrStringEmpty())
            .Append(", ")
            .Append(dbJoinData.TableAlias.AsStringSourceCodeOrStringEmpty())
            .Append(", ")
            .Append(dbJoinData.RawFilter.AsStringSourceCodeOrStringEmpty())
            .Append(')')
            .ToString();
    }

    private static string BuildDbSelectQueryArguments(this DbSelectQueryData queryData)
    {
        var builder = new StringBuilder(queryData.TableData.TableName.AsStringSourceCodeOrStringEmpty());
        if (string.IsNullOrEmpty(queryData.TableData.TableAlias))
        {
            return builder.ToString();
        }

        return builder.Append(", ").Append(queryData.TableData.TableAlias.AsStringSourceCodeOrStringEmpty()).ToString();
    }
}