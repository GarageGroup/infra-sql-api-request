using System;

namespace GarageGroup.Infra;

public sealed record class DbDeleteQuery : IDbQuery
{
    public DbDeleteQuery(string tableName, IDbFilter filter)
    {
        TableName = tableName.OrEmpty();
        Filter = filter;
    }

    public string TableName { get; }

    public IDbFilter Filter { get; }

    public int? TimeoutInSeconds { get; init; }

    public string GetSqlQuery(SqlDialect dialect)
        =>
        dialect switch
        {
            SqlDialect.TransactSql => this.BuildTransactSqlQuery(),
            _ => throw dialect.CreateNotSupportedException("DELETE")
        };

    public FlatArray<DbParameter> GetParameters()
        =>
        this.BuildParameters();
}