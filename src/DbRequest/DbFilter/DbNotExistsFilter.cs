using System;

namespace GarageGroup.Infra;

public sealed record class DbNotExistsFilter : IDbFilter
{
    public DbNotExistsFilter(DbSelectQuery selectQuery)
        =>
        SelectQuery = selectQuery;

    public DbSelectQuery SelectQuery { get; }

    public string GetFilterSqlQuery(SqlDialect dialect)
        =>
        this.BuildTransactSqlQuery();

    public FlatArray<DbParameter> GetFilterParameters()
        =>
        this.BuildParameters();
}