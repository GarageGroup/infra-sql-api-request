using System;

namespace GarageGroup.Infra;

public sealed record class DbCombinedQuery : IDbQuery
{
    public DbCombinedQuery()
    {
    }

    public DbCombinedQuery(FlatArray<IDbQuery> queries)
        =>
        Queries = queries;

    public FlatArray<IDbQuery> Queries { get; init; }

    public int? TimeoutInSeconds { get; init; }

    public string GetSqlQuery(SqlDialect dialect)
        =>
        this.BuildSqlQuery(dialect);

    public FlatArray<DbParameter> GetParameters()
        =>
        this.BuildParameters();
}