using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbLikeFilterTest
{
    [Theory]
    [InlineData((SqlDialect)21, "21")]
    public static void GetFilterSqlQuery_DialectIsNotSupported_ExpectNotSupportedException(
        SqlDialect dialect, string expectedName)
    {
        var source = new DbLikeFilter(
            fieldName: "SomeField",
            fieldValue: "Some Text",
            parameterName: "SomeParameter");

        var ex = Assert.Throws<NotSupportedException>(Test);

        Assert.Contains("LIKE", ex.Message, StringComparison.InvariantCulture);
        Assert.Contains(expectedName, ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            _ = source.GetFilterSqlQuery(dialect);
    }

    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_DialectIsSupported_ExpectCorrectQuery(
        DbLikeFilter source, SqlDialect dialect, string expected)
    {
        var actual = source.GetFilterSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbLikeFilter, SqlDialect, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new("LOWER(p.Name)", "TeSt", "Search"),
                SqlDialect.TransactSql,
                "LOWER(p.Name) LIKE '%' + @Search + '%'"
            },
            {
                new("Field1", "Some text", string.Empty),
                SqlDialect.TransactSql,
                "Field1 LIKE '%' + @Field1 + '%'"
            },
            {
                new("p.Name", null, "Name"),
                SqlDialect.TransactSql,
                "p.Name LIKE '%' + @Name + '%'"
            },
            {
                new("LOWER(p.Name)", "TeSt", "Search"),
                SqlDialect.PostgreSql,
                "LOWER(p.Name) LIKE '%' || @Search || '%'"
            },
            {
                new("Field1", "Some text", string.Empty),
                SqlDialect.PostgreSql,
                "Field1 LIKE '%' || @Field1 || '%'"
            },
            {
                new("p.Name", null, "Name"),
                SqlDialect.PostgreSql,
                "p.Name LIKE '%' || @Name || '%'"
            }
        };
}