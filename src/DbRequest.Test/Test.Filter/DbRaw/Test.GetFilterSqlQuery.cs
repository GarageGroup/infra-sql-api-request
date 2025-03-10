using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbRawFilterTest
{
    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_ExpectCorrectSqlQuery(
        DbRawFilter source, SqlDialect dialect, string expected)
    {
        var actual = source.GetFilterSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbRawFilter, SqlDialect, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new(null!),
                SqlDialect.TransactSql,
                string.Empty
            },
            {
                new("SELECT * FROM Product"),
                SqlDialect.TransactSql,
                "SELECT * FROM Product"
            },
            {
                new("SELECT * FROM Product")
                {
                    Parameters =
                    [
                        new("Param1", "Some value")
                    ]
                },
                SqlDialect.TransactSql,
                "SELECT * FROM Product"
            },
            {
                new(null!),
                SqlDialect.PostgreSql,
                string.Empty
            },
            {
                new("SELECT * FROM Product"),
                (SqlDialect)37,
                "SELECT * FROM Product"
            },
            {
                new("SELECT * FROM Product")
                {
                    Parameters =
                    [
                        new("Param1", "Some value")
                    ]
                },
                SqlDialect.PostgreSql,
                "SELECT * FROM Product"
            }
        };
}