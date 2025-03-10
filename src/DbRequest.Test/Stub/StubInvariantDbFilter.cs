using System;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

internal sealed class StubInvariantDbFilter(string query, params DbParameter[] parameters) : IDbFilter
{
    public string GetFilterSqlQuery(SqlDialect dialect)
        =>
        query;

    public FlatArray<DbParameter> GetFilterParameters()
        =>
        parameters;
}
