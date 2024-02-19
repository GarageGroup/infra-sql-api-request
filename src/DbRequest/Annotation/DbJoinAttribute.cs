using System;

namespace GarageGroup.Infra;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class DbJoinAttribute(DbJoinType type, string tableName, string? tableAlias, string rawFilter) : Attribute
{
    public DbJoinType Type { get; } = type;

    public string TableName { get; } = tableName.OrEmpty();

    public string? TableAlias { get; } = tableAlias.OrNullIfEmpty();

    public string RawFilter { get; } = rawFilter.OrEmpty();
}