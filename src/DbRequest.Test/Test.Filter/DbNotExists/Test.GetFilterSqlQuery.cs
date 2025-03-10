using System;
using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbNotExistsFilterTest
{
    [Theory]
    [InlineData(SqlDialect.PostgreSql, "PostgreSql")]
    [InlineData((SqlDialect)21, "21")]
    public static void GetFilterSqlQuery_DialectIsNotSupported_ExpectNotSupportedException(
        SqlDialect dialect, string expectedName)
    {
        var selectQuery = new DbSelectQuery("SomeTable")
        {
            SelectedFields = new("Id"),
            Filter = new StubDbFilter(
                queries: new Dictionary<SqlDialect, string>
                {
                    [dialect] = "Price > 0"
                },
                parameters:
                [
                    new("Price", 15)
                ])
        };

        var source = new DbNotExistsFilter(selectQuery);
        var ex = Assert.Throws<NotSupportedException>(Test);

        Assert.Contains("NOT EXISTS", ex.Message, StringComparison.InvariantCulture);
        Assert.Contains(expectedName, ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            _ = source.GetFilterSqlQuery(dialect);
    }

    [Theory]
    [InlineData(SqlDialect.TransactSql)]
    public static void GetFilterSqlQuery_DialectIsSupported_ExpectCorrectQuery(SqlDialect dialect)
    {
        var selectQuery = new DbSelectQuery("SomeTable")
        {
            SelectedFields = new("Id"),
            Filter = new StubDbFilter(
                queries: new Dictionary<SqlDialect, string>
                {
                    [dialect] = "Price > 0"
                },
                parameters: new DbParameter("Price", 15))
        };
        var source = new DbNotExistsFilter(selectQuery);

        var expected = $"NOT EXISTS ({selectQuery.GetSqlQuery(dialect)})";
        var actual = source.GetFilterSqlQuery(dialect);

        Assert.Equal(expected, actual);
    }
}