using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbDeleteQueryTest
{
    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetFilterSqlQuery_TypesAreInRange_ExpectCorrectSqlQuery(DbDeleteQuery source, string expected)
    {
        var actual = source.GetSqlQuery();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbDeleteQuery, string> SqlQueryTestData
        =>
        new()
        {
            {
                new(
                    tableName: "Country",
                    filter: new StubDbFilter("Id = 15")),
                "DELETE FROM Country WHERE Id = 15;"
            }
        };
}