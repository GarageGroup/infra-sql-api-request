using System;
using System.Linq;
using System.Text;

namespace GarageGroup.Infra;

partial class DbQueryExtensions
{
    internal static string BuildFilterTransactSqlQuery(this DbParameterArrayFilter filter)
    {
        if (filter.FieldValues.IsEmpty)
        {
            return string.Empty;
        }

        return new StringBuilder()
            .Append(
                filter.FieldName)
            .Append(
                ' ')
            .Append(
                filter.Operator.GetSign())
            .Append(
                ' ')
            .Append(
                '(')
            .AppendJoined(
                Enumerable.Range(0, filter.FieldValues.Length).Select(GetParameterName))
            .Append(
                ')')
            .ToString();

        string GetParameterName(int index)
            =>
            '@' + filter.ParameterPrefix + index;
    }

    internal static FlatArray<DbParameter> BuildFilterParameters(this DbParameterArrayFilter filter)
    {
        if (filter.FieldValues.IsEmpty)
        {
            return default;
        }

        var dbParameters = FlatArray<DbParameter>.Builder.OfLength(filter.FieldValues.Length);

        for (var index = 0; index < filter.FieldValues.Length; index++)
        {
            dbParameters[index] = new(filter.ParameterPrefix + index, filter.FieldValues[index]);
        }

        return dbParameters.MoveToFlatArray();
    }
}