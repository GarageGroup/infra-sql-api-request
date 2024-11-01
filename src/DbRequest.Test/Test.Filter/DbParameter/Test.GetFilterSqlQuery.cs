using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

/*partial class DbParameterFilterTest
{
    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_OperatorIsInRange_ExpectCorrectSqlQuery(DbParameterFilter source, string expected)
    {
        var actual = source.GetFilterSqlQuery();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public static void GetFilterSqlQuery_OperatorIsOutOfRange_ExpectArgumentOutOfRangeException()
    {
        const int @operator = -1;
        var source = new DbParameterFilter("Id", (DbFilterOperator)@operator, 15);

        var ex = Assert.Throws<ArgumentOutOfRangeException>(Test);
        Assert.Contains(@operator.ToString(), ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            source.GetFilterSqlQuery();
    }

    public static TheoryData<DbParameterFilter, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new("Id", DbFilterOperator.Equal, 1),
                "Id = @Id"
            },
            {
                new("Id", DbFilterOperator.Equal, null),
                "Id = @Id"
            },
            {
                new("a.id", DbFilterOperator.Equal, "Some value", "p.Id"),
                "a.id = @p.Id"
            },
            {
                new("Price", DbFilterOperator.Greater, 104.51m, "Value"),
                "Price > @Value"
            },
            {
                new("Name", DbFilterOperator.GreaterOrEqual, "Some string", string.Empty),
                "Name >= @Name"
            },
            {
                new("Id", DbFilterOperator.Less, 11),
                "Id < @Id"
            },
            {
                new("value", DbFilterOperator.LessOrEqual, null),
                "value <= @value"
            },
            {
                new("Id", DbFilterOperator.Inequal, 1),
                "Id <> @Id"
            }
        };
}*/