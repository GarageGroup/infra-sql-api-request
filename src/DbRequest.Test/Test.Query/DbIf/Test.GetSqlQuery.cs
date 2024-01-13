using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbIfQueryTest
{
    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetFilterSqlQuery_TypesAreInRange_ExpectCorrectSqlQuery(DbIfQuery source, string expected)
    {
        var actual = source.GetSqlQuery();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbIfQuery, string> SqlQueryTestData
        =>
        new()
        {
            {
                new(
                    condition: new StubDbFilter("Id = @Id", new DbParameter("Id", 15)),
                    thenQuery: new StubDbQuery("SELECT * FROM Country")),
                "IF Id = @Id\n" +
                "BEGIN\n" +
                "SELECT * FROM Country\n" +
                "END"
            },
            {
                new(
                    condition: new StubDbFilter("Id = @Id", new DbParameter("Id", null)),
                    thenQuery: new StubDbQuery("SELECT * FROM Country"),
                    elseQuery: new StubDbQuery("SELECT Price, Name FROM Product WHERE Price > @Price", new DbParameter("Price", 1000))),
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