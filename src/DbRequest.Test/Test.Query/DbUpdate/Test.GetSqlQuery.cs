using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

/*partial class DbUpdateQueryTest
{
    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetFilterSqlQuery_TypesAreInRange_ExpectCorrectSqlQuery(DbUpdateQuery source, string expected)
    {
        var actual = source.GetSqlQuery();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbUpdateQuery, string> SqlQueryTestData
        =>
        new()
        {
            {
                new(
                    tableName: "Country",
                    fieldValues: default,
                    filter: new StubDbFilter("Price > 0")),
                string.Empty
            },
            {
                new(
                    tableName: "SomeTable",
                    fieldValues: new DbFieldValue[]
                    {
                        new("Id", 15)
                    },
                    filter: new StubDbFilter("Price > 0")),
                "UPDATE SomeTable SET Id = @Id WHERE Price > 0;"
            },
            {
                new(
                    tableName: "Country",
                    fieldValues: new DbFieldValue[]
                    {
                        new("Name", "Some value"),
                        new("Id", null, "Id1")
                    },
                    filter: new StubDbFilter("Price > 0")),
                "UPDATE Country SET Name = @Name, Id = @Id1 WHERE Price > 0;"
            }
        };
}*/