using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbLikeFilterTest
{
    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_ExpectCorrectQuery(DbLikeFilter source, string expected)
    {
        var actual = source.GetFilterSqlQuery();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbLikeFilter, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new("LOWER(p.Name)", "TeSt", "Search"),
                "LOWER(p.Name) LIKE '%' + @Search + '%'"
            },
            {
                new("Field1", null, "Field1"),
                "Field1 LIKE '%' + @Field1 + '%'"
            },
            {
                new("p.Name", "\t", "Name"),
                "p.Name LIKE '%' + @Name + '%'"
            }
        };
}