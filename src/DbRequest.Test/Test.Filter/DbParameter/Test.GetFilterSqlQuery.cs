using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbParameterFilterTest
{
    [Theory]
    [InlineData(SqlDialect.PostgreSql, "PostgreSql")]
    [InlineData((SqlDialect)37, "37")]
    public static void GetFilterSqlQuery_DialectIsNotSupported_ExpectNotSupportedException(
        SqlDialect dialect, string expectedName)
    {
        var source = new DbParameterFilter("Id", DbFilterOperator.Equal, 1);
        var ex = Assert.Throws<NotSupportedException>(Test);

        Assert.Contains("FilterWithParameter", ex.Message, StringComparison.InvariantCulture);
        Assert.Contains(expectedName, ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            _ = source.GetFilterSqlQuery(dialect);
    }

    [Theory]
    [InlineData(SqlDialect.TransactSql)]
    public static void GetFilterSqlQuery_OperatorIsOutOfRange_ExpectArgumentOutOfRangeException(
        SqlDialect dialect)
    {
        const int @operator = -1;
        var source = new DbParameterFilter("Id", (DbFilterOperator)@operator, 15);

        var ex = Assert.Throws<ArgumentOutOfRangeException>(Test);
        Assert.Contains(@operator.ToString(), ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            source.GetFilterSqlQuery(dialect);
    }

    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_OperatorIsInRangeAndDialectIsSupported_ExpectCorrectSqlQuery(
        DbParameterFilter source, SqlDialect dialect, string expected)
    {
        var actual = source.GetFilterSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbParameterFilter, SqlDialect, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new("Id", DbFilterOperator.Equal, 1),
                SqlDialect.TransactSql,
                "Id = @Id"
            },
            {
                new("Id", DbFilterOperator.Equal, null),
                SqlDialect.TransactSql,
                "Id = @Id"
            },
            {
                new("a.id", DbFilterOperator.Equal, "Some value", "p.Id"),
                SqlDialect.TransactSql,
                "a.id = @p.Id"
            },
            {
                new("Price", DbFilterOperator.Greater, 104.51m, "Value"),
                SqlDialect.TransactSql,
                "Price > @Value"
            },
            {
                new("Name", DbFilterOperator.GreaterOrEqual, "Some string", string.Empty),
                SqlDialect.TransactSql,
                "Name >= @Name"
            },
            {
                new("Id", DbFilterOperator.Less, 11),
                SqlDialect.TransactSql,
                "Id < @Id"
            },
            {
                new("value", DbFilterOperator.LessOrEqual, null),
                SqlDialect.TransactSql,
                "value <= @value"
            },
            {
                new("Id", DbFilterOperator.Inequal, 1),
                SqlDialect.TransactSql,
                "Id <> @Id"
            }
        };
}