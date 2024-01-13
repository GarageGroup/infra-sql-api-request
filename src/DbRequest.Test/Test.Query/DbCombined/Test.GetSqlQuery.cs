using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbCombinedQueryTest
{
    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetFilterSqlQuery_TypesAreInRange_ExpectCorrectSqlQuery(DbCombinedQuery source, string expected)
    {
        var actual = source.GetSqlQuery();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbCombinedQuery, string> SqlQueryTestData
        =>
        new()
        {
            {
                new(default),
                string.Empty
            },
            {
                new(
                    queries: new StubDbQuery[]
                    {
                        new("SELECT Id, Name From Country"),
                        new("INSERT INTO SomeTable (Id) VALUES (@Id);", new("Id", 1), new("Price", null))
                    }),
                "SELECT Id, Name From Country\n" +
                "INSERT INTO SomeTable (Id) VALUES (@Id);"
            }
        };
}