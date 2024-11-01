using System;
using System.Diagnostics.CodeAnalysis;

namespace GarageGroup.Infra;

public sealed record class DbCombinedFilter : IDbFilter
{
    public DbCombinedFilter(DbLogicalOperator @operator)
        =>
        Operator = @operator;

    [SetsRequiredMembers]
    public DbCombinedFilter(DbLogicalOperator @operator, FlatArray<IDbFilter> filters)
    {
        Operator = @operator;
        Filters = filters;
    }

    public DbLogicalOperator Operator { get; }

    public required FlatArray<IDbFilter> Filters { get; init; }

    public string GetFilterSqlQuery(SqlDialect dialect)
        =>
        this.BuildFilterSqlQuery(dialect);

    public FlatArray<DbParameter> GetFilterParameters()
        =>
        this.BuildFilterParameters();
}