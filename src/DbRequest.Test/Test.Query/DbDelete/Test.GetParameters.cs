using System;
using System.Collections.Generic;
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
                    filter: new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "Id = 15"
                        })),
                default
            },
            {
                new(
                    tableName: "Country",
                    filter: new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "Price > 0"
                        },
                        parameters: [new("Price", 25), new("Id", null)])),
                new(
                    new("Price", 25),
                    new("Id", null))
            }
        };
}