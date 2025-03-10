using System;
using System.Text;

namespace GarageGroup.Infra;

partial class DbQueryExtensions
{
    internal static string BuildFilterTransactSqlQuery(this DbParameterFilter filter)
        =>
        new StringBuilder()
        .Append(filter.FieldName)
        .Append(' ')
        .Append(filter.Operator.GetSign())
        .Append(' ')
        .Append('@')
        .Append(filter.ParameterName)
        .ToString();

    internal static FlatArray<DbParameter> BuildFilterParameters(this DbParameterFilter filter)
        =>
        [
            new(filter.ParameterName, filter.FieldValue)
        ];
}