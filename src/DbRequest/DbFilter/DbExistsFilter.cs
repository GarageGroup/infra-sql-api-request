using System;

namespace GarageGroup.Infra;

public sealed record class DbExistsFilter : IDbFilter
{
    public DbExistsFilter(DbSelectQuery selectQuery)
        =>
        SelectQuery = selectQuery;

    public DbSelectQuery SelectQuery { get; }

    public string GetFilterSqlQuery(SqlDialect dialect)
        =>
        dialect switch
        {
            SqlDialect.TransactSql => this.BuildTransactSqlQuery(),
            _ => throw dialect.CreateNotSupportedException("EXISTS")
        };

    public FlatArray<DbParameter> GetFilterParameters()
        =>
        this.BuildParameters();
}