using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

/*partial class DbExistsFilterTest
{
    [Theory]
    [InlineData(SqlDialect.TransactSql)]
    [InlineData(SqlDialect.PostgreSql)]
    public static void GetFilterSqlQuery_DialectIsCorrect_ExpectCorrectQuery(SqlDialect dialect)
    {
        var selectQuery = new DbSelectQuery("SomeTable")
        {
            SelectedFields = new("Id"),
            Filter = new StubDbFilter(
                queries: new Dictionary<SqlDialect, string>
                {
                    [dialect] = "Price > 0"
                },
                parameters:
                [
                    new("Price", 15)
                ])
        };

        var source = new DbExistsFilter(selectQuery);

        var expected = $"EXISTS ({selectQuery.GetSqlQuery(dialect)})";
        var actual = source.GetFilterSqlQuery(dialect);

        Assert.Equal(expected, actual);
    }
}*/