using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbDeleteQueryTest
{
    [Theory]
    [MemberData(nameof(ParametersTestData))]
    public static void GetFilterParameters_ExpectCorrectParameters(DbDeleteQuery source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetParameters();
        Assert.StrictEqual(expected, actual);
    }

    public static TheoryData<DbDeleteQuery, FlatArray<DbParameter>> ParametersTestData
        =>
        new()
        {
            {
                new(
                    tableName: "Country",
                    filter: new StubDbFilter("Id = 15")),
                default
            },
            {
                new(
                    tableName: "Country",
                    filter: new StubDbFilter("Price > 0", new("Price", 25), new("Id", null))),
                new(
                    new("Price", 25),
                    new("Id", null))
            }
        };
}