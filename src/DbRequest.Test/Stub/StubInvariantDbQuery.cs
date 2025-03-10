using System;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

internal sealed class StubInvariantDbQuery(string query, params DbParameter[] parameters) : IDbQuery
{
    public int? TimeoutInSeconds { get; init; }

    public string GetSqlQuery(SqlDialect dialect)
        =>
        query;

    public FlatArray<DbParameter> GetParameters()
        =>
        parameters;
}