using System;

namespace GarageGroup.Infra;

public sealed record class DbUpdateQuery : IDbQuery
{
    public DbUpdateQuery(string tableName, FlatArray<DbFieldValue> fieldValues, IDbFilter filter)
    {
        TableName = tableName.OrEmpty();
        FieldValues = fieldValues;
        Filter = filter;
    }

    public string TableName { get; }

    public FlatArray<DbFieldValue> FieldValues { get; }

    public IDbFilter Filter { get; }

    public int? TimeoutInSeconds { get; init; }

    public string GetSqlQuery(SqlDialect dialect)
        =>
        dialect switch
        {
            SqlDialect.TransactSql => this.BuildTransactSqlQuery(),
            _ => throw dialect.CreateNotSupportedException("UPDATE")
        };

    public FlatArray<DbParameter> GetParameters()
        =>
        this.BuildParameters();
}