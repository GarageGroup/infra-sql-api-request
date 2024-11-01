using System;
using System.Collections.Generic;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

internal sealed partial class StubDbQuery(IReadOnlyDictionary<SqlDialect, string> queries, params DbParameter[] parameters) : IDbQuery
{
    public int? TimeoutInSeconds { get; init; }

    public string GetSqlQuery(SqlDialect dialect)
        =>
        queries[dialect];

    public FlatArray<DbParameter> GetParameters()
        =>
        parameters;
}