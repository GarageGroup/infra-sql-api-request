using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

/*partial class DbRawFilterTest
{
    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_ExpectCorrectSqlQuery(DbRawFilter source, string expected)
    {
        var actual = source.GetFilterSqlQuery();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbRawFilter, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new(null!),
                string.Empty
            },
            {
                new("SELECT * FROM Product"),
                "SELECT * FROM Product"
            },
            {
                new("SELECT * FROM Product")
                {
                    Parameters = new DbParameter[]
                    {
                        new("Param1", "Some value")
                    }
                },
                "SELECT * FROM Product"
            }
        };
}*/