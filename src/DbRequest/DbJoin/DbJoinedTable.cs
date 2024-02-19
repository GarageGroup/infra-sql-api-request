using System;

namespace GarageGroup.Infra;

public sealed record class DbJoinedTable
{
    public DbJoinedTable(DbJoinType type, string tableName, string tableAlias, IDbFilter filter)
    {
        Type = type;
        TableName = tableName.OrEmpty();
        TableAlias = tableAlias.OrEmpty();
        Filter = filter;
    }

    public DbJoinedTable(DbJoinType type, string tableName, string tableAlias, string rawFilter)
        : this(type, tableName, tableAlias, new DbRawFilter(rawFilter))
    {
    }

    public DbJoinType Type { get; }

    public string TableName { get; }

    public string? TableAlias { get; }

    public IDbFilter Filter { get; }
}