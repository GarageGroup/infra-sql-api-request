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
        [
            new DbFieldFilter("Id", DbFilterOperator.Equal, "73"),
            new DbFieldFilter("Id", DbFilterOperator.Greater, string.Empty),
            new DbFieldFilter("Value", DbFilterOperator.Equal, "(SELECT COUNT(*) FROM Country)"),
            new DbFieldFilter("Name", DbFilterOperator.Inequal, "true"),
            new DbFieldFilter("value", (DbFilterOperator)(-3), "\"Some text\"")
        ];
}