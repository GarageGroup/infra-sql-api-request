using System;

namespace GarageGroup.Infra;

public sealed record class DbSelectQuery : IDbQuery
{
    public DbSelectQuery(string tableName)
        =>
        TableName = tableName.OrEmpty();

    public DbSelectQuery(string tableName, string tableAlias)
    {
        TableName = tableName.OrEmpty();
        TableAlias = string.IsNullOrWhiteSpace(tableAlias) ? null : tableAlias;
    }

    public string TableName { get; }

    public string? TableAlias { get; }

    public int? Top { get; init; }

    public long? Offset { get; init; }

    public FlatArray<string> SelectedFields { get; init; }

    public FlatArray<string> GroupByFields { get; init; }

    public IDbFilter? Filter { get; init; }

    public FlatArray<DbJoinedTable> JoinedTables { get; init; }

    public FlatArray<DbAppliedTable> AppliedTables { get; init; }

    public FlatArray<DbOrder> Orders { get; init; }

    public int? TimeoutInSeconds { get; init; }

    public string GetSqlQuery(SqlDialect dialect)
        =>
        this.BuildTransactSqlQuery();

    public FlatArray<DbParameter> GetParameters()
        =>
        this.BuildParameters();
}