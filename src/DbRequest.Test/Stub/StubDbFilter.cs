using System;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

internal sealed partial class StubDbFilter(string sqlQuery, params DbParameter[] parameters) : IDbFilter
{
    public string GetFilterSqlQuery()
        =>
        sqlQuery;

    public FlatArray<DbParameter> GetFilterParameters()
        =>
        parameters;
}