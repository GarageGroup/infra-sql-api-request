using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbSelectQueryTest
{
    [Theory]
    [MemberData(nameof(ParametersTestData))]
    public static void GetFilterParameters_ExpectCorrectParameters(DbSelectQuery source, FlatArray<DbParameter> expected)
    {
        var actual = source.GetParameters();
        Assert.StrictEqual(expected, actual);
    }

    public static TheoryData<DbSelectQuery, FlatArray<DbParameter>> ParametersTestData
        =>
        new()
        {
            {
                new("Country"),
                default
            },
            {
                new("Country")
                {
                    Top = 5,
                    Offset = long.MaxValue,
                    SelectedFields = new("Id", "Name"),
                    Filter = new StubDbFilter("Id > 0"),
                    Orders = new(
                        new DbOrder("Name"))
                },
                default
            },
            {
                new("Property", "p")
                {
                    Filter = new StubDbFilter("HasBalcony = @HasBalcony", new DbParameter("HasBalcony", false))
                },
                new(
                    new DbParameter("HasBalcony", false))
            },
            {
                new("Property", "\t")
                {
                    SelectedFields = new("Id", "Name"),
                    Filter = new StubDbFilter("Id > @Id", new("Id", 15), new("Name", null)),
                    GroupByFields = new("Id", "Name")
                },
                new(
                    new("Id", 15), new("Name", null))
            },
            {
                new("Property", "p")
                {
                    JoinedTables = new DbJoinedTable[]
                    {
                        new(DbJoinType.Inner, "Translation", "t", new StubDbFilter("t.PropertyId = p.Id", new DbParameter("Id", 101)))
                    }
                },
                new(
                    new DbParameter("Id", 101))
            },
            {
                new("Property", "p")
                {
                    Top = 15,
                    SelectedFields = new("p.Id", "t.Id"),
                    Filter = new StubDbFilter(
                        "Id > @Id", new("Id", 15), new("Name", "Some string"), new("Price0", null), new("Price1", 10.51m)),
                    JoinedTables = new DbJoinedTable[]
                    {
                        new(DbJoinType.Left, "Translation", "t", new StubDbFilter("t.PropertyId = p.Id", new DbParameter("Value", null))),
                        new(DbJoinType.Right, "City", "c", new StubDbFilter("c.Id <> p.CityId", new DbParameter("CityValue", "Some value")))
                    },
                    GroupByFields = new("p.Id")
                },
                new(
                    new("Id", 15),
                    new("Name", "Some string"),
                    new("Price0", null),
                    new("Price1", 10.51m),
                    new("Value", null),
                    new("CityValue", "Some value"))
            },
            {
                new("Property", "p")
                {
                    AppliedTables = new DbAppliedTable[]
                    {
                        new(
                            type: DbApplyType.Cross,
                            selectQuery: new("PropertyType", "pt")
                            {
                                Filter = new StubDbFilter(
                                    "Name = @Name", new("Id", 5), new("Name", "Test"))
                            },
                            alias: "pt")
                    }
                },
                new(
                    new("Id", 5),
                    new("Name", "Test"))
            }
        };
}