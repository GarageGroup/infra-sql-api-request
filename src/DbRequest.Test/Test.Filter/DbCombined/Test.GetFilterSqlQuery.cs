using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbCombinedFilterTest
{
    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_ExpectCorrectQuery(DbCombinedFilter source, SqlDialect dialect, string expected)
    {
        var actual = source.GetFilterSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbCombinedFilter, SqlDialect, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new(DbLogicalOperator.And, default),
                SqlDialect.TransactSql,
                string.Empty
            },
            {
                new(DbLogicalOperator.Or, default),
                SqlDialect.PostgreSql,
                string.Empty
            },
            {
                new(DbLogicalOperator.And)
                {
                    Filters =
                    [
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.TransactSql] = string.Empty
                            },
                            parameters:
                            [
                                new("Id", 151)
                            ])
                    ]
                },
                SqlDialect.TransactSql,
                string.Empty
            },
            {
                new(DbLogicalOperator.Or)
                {
                    Filters =
                    [
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.PostgreSql] = string.Empty
                            },
                            parameters:
                            [
                                new("Name", "Some string")
                            ])
                    ]
                },
                SqlDialect.PostgreSql,
                string.Empty
            },
            {
                new(DbLogicalOperator.And)
                {
                    Filters =
                    [
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.TransactSql] = "Name = @Name"
                            },
                            parameters:
                            [
                                new("Name", "Some string")
                            ])
                    ]
                },
                SqlDialect.TransactSql,
                "Name = @Name"
            },
            {
                new(DbLogicalOperator.Or)
                {
                    Filters =
                    [
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.TransactSql] = "Id = @Id"
                            },
                            parameters:
                            [
                                new("Id", 21)
                            ])
                    ]
                },
                SqlDialect.TransactSql,
                "Id = @Id"
            },
            {
                new(DbLogicalOperator.And)
                {
                    Filters =
                    [
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.PostgreSql] = "Name <> @Name"
                            },
                            parameters:
                            [
                                new("Name", "Some name")
                            ]),
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.PostgreSql] = "Id = @Id OR Sum = NULL"
                            },
                            parameters:
                            [
                                new("Param1", null),
                                new("Param2", 57),
                                new("Param3", 105)
                            ]),
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.PostgreSql] = string.Empty
                            },
                            parameters:
                            [
                                new("Id", 275)
                            ]),
                    ]
                },
                SqlDialect.PostgreSql,
                "(Name <> @Name AND Id = @Id OR Sum = NULL)"
            },
            {
                new(DbLogicalOperator.Or)
                {
                    Filters =
                    [
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [(SqlDialect)67123] = "Id > 1"
                            }),
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [(SqlDialect)67123] = "\t"
                            },
                            parameters:
                            [
                                new("Name", "Some string")
                            ]),
                        new StubDbFilter(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [(SqlDialect)67123] = "Id < 5"
                            },
                            parameters:
                            [
                                new("Param1", 15),
                                new("Param2", null),
                                new("Param3", -1)
                            ])
                    ]
                },
                (SqlDialect)67123,
                "(Id > 1 OR Id < 5)"
            }
        };
}