using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbExistsFilterTest
{
    [Fact]
    public static void GetFilterParameters_ExpectCorrectParameters()
    {
        var selectQuery = new DbSelectQuery("SomeTable")
        {
            SelectedFields = new("Id"),
            Filter = new StubDbFilter(
                queries: new Dictionary<SqlDialect, string>
                {
                    [SqlDialect.TransactSql] = "Price > 0",
                    [SqlDialect.PostgreSql] = "Price > 0"
                },
                parameters:
                [
                    new("Price", 15),
                    new("Name", "Some name")
                ])
        };
        var source = new DbExistsFilter(selectQuery);

        var expected = selectQuery.GetParameters();
        var actual = source.GetFilterParameters();

        Assert.StrictEqual(expected, actual);
    }
}