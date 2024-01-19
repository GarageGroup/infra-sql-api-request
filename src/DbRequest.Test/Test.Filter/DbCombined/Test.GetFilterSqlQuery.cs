using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbCombinedFilterTest
{
    [Theory]
    [MemberData(nameof(FilterSqlQueryTestData))]
    public static void GetFilterSqlQuery_ExpectCorrectQuery(DbCombinedFilter source, string expected)
    {
        var actual = source.GetFilterSqlQuery();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbCombinedFilter, string> FilterSqlQueryTestData
        =>
        new()
        {
            {
                new(DbLogicalOperator.And, default),
                string.Empty
            },
            {
                new(DbLogicalOperator.Or, default),
                string.Empty
            },
            {
                new(
                    DbLogicalOperator.And,
                    new StubDbFilter[]
                    {
                        new(
                            string.Empty,
                            [
                                new("Id", 151)
                            ])
                    }),
                string.Empty
            },
            {
                new(DbLogicalOperator.Or)
                {
                    Filters = new StubDbFilter[]
                    {
                        new(
                            string.Empty,
                            [
                                new("Name", "Some string")
                            ])
                    }
                },
                string.Empty
            },
            {
                new(
                    DbLogicalOperator.And,
                    new StubDbFilter[]
                    {
                        new(
                            "Name = @Name",
                            [
                                new("Name", "Some string")
                            ])
                    }),
                "Name = @Name"
            },
            {
                new(
                    DbLogicalOperator.Or,
                    new StubDbFilter[]
                    {
                        new(
                            "Id = @Id",
                            [
                                new("Id", 21)
                            ])
                    }),
                "Id = @Id"
            },
            {
                new(DbLogicalOperator.And)
                {
                    Filters = new StubDbFilter[]
                    {
                        new(
                            "Name <> @Name",
                            [
                                new("Name", "Some name")
                            ]),
                        new(
                            "Id = @Id OR Sum = NULL",
                            [
                                new("Param1", null),
                                new("Param2", 57),
                                new("Param3", 105)
                            ]),
                        new(
                            string.Empty,
                            [
                                new("Id", 275)
                            ]),
                    }
                },
                "(Name <> @Name AND Id = @Id OR Sum = NULL)"
            },
            {
                new(
                    DbLogicalOperator.Or,
                    new StubDbFilter[]
                    {
                        new("Id > 1"),
                        new(
                            "\t",
                            [
                                new("Name", "Some string")
                            ]),
                        new(
                            "Id < 5",
                            [
                                new("Param1", 15),
                                new("Param2", null),
                                new("Param3", -1)
                            ])
                    }),
                "(Id > 1 OR Id < 5)"
            }
        };
}