using System;

namespace GarageGroup.Infra;

public sealed record class DbParameterArrayFilter : IDbFilter
{
    public DbParameterArrayFilter(string fieldName, DbArrayFilterOperator @operator, FlatArray<object?> fieldValues)
    {
        FieldName = fieldName.OrEmpty();
        Operator = @operator;
        FieldValues = fieldValues;
        ParameterPrefix = FieldName;
    }

    public DbParameterArrayFilter(string fieldName, DbArrayFilterOperator @operator, FlatArray<object?> fieldValues, string parameterPrefix)
    {
        FieldName = fieldName.OrEmpty();
        Operator = @operator;
        FieldValues = fieldValues;
        ParameterPrefix = string.IsNullOrEmpty(parameterPrefix) ? FieldName : parameterPrefix;
    }

    public string FieldName { get; }

    public DbArrayFilterOperator Operator { get; }

    public FlatArray<object?> FieldValues { get; }

    public string ParameterPrefix { get; }

    public string GetFilterSqlQuery(SqlDialect dialect)
        =>
        dialect switch
        {
            SqlDialect.TransactSql => this.BuildFilterTransactSqlQuery(),
            _ => throw dialect.CreateNotSupportedException("ArrayFilterWithParameter")
        };

    public FlatArray<DbParameter> GetFilterParameters()
        =>
        this.BuildFilterParameters();
}