using System;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

internal sealed partial class StubDbQuery(string sqlQuery, params DbParameter[] parameters) : IDbQuery
{
    public int? TimeoutInSeconds { get; init; }

    public string GetSqlQuery()
        =>
        sqlQuery;

    public FlatArray<DbParameter> GetParameters()
        =>
        parameters;
}