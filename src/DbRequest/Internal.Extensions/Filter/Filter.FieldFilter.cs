namespace GarageGroup.Infra;

partial class DbQueryExtensions
{
    internal static string BuildFilterSqlQuery(this DbFieldFilter filter)
        =>
        $"{filter.FieldName} {filter.Operator.GetSign()} {filter.RawFieldValue}";
}