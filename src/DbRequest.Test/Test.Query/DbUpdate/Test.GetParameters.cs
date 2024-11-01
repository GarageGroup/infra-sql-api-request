using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

/*partial class DbUpdateQueryTest
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
                    filter: new StubDbFilter("Price > 0", new DbParameter("SomeParam", 15))),
                new(
                    new DbParameter("SomeParam", 15))
            },
            {
                new(
                    tableName: "Country",
                    fieldValues: new DbFieldValue[]
                    {
                        new("Id", 15)
                    },
                    filter: new StubDbFilter("Price > 0")),
                new(
                    new DbParameter("Id", 15))
            },
            {
                new(
                    tableName: "Country",
                    fieldValues: new DbFieldValue[]
                    {
                        new("Name", "Some value"),
                        new("Id", null, "Id1")
                    },
                    filter: new StubDbFilter("Price > 0", new DbParameter("Price", 25))),
                new(
                    new("Name", "Some value"),
                    new("Id1", null),
                    new("Price", 25))
            }
        };
}*/