using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbIfQueryTest
{
    [Theory]
    [MemberData(nameof(ParametersTestData))]
    public static void GetParameters_ExpectCorrectParameters(DbIfQuery source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetParameters();
        Assert.StrictEqual(expected, actual);
    }

    public static TheoryData<DbIfQuery, FlatArray<DbParameter>> ParametersTestData
        =>
        new()
        {
            {
                new(
                    condition: new StubInvariantDbFilter("Id = @Id"),
                    thenQuery: new StubInvariantDbQuery("SELECT * FROM Country")),
                default
            },
            {
                new(
                    condition: new StubInvariantDbFilter("Id = @Id", new("Id", null), new("Name", "SomeName")),
                    thenQuery: new StubInvariantDbQuery("SELECT * FROM Country", new DbParameter("SomeParameter", 200)),
                    elseQuery: new StubInvariantDbQuery("SELECT Price, Name FROM Product WHERE Price > @Price", new DbParameter("Price", 1000))),
                new(
                    new("Id", null),
                    new("Name", "SomeName"),
                    new("SomeParameter", 200),
                    new("Price", 1000))
            }
        };
}