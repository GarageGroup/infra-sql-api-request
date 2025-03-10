using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbParameterArrayFilterTest
{
    [Theory]
    [InlineData(SqlDialect.PostgreSql, "PostgreSql")]
    [InlineData((SqlDialect)37, "37")]
    public static void GetFilterSqlQuery_DialectIsNotSupported_ExpectNotSupportedException(
        SqlDialect dialect, string expectedName)
    {
        var source = new DbParameterArrayFilter("Id", DbArrayFilterOperator.In, [1, 7], "IdParam");
        var ex = Assert.Throws<NotSupportedException>(Test);

        Assert.Contains("ArrayFilterWithParameter", ex.Message, StringComparison.InvariantCulture);
        Assert.Contains(expectedName, ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            _ = source.GetFilterSqlQuery(dialect);
    }

    [Fact]
    public static void GetFilterSqlQuery_OperatorIsOutOfRange_ExpectArgumentOutOfRangeException()
    {
        const int @operator = -1;
        var source = new DbParameterArrayFilter("Value", (DbArrayFilterOperator)@operator, new(1, 2, 3));

        var ex = Assert.Throws<ArgumentOutOfRangeException>(Test);
        Assert.Contains(@operator.ToString(), ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            _ = source.GetFilterSqlQuery(SqlDialect.TransactSql);
    }

    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_OperatorIsInRangeAndDialectIsSupported_ExpectCorrectSqlQuery(
        DbParameterArrayFilter source, SqlDialect dialect, string expected)
    {
        var actual = source.GetFilterSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbParameterArrayFilter, SqlDialect, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new("Id", DbArrayFilterOperator.In, default, "IdParam"),
                SqlDialect.TransactSql,
                string.Empty
            },
            {
                new("Id", DbArrayFilterOperator.In, [4, 7, 9], "IdParam"),
                SqlDialect.TransactSql,
                "Id IN (@IdParam0, @IdParam1, @IdParam2)"
            },
            {
                new("Value", DbArrayFilterOperator.In, new("Some text", null)),
                SqlDialect.TransactSql,
                "Value IN (@Value0, @Value1)"
            },
            {
                new("Price", DbArrayFilterOperator.In, [string.Empty], string.Empty),
                SqlDialect.TransactSql,
                "Price IN (@Price0)"
            },
            {
                new("Id", DbArrayFilterOperator.NotIn, default),
                SqlDialect.TransactSql,
                string.Empty
            },
            {
                new("Id", DbArrayFilterOperator.NotIn, new(1, 2)),
                SqlDialect.TransactSql,
                "Id NOT IN (@Id0, @Id1)"
            },
            {
                new("Price", DbArrayFilterOperator.NotIn, new(null, 1, 15), string.Empty),
                SqlDialect.TransactSql,
                "Price NOT IN (@Price0, @Price1, @Price2)"
            },
            {
                new("Id", DbArrayFilterOperator.NotIn, new("Some value"), "IdParam"),
                SqlDialect.TransactSql,
                "Id NOT IN (@IdParam0)"
            }
        };
}