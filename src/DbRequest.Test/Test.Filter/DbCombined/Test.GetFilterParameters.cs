using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbCombinedFilterTest
{
    [Theory]
    [MemberData(nameof(FilterParametersTestData))]
    public static void GetFilterParameters_ExpectCorrectParameters(DbCombinedFilter source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetFilterParameters();
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbCombinedFilter, FlatArray<DbParameter>> FilterParametersTestData
        =>
        new()
        {
            {
                new(DbLogicalOperator.And, default),
                default
            },
            {
                new(DbLogicalOperator.Or, default),
                default
            },
            {
                new(
                    DbLogicalOperator.Or,
                    new StubDbFilter[]
                    {
                        new("Id > 0")
                    }),
                default
            },
            {
                new(
                    DbLogicalOperator.And,
                    new StubDbFilter[]
                    {
                        new(
                            "HasBalcony = @HasBalcony",
                            [
                                new("HasBalcony", false)
                            ])
                    }),
                new(
                    new DbParameter("HasBalcony", false))
            },
            {
                new(
                    DbLogicalOperator.And,
                    new StubDbFilter[]
                    {
                        new(
                            "Id > @Id",
                            [
                                new("Id", 15)
                            ]),
                        new(
                            "Name = @Name",
                            [
                                new("Name", null)
                            ]),
                        new("Price > 0")
                    }),
                new(
                    new("Id", 15),
                    new("Name", null))
            },
            {
                new(
                    DbLogicalOperator.Or,
                    new StubDbFilter[]
                    {
                        new(
                            "Id > @Id",
                            [
                                new("Id", 15)
                            ]),
                        new(
                            "\t",
                            [
                                new("Name", "Some string")
                            ]),
                        new(
                            "Price IN (@Price0, @Price1)",
                            [
                                new("Price0", null),
                                new("Price1", 10.51m)
                            ])
                    }),
                new(
                    new("Id", 15),
                    new("Name", "Some string"),
                    new("Price0", null),
                    new("Price1", 10.51m))
            }
        };
}