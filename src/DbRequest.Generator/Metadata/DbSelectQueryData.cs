using System.Collections.Generic;

namespace GarageGroup.Infra;

internal sealed record class DbSelectQueryData
{
    public DbSelectQueryData(
        string queryName,
        DbTableData tableData,
        IReadOnlyList<DbJoinData>? joinedTables,
        IReadOnlyList<string>? fieldNames,
        IReadOnlyList<string>? groupByFields)
    {
        QueryName = queryName ?? string.Empty;
        TableData = tableData;
        JoinedTables = joinedTables ?? [];
        FieldNames = fieldNames ?? [];
        GroupByFields = groupByFields ?? [];
    }

    public string QueryName { get; }

    public DbTableData TableData { get; }

    public IReadOnlyList<DbJoinData> JoinedTables { get; }

    public IReadOnlyList<string> FieldNames { get; }

    public IReadOnlyList<string> GroupByFields { get; }
}