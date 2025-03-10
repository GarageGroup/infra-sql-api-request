using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbInsertQueryTest
{
    [Theory]
    [InlineData(SqlDialect.PostgreSql, "PostgreSql")]
    [InlineData((SqlDialect)101, "101")]
    public static void GetSqlQuery_DialectIsNotSupported_ExpectNotSupportedException(
        SqlDialect dialect, string expectedName)
    {
        var source = new DbInsertQuery(
            tableName: "SomeTable",
            fieldValues:
            [
                new("Id", 15),
                new("Name", "SomeName")
            ]);

        var ex = Assert.Throws<NotSupportedException>(Test);

        Assert.Contains("INSERT", ex.Message, StringComparison.InvariantCulture);
        Assert.Contains(expectedName, ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            _ = source.GetSqlQuery(dialect);
    }

    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetSqlQuery_DialectIsSupported_ExpectCorrectSqlQuery(
        DbInsertQuery source, SqlDialect dialect, string expected)
    {
        var actual = source.GetSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbInsertQuery, SqlDialect, string> SqlQueryTestData
        =>
        new()
        {
            {
                new(
                    tableName: "Country",
                    fieldValues: default),
                SqlDialect.TransactSql,
                string.Empty
            },
            {
                new(
                    tableName: "SomeTable",
                    fieldValues:
                    [
                        new("Id", 15)
                    ]),
                SqlDialect.TransactSql,
                "INSERT INTO SomeTable (Id) VALUES (@Id);"
            },
            {
                new(
                    tableName: "Country",
                    fieldValues:
                    [
                        new("Name", "Some value"),
                        new("Id", null, "Id1")
                    ]),
                SqlDialect.TransactSql,
                "INSERT INTO Country (Name, Id) VALUES (@Name, @Id1);"
            }
        };
}