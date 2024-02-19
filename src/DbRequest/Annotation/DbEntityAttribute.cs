using System;

namespace GarageGroup.Infra;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class DbEntityAttribute(string? tableName = null, string? tableAlias = null) : Attribute
{
    public string? TableName { get; } = tableName;

    public string? TableAlias { get; } = tableAlias;
}