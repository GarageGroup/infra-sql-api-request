using System;
using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbIfQueryTest
{
    [Theory]
    [InlineData(SqlDialect.PostgreSql, "PostgreSql")]
    [InlineData((SqlDialect)21, "21")]
    public static void GetSqlQuery_DialectIsNotSupported_ExpectNotSupportedException(
        SqlDialect dialect, string expectedName)
    {
        var source = new DbIfQuery(
            condition: new StubInvariantDbFilter("Id = 5"),
            thenQuery: new StubInvariantDbQuery("SELECT * FROM Country"),
            elseQuery: new StubInvariantDbQuery("SELECT * FROM City"));

        var ex = Assert.Throws<NotSupportedException>(Test);

        Assert.Contains("IF", ex.Message, StringComparison.InvariantCulture);
        Assert.Contains(expectedName, ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            _ = source.GetSqlQuery(dialect);
    }

    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetSqlQuery_DialectIsSupported_ExpectCorrectSqlQuery(
        DbIfQuery source, SqlDialect dialect, string expected)
    {
        var actual = source.GetSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbIfQuery, SqlDialect, string> SqlQueryTestData
        =>
        new()
        {
            {
                new(
                    condition: new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "Id = @Id"
                        },
                        parameters: new DbParameter("Id", 15)),
                    thenQuery: new StubDbQuery(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "SELECT * FROM Country"
                        })),
                SqlDialect.TransactSql,
                "IF Id = @Id\nBEGIN\nSELECT * FROM Country\nEND"
            },
            {
                new(
                    condition: new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "Id = @Id"
                        },
                        parameters: new DbParameter("Id", null)),
                    thenQuery: new StubDbQuery(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "SELECT * FROM Country"
                        }),
                    elseQuery: new StubDbQuery(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "SELECT Price, Name FROM Product WHERE Price > @Price"
                        },
                        parameters: new DbParameter("Price", 1000))),
                SqlDialect.TransactSql,
                "IF Id = @Id\n" +
                "BEGIN\n" +
                "SELECT * FROM Country\n" +
                "END\n" +
                "ELSE\n" +
                "BEGIN\n" +
                "SELECT Price, Name FROM Product WHERE Price > @Price\n" +
                "END"
            }
        };
}