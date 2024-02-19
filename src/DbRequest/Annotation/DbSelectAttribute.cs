using System;

namespace GarageGroup.Infra;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class DbSelectAttribute(string queryName, string? tableName = null, string? fieldName = null) : Attribute
{
    public string QueryName { get; } = queryName.OrEmpty();

    public string? TableName { get; set; } = tableName.OrNullIfEmpty();

    public string? FieldName { get; set; } = fieldName.OrNullIfEmpty();

    public bool GroupBy { get; set; }
}