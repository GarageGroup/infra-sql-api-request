using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbFieldFilterTest
{
    [Theory]
    [InlineData(SqlDialect.TransactSql)]
    [InlineData(SqlDialect.PostgreSql)]
    public static void GetFilterSqlQuery_OperatorIsOutOfRange_ExpectArgumentOutOfRangeException(SqlDialect dialect)
    {
        const int @operator = -7;
        var source = new DbFieldFilter("Value", (DbFilterOperator)@operator, "25");

        var ex = Assert.Throws<ArgumentOutOfRangeException>(Test);
        Assert.Contains(@operator.ToString(), ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            source.GetFilterSqlQuery(dialect);
    }

    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_OperatorIsInRange_ExpectCorrectSqlQuery(
        DbFieldFilter source, SqlDialect dialect, string expected)
    {
        var actual = source.GetFilterSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbFieldFilter, SqlDialect, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new("Id", DbFilterOperator.Equal, "1"),
                SqlDialect.TransactSql,
                "Id = 1"
            },
            {
                new("Id", DbFilterOperator.Greater, "\"Some value\""),
                (SqlDialect)19023,
                "Id > \"Some value\""
            },
            {
                new("p.id", DbFilterOperator.GreaterOrEqual, "75.34"),
                SqlDialect.PostgreSql,
                "p.id >= 75.34"
            },
            {
                new("Name", DbFilterOperator.GreaterOrEqual, string.Empty),
                SqlDialect.TransactSql,
                "Name >= null"
            },
            {
                new("Id", DbFilterOperator.Less, null!),
                SqlDialect.PostgreSql,
                "Id < null"
            },
            {
                new("value", DbFilterOperator.LessOrEqual, "(SELECT COUNT(*) FROM Country)"),
                SqlDialect.TransactSql,
                "value <= (SELECT COUNT(*) FROM Country)"
            },
            {
                new("Id", DbFilterOperator.Inequal, "\t"),
                SqlDialect.TransactSql,
                "Id <> null"
            }
        };
}