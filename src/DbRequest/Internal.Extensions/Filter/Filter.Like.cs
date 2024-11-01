using System;
using System.Text;

namespace GarageGroup.Infra;

partial class DbQueryExtensions
{
    internal static string BuildFilterSqlQuery(this DbLikeFilter filter)
        =>
        new StringBuilder()
        .Append(filter.FieldName)
        .Append(" LIKE '%' + @")
        .Append(filter.ParameterName)
        .Append(" + '%'")
        .ToString();

    internal static FlatArray<DbParameter> BuildFilterParameters(this DbLikeFilter filter)
        =>
        [
            new(filter.ParameterName, filter.FieldValue)
        ];
}