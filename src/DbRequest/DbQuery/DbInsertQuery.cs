using System;

namespace GarageGroup.Infra;

public sealed record class DbInsertQuery : IDbQuery
{
    public DbInsertQuery(string tableName, FlatArray<DbFieldValue> fieldValues)
    {
        TableName = tableName.OrEmpty();
        FieldValues = fieldValues;
    }

    public string TableName { get; }

    public FlatArray<DbFieldValue> FieldValues { get; }

    public int? TimeoutInSeconds { get; init; }

    public string GetSqlQuery(SqlDialect dialect)
        =>
        dialect switch
        {
            SqlDialect.TransactSql => this.BuildTransactSqlQuery(),
            _ => throw dialect.CreateNotSupportedException("INSERT")
        };

    public FlatArray<DbParameter> GetParameters()
        =>
        this.BuildParameters();
}