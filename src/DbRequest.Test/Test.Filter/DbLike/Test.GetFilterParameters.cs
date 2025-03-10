using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbLikeFilterTest
{
    [Theory]
    [MemberData(nameof(FilterParametersTestData))]
    public static void GetFilterParameters_ExpectCorrectParameters(DbLikeFilter source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetFilterParameters();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbLikeFilter, FlatArray<DbParameter>> FilterParametersTestData
        =>
        new()
        {
            {
                new("LOWER(p.Name)", "TeSt", "Search"),
                [
                    new("Search", "TeSt")
                ]
            },
            {
                new("p.Name", null, "Name"),
                [
                    new("Name", null)
                ]
            },
            {
                new("Description", "\t\n", "DescriptionParameter"),
                [
                    new("DescriptionParameter", "\t\n")
                ]
            }
        };
}