using System;
using System.Collections.Generic;
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
                    queries:
                    [
                        new StubDbQuery(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.TransactSql] = string.Empty
                            },
                            parameters: new DbParameter("SomeName", "SomeValue")),
                        new StubDbQuery(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.TransactSql] = "SELECT Id, Name From Country"
                            }),
                        new StubDbQuery(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.TransactSql] = "INSERT INTO SomeTable (Id) VALUES (@Id);"
                            },
                            parameters: [new("Id", 1), new("Price", null)])
                    ]),
                new(
                    new("SomeName", "SomeValue"),
                    new("Id", 1),
                    new("Price", null))
            },
        };
}