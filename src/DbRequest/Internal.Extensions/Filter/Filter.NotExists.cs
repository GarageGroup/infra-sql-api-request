using System;
using System.Text;

namespace GarageGroup.Infra;

partial class DbQueryExtensions
{
    internal static string BuildTransactSqlQuery(this DbNotExistsFilter filter)
        =>
        new StringBuilder(
            "NOT EXISTS (")
        .Append(
            filter.SelectQuery.GetSqlQuery(SqlDialect.TransactSql))
        .Append(
            ')')
        .ToString();

    internal static FlatArray<DbParameter> BuildParameters(this DbNotExistsFilter filter)
        =>
        filter.SelectQuery.GetParameters();
}