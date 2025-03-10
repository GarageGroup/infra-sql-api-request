using System;
using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbCombinedFilterTest
{
    [Theory]
    [MemberData(nameof(FilterParametersTestData))]
    public static void GetFilterParameters_ExpectCorrectParameters(DbCombinedFilter source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetFilterParameters();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbCombinedFilter, FlatArray<DbParameter>> FilterParametersTestData
        =>
        new()
        {
            {
                new(DbLogicalOperator.And, default),
                default
            },
            {
                new(DbLogicalOperator.Or, default),
                default
            },
            {
                new(DbLogicalOperator.Or)
                {
                    Filters =
                    [
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.TransactSql] = "Id > 0"
                            })
                    ]
                },
                default
            },
            {
                new(DbLogicalOperator.And)
                {
                    Filters =
                    [
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.PostgreSql] = "HasBalcony = @HasBalcony"
                            },
                            parameters:
                            [
                                new("HasBalcony", false)
                            ])
                    ]
                },
                [
                    new("HasBalcony", false)
                ]
            },
            {
                new(DbLogicalOperator.And)
                {
                    Filters =
                    [
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.PostgreSql] = "Id > @Id"
                            },
                            parameters:
                            [
                                new("Id", 15)
                            ]),
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.PostgreSql] = "Name = @Name"
                            },
                            parameters:
                            [
                                new("Name", null)
                            ]),
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.PostgreSql] = "Price > 0"
                            })
                    ]
                },
                [
                    new("Id", 15),
                    new("Name", null)
                ]
            },
            {
                new(DbLogicalOperator.Or)
                {
                    Filters =
                    [
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.TransactSql] = "Id > @Id"
                            },
                            parameters:
                            [
                                new("Id", 15)
                            ]),
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.TransactSql] = "\t"
                            },
                            parameters:
                            [
                                new("Name", "Some string")
                            ]),
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.TransactSql] = "Price IN (@Price0, @Price1)"
                            },
                            parameters:
                            [
                                new("Price0", null),
                                new("Price1", 10.51m)
                            ])
                    ]
                },
                [
                    new("Id", 15),
                    new("Name", "Some string"),
                    new("Price0", null),
                    new("Price1", 10.51m)
                ]
            }
        };
}