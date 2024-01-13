using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbFieldFilterTest
{
    [Theory]
    [MemberData(nameof(FilterParametersTestData))]
    public static void GetFilterParameters_ExpectDefault(DbFieldFilter source)
    {
        var actual = source.GetFilterParameters();
        var expected = default(FlatArray<DbParameter>);

        Assert.StrictEqual(expected, actual);
    }

    public static TheoryData<DbFieldFilter> FilterParametersTestData
        =>
        new()
        {
            {
                new("Id", DbFilterOperator.Equal, "73")
            },
            {
                new("Id", DbFilterOperator.Greater, string.Empty)
            },
            {
                new("Value", DbFilterOperator.Equal, "(SELECT COUNT(*) FROM Country)")
            },
            {
                new("Name", DbFilterOperator.Inequal, "true")
            },
            {
                new("value", (DbFilterOperator)(-3), "\"Some text\"")
            }
        };
}