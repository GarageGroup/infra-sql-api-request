using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbInsertQueryTest
{
    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetFilterSqlQuery_TypesAreInRange_ExpectCorrectSqlQuery(DbInsertQuery source, string expected)
    {
        var actual = source.GetSqlQuery();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbInsertQuery, string> SqlQueryTestData
        =>
        new()
        {
            {
                new(
                    tableName: "Country",
                    fieldValues: default),
                string.Empty
            },
            {
                new(
                    tableName: "SomeTable",
                    fieldValues: new DbFieldValue[]
                    {
                        new("Id", 15)
                    }),
                "INSERT INTO SomeTable (Id) VALUES (@Id);"
            },
            {
                new(
                    tableName: "Country",
                    fieldValues: new DbFieldValue[]
                    {
                        new("Name", "Some value"),
                        new("Id", null, "Id1")
                    }),
                "INSERT INTO Country (Name, Id) VALUES (@Name, @Id1);"
            }
        };
}