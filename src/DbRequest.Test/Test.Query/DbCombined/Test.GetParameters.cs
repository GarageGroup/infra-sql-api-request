using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbCombinedQueryTest
{
    [Theory]
    [MemberData(nameof(ParametersTestData))]
    public static void GetFilterParameters_ExpectCorrectParameters(DbCombinedQuery source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetParameters();
        Assert.StrictEqual(expected, actual);
    }

    public static TheoryData<DbCombinedQuery, FlatArray<DbParameter>> ParametersTestData
        =>
        new()
        {
            {
                new(default),
                default
            },
            {
                new(
                    queries: new StubDbQuery[]
                    {
                        new(string.Empty, new DbParameter("SomeName", "SomeValue")),
                        new("SELECT Id, Name From Country"),
                        new("INSERT INTO SomeTable (Id) VALUES (@Id);", new("Id", 1), new("Price", null))
                    }),
                new(
                    new("SomeName", "SomeValue"),
                    new("Id", 1),
                    new("Price", null))
            },
        };
}