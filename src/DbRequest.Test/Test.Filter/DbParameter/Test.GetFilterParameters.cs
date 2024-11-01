using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbParameterFilterTest
{
    [Theory]
    [MemberData(nameof(FilterParametersTestData))]
    public static void GetFilterParameters_ExpectCorrectParameters(DbParameterFilter source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetFilterParameters();
        Assert.StrictEqual(expected, actual);
    }

    public static TheoryData<DbParameterFilter, FlatArray<DbParameter>> FilterParametersTestData
        =>
        new()
        {
            {
                new("Id", DbFilterOperator.Equal, 1),
                [
                    new("Id", 1)
                ]
            },
            {
                new("Id", DbFilterOperator.Greater, null),
                [
                    new("Id", null)
                ]
            },
            {
                new("id", DbFilterOperator.Equal, "Some value", "p.Id"),
                [
                    new("p.Id", "Some value")
                ]
            },
            {
                new("Name", DbFilterOperator.GreaterOrEqual, false, string.Empty),
                [
                    new("Name", false)
                ]
            },
            {
                new("value", (DbFilterOperator)(-1), 75.91m),
                [
                    new("value", 75.91m)
                ]
            }
        };
}