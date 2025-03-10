using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbUpdateQueryTest
{
    [Theory]
    [MemberData(nameof(ParametersTestData))]
    public static void GetFilterParameters_ExpectCorrectParameters(DbUpdateQuery source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetParameters();
        Assert.StrictEqual(expected, actual);
    }

    public static TheoryData<DbUpdateQuery, FlatArray<DbParameter>> ParametersTestData
        =>
        new()
        {
            {
                new(
                    tableName: "Country",
                    fieldValues: default,
                    filter: new StubInvariantDbFilter("Price > 0", new DbParameter("SomeParam", 15))),
                [
                    new("SomeParam", 15)
                ]
            },
            {
                new(
                    tableName: "Country",
                    fieldValues:
                    [
                        new("Id", 15)
                    ],
                    filter: new StubInvariantDbFilter("Price > 0")),
                [
                    new("Id", 15)
                ]
            },
            {
                new(
                    tableName: "Country",
                    fieldValues:
                    [
                        new("Name", "Some value"),
                        new("Id", null, "Id1")
                    ],
                    filter: new StubInvariantDbFilter("Price > 0", new DbParameter("Price", 25))),
                [
                    new("Name", "Some value"),
                    new("Id1", null),
                    new("Price", 25)
                ]
            }
        };
}