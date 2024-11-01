using System;

namespace GarageGroup.Infra;

partial class DbQueryExtensions
{
    internal static string BuildFilterTransactSqlQuery(this DbLikeFilter filter)
        =>
        $"{filter.FieldName} LIKE '%' + @{filter.ParameterName} + '%'";

    internal static string BuildFilterPostgreSqlQuery(this DbLikeFilter filter)
        =>
        $"{filter.FieldName} LIKE '%' || @{filter.ParameterName} || '%'";

    internal static FlatArray<DbParameter> BuildFilterParameters(this DbLikeFilter filter)
        =>
        new DbParameter(filter.ParameterName, filter.FieldValue).AsFlatArray();
}