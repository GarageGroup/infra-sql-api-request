using System;
using System.Text;

namespace GarageGroup.Infra;

partial class DbQueryExtensions
{
    internal static string BuildSqlQuery(this DbCombinedQuery query)
    {
        if (query.Queries.IsEmpty)
        {
            return string.Empty;
        }

        var queryBuilder = new StringBuilder();

        foreach (var dbQuery in query.Queries)
        {
            queryBuilder = queryBuilder.AppendSeparator("\n").Append(dbQuery.GetSqlQuery());
        }

        return queryBuilder.ToString();
    }

    internal static FlatArray<DbParameter> BuildParameters(this DbCombinedQuery query)
    {
        return query.Queries.FlatMap(GetParameters);

        static FlatArray<DbParameter> GetParameters(IDbQuery dbQuery)
            =>
            dbQuery.GetParameters();
    }
}