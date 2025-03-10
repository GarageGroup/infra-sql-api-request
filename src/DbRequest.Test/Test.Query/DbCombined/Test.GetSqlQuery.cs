using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbCombinedQueryTest
{
    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetSqlQuery_ExpectCorrectSqlQuery(
        DbCombinedQuery source, SqlDialect dialect, string expected)
    {
        var actual = source.GetSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbCombinedQuery, SqlDialect, string> SqlQueryTestData
        =>
        new()
        {
            {
                new(default),
                SqlDialect.TransactSql,
                string.Empty
            },
            {
                new(
                    queries:
                    [
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
                SqlDialect.TransactSql,
                "SELECT Id, Name From Country\nINSERT INTO SomeTable (Id) VALUES (@Id);"
            },
            {
                new(default),
                SqlDialect.PostgreSql,
                string.Empty
            },
            {
                new(
                    queries:
                    [
                        new StubDbQuery(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.PostgreSql] = "SELECT Id, Name From Country"
                            }),
                        new StubDbQuery(
                            queries: new Dictionary<SqlDialect, string>
                            {
                                [SqlDialect.PostgreSql] = "INSERT INTO SomeTable (Id) VALUES (@Id);"
                            },
                            parameters: [new("Id", 1), new("Price", null)])
                    ]),
                SqlDialect.PostgreSql,
                "SELECT Id, Name From Country\nINSERT INTO SomeTable (Id) VALUES (@Id);"
            }
        };
}