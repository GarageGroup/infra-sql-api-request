using System;

namespace GarageGroup.Infra;

public interface IDbFilter
{
    string GetFilterSqlQuery(SqlDialect dialect);

    FlatArray<DbParameter> GetFilterParameters();
}