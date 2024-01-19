using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbRawFilterTest
{
    [Theory]
    [MemberData(nameof(FilterParametersTestData))]
    public static void GetFilterParameters_ExpectCorrectParameters(DbRawFilter source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetFilterParameters();
        Assert.StrictEqual(expected, actual);
    }

    public static TheoryData<DbRawFilter, FlatArray<DbParameter>> FilterParametersTestData
        =>
        new()
        {
            {
                new(null!),
                default
            },
            {
                new("SELECT * FROM Product"),
                default
            },
            {
                new("SELECT * FROM Product")
                {
                    Parameters = new DbParameter[]
                    {
                        new("Param1", "Some value"),
                        new("Param2", 27)
                    }
                },
                new(
                    new("Param1", "Some value"),
                    new("Param2", 27))
            }
        };
}