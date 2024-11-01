using System;
using System.Collections.Generic;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

internal sealed partial class StubDbFilter(IReadOnlyDictionary<SqlDialect, string> queries, params DbParameter[] parameters) : IDbFilter
{
    public string GetFilterSqlQuery(SqlDialect dialect)
        =>
        queries[dialect];

    public FlatArray<DbParameter> GetFilterParameters()
        =>
        parameters;
}