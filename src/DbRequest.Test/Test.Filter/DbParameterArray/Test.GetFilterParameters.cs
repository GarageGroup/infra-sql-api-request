using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbParameterArrayFilterTest
{
    [Theory]
    [MemberData(nameof(FilterParametersTestData))]
    public static void GetFilterParameters_ExpectCorrectParameters(DbParameterArrayFilter source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetFilterParameters();
        Assert.StrictEqual(expected, actual);
    }

    public static TheoryData<DbParameterArrayFilter, FlatArray<DbParameter>> FilterParametersTestData
        =>
        new()
        {
            {
                new("Id", DbArrayFilterOperator.In, default),
                default
            },
            {
                new("Id", DbArrayFilterOperator.In, new(1, 2, null), "IdParam"),
                new(
                    new("IdParam0", 1),
                    new("IdParam1", 2),
                    new("IdParam2", null))
            },
            {
                new("Price", DbArrayFilterOperator.NotIn, default, "PriceParam"),
                default
            },
            {
                new("Value", (DbArrayFilterOperator)(-1), new(-7, null, "Some text"), string.Empty),
                new(
                    new("Value0", -7),
                    new("Value1", null),
                    new("Value2", "Some text"))
            }
        };
}