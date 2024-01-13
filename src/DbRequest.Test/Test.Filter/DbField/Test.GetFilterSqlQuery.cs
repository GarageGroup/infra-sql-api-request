using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbFieldFilterTest
{
    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_OperatorIsInRange_ExpectCorrectSqlQuery(DbFieldFilter source, string expected)
    {
        var actual = source.GetFilterSqlQuery();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public static void GetFilterSqlQuery_OperatorIsOutOfRange_ExpectArgumentOutOfRangeException()
    {
        const int @operator = -7;
        var source = new DbFieldFilter("Value", (DbFilterOperator)@operator, "25");

        var ex = Assert.Throws<ArgumentOutOfRangeException>(Test);
        Assert.Contains(@operator.ToString(), ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            source.GetFilterSqlQuery();
    }

    public static TheoryData<DbFieldFilter, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new("Id", DbFilterOperator.Equal, "1"),
                "Id = 1"
            },
            {
                new("Id", DbFilterOperator.Greater, "\"Some value\""),
                "Id > \"Some value\""
            },
            {
                new("p.id", DbFilterOperator.GreaterOrEqual, "75.34"),
                "p.id >= 75.34"
            },
            {
                new("Name", DbFilterOperator.GreaterOrEqual, string.Empty),
                "Name >= null"
            },
            {
                new("Id", DbFilterOperator.Less, null!),
                "Id < null"
            },
            {
                new("value", DbFilterOperator.LessOrEqual, "(SELECT COUNT(*) FROM Country)"),
                "value <= (SELECT COUNT(*) FROM Country)"
            },
            {
                new("Id", DbFilterOperator.Inequal, "\t"),
                "Id <> null"
            }
        };
}