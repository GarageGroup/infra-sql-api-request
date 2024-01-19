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
                new DbParameter("Id", 1).AsFlatArray()
            },
            {
                new("Id", DbFilterOperator.Greater, null),
                new(
                    new DbParameter("Id", null))
            },
            {
                new("id", DbFilterOperator.Equal, "Some value", "p.Id"),
                new DbParameter("p.Id", "Some value").AsFlatArray()
            },
            {
                new("Name", DbFilterOperator.GreaterOrEqual, false, string.Empty),
                new DbParameter("Name", false).AsFlatArray()
            },
            {
                new("value", (DbFilterOperator)(-1), 75.91m),
                new DbParameter("value", 75.91m).AsFlatArray()
            }
        };
}