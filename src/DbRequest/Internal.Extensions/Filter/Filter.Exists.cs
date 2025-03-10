using System;
using System.Text;

namespace GarageGroup.Infra;

partial class DbQueryExtensions
{
    internal static string BuildTransactSqlQuery(this DbExistsFilter filter)
        =>
        new StringBuilder(
            "EXISTS (")
        .Append(
            filter.SelectQuery.GetSqlQuery(SqlDialect.TransactSql))
        .Append(
            ')')
        .ToString();

    internal static FlatArray<DbParameter> BuildParameters(this DbExistsFilter filter)
        =>
        filter.SelectQuery.GetParameters();
}