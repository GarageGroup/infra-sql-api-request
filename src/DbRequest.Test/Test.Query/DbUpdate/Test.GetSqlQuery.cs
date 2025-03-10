using System;
using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbUpdateQueryTest
{
    [Theory]
    [InlineData(SqlDialect.PostgreSql, "PostgreSql")]
    [InlineData((SqlDialect)15, "15")]
    public static void GetSqlQuery_DialectIsNotSupported_ExpectNotSupportedException(
        SqlDialect dialect, string expectedName)
    {
        var source = new DbUpdateQuery(
            tableName: "SomeTable",
            fieldValues:
            [
                new("Name", "Some value"),
                new("Id", null, "SomeId")
            ],
            filter: new StubInvariantDbFilter("Id = 57"));

        var ex = Assert.Throws<NotSupportedException>(Test);

        Assert.Contains("UPDATE", ex.Message, StringComparison.InvariantCulture);
        Assert.Contains(expectedName, ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            _ = source.GetSqlQuery(dialect);
    }

    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetSqlQuery_DialectIsSupported_ExpectCorrectSqlQuery(
        DbUpdateQuery source, SqlDialect dialect, string expected)
    {
        var actual = source.GetSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbUpdateQuery, SqlDialect, string> SqlQueryTestData
        =>
        new()
        {
            {
                new(
                    tableName: "Country",
                    fieldValues: default,
                    filter: new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "Price > 0"
                        })),
                SqlDialect.TransactSql,
                string.Empty
            },
            {
                new(
                    tableName: "SomeTable",
                    fieldValues:
                    [
                        new("Id", 15)
                    ],
                    filter: new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "Price > 0"
                        })),
                SqlDialect.TransactSql,
                "UPDATE SomeTable SET Id = @Id WHERE Price > 0;"
            },
            {
                new(
                    tableName: "Country",
                    fieldValues:
                    [
                        new("Name", "Some value"),
                        new("Id", null, "Id1")
                    ],
                    filter: new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "Id = 27"
                        })),
                SqlDialect.TransactSql,
                "UPDATE Country SET Name = @Name, Id = @Id1 WHERE Id = 27;"
            }
        };
}