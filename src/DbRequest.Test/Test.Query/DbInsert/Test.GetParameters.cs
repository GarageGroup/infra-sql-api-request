using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbInsertQueryTest
{
    [Theory]
    [MemberData(nameof(ParametersTestData))]
    public static void GetFilterParameters_ExpectCorrectParameters(DbInsertQuery source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetParameters();
        Assert.StrictEqual(expected, actual);
    }

    public static TheoryData<DbInsertQuery, FlatArray<DbParameter>> ParametersTestData
        =>
        new()
        {
            {
                new(
                    tableName: "Country",
                    fieldValues: default),
                default
            },
            {
                new(
                    tableName: "Country",
                    fieldValues: new DbFieldValue[]
                    {
                        new("Id", 15)
                    }),
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
                    }),
                new(
                    new("Name", "Some value"),
                    new("Id1", null))
            }
        };
}