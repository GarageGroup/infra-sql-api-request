using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

/*partial class DbParameterArrayFilterTest
{
    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_OperatorIsInRange_ExpectCorrectSqlQuery(DbParameterArrayFilter source, string expected)
    {
        var actual = source.GetFilterSqlQuery();
        Assert.Equal(expected, actual);
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
            source.GetFilterSqlQuery();
    }

    public static TheoryData<DbParameterArrayFilter, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new("Id", DbArrayFilterOperator.In, default, "IdParam"),
                string.Empty
            },
            {
                new("Id", DbArrayFilterOperator.In, new(1, 2, 3), "IdParam"),
                "Id IN (@IdParam0, @IdParam1, @IdParam2)"
            },
            {
                new("Value", DbArrayFilterOperator.In, new("Some text", null)),
                "Value IN (@Value0, @Value1)"
            },
            {
                new("Price", DbArrayFilterOperator.In, new(string.Empty), string.Empty),
                "Price IN (@Price0)"
            },
            {
                new("Id", DbArrayFilterOperator.NotIn, default),
                string.Empty
            },
            {
                new("Id", DbArrayFilterOperator.NotIn, new(1, 2)),
                "Id NOT IN (@Id0, @Id1)"
            },
            {
                new("Price", DbArrayFilterOperator.NotIn, new(null, 1, 15), string.Empty),
                "Price NOT IN (@Price0, @Price1, @Price2)"
            },
            {
                new("Id", DbArrayFilterOperator.NotIn, new("Some value"), "IdParam"),
                "Id NOT IN (@IdParam0)"
            }
        };
}*/