using System;
using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbDeleteQueryTest
{
    [Theory]
    [InlineData(SqlDialect.PostgreSql, "PostgreSql")]
    [InlineData((SqlDialect)21, "21")]
    public static void GetSqlQuery_DialectIsNotSupported_ExpectNotSupportedException(
        SqlDialect dialect, string expectedName)
    {
        var source = new DbDeleteQuery(
            tableName: "SomeTable",
            filter: new StubDbFilter(
                queries: new Dictionary<SqlDialect, string>
                {
                    [dialect] = "Price > 0"
                }));
        var ex = Assert.Throws<NotSupportedException>(Test);

        Assert.Contains("DELETE", ex.Message, StringComparison.InvariantCulture);
        Assert.Contains(expectedName, ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            _ = source.GetSqlQuery(dialect);
    }

    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetSqlQuery_DialectIsSupported_ExpectCorrectSqlQuery(
        DbDeleteQuery source, SqlDialect dialect, string expected)
    {
        var actual = source.GetSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbDeleteQuery, SqlDialect, string> SqlQueryTestData
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
                        }
                    )),
                SqlDialect.TransactSql,
                "DELETE FROM Country WHERE Id = 15;"
            }
        };
}