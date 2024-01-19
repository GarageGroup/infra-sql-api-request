using System;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbSelectQueryTest
{
    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetFilterSqlQuery_TypesAreInRange_ExpectCorrectSqlQuery(DbSelectQuery source, string expected)
    {
        var actual = source.GetSqlQuery();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public static void GetSqlQuery_JoinTypeIsOutOfRange_ExpectArgumentOutOfRangeException()
    {
        const int joinType = -5;

        var source = new DbSelectQuery("City")
        {
            JoinedTables = new DbJoinedTable[]
            {
                new((DbJoinType)joinType, "Country", "c", new StubDbFilter("c.Id = 1"))
            }
        };

        var ex = Assert.Throws<ArgumentOutOfRangeException>(Test);
        Assert.Contains(joinType.ToString(), ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            source.GetSqlQuery();
    }

    [Fact]
    public static void GetSqlQuery_OrderTypeIsOutOfRange_ExpectArgumentOutOfRangeException()
    {
        const int orderType = -3;

        var source = new DbSelectQuery("Country")
        {
            Orders = new DbOrder[]
            {
                new("Name", (DbOrderType)orderType)
            }
        };

        var ex = Assert.Throws<ArgumentOutOfRangeException>(Test);
        Assert.Contains(orderType.ToString(), ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            source.GetSqlQuery();
    }

    public static TheoryData<DbSelectQuery, string> SqlQueryTestData
        =>
        new()
        {
            {
                new("Country"),
                "SELECT * FROM Country"
            },
            {
                new("Country")
                {
                    Top = 5
                },
                "SELECT TOP 5 * FROM Country"
            },
            {
                new("Property", "p")
                {
                    Filter = new StubDbFilter("HasBalcony = 1")
                },
                "SELECT * FROM Property p WHERE HasBalcony = 1"
            },
            {
                new("Property", "\t")
                {
                    SelectedFields = new("Id", "SUM(Price) AS Price"),
                    Filter = new StubDbFilter(
                        "Id > @Id AND Name = @Name AND Price > 0",
                        new("HasBalcony", false),
                        new("Name", null)),
                    GroupByFields = new("Id")
                },
                "SELECT Id, SUM(Price) AS Price FROM Property WHERE Id > @Id AND Name = @Name AND Price > 0 GROUP BY Id"
            },
            {
                new("Property", "p")
                {
                    JoinedTables = new DbJoinedTable[]
                    {
                        new(DbJoinType.Inner, "Translation", "t", new StubDbFilter("t.PropertyId = p.Id"))
                    }
                },
                "SELECT * FROM Property p INNER JOIN Translation t ON t.PropertyId = p.Id"
            },
            {
                new("Property", "p")
                {
                    Filter = new StubDbFilter("p.Id > @Id", new DbParameter("Id", 15)),
                    JoinedTables = new DbJoinedTable[]
                    {
                        new(DbJoinType.Left, "Translation", "t", new StubDbFilter("t.PropertyId = p.Id")),
                        new(DbJoinType.Right, "City", "c", new StubDbFilter("c.Id <> p.CityId"))
                    }
                },
                "SELECT * FROM Property p LEFT JOIN Translation t ON t.PropertyId = p.Id RIGHT JOIN City c ON c.Id <> p.CityId WHERE p.Id > @Id"
            },
            {
                new("Property", "p")
                {
                    SelectedFields = new("p.Id", "\t", "t.Id", null!),
                    Filter = new StubDbFilter("p.Id > @Id", new DbParameter("Id", 15)),
                    JoinedTables = new DbJoinedTable[]
                    {
                        new(DbJoinType.Left, "Translation", "t", new StubDbFilter("t.PropertyId = p.Id")),
                        new(DbJoinType.Right, "City", "c", new StubDbFilter("c.Id <> p.CityId"))
                    },
                    GroupByFields = new("p.Id", null!, "t.Id", "\n\r"),
                    Orders = new DbOrder("Date").AsFlatArray()
                },
                "SELECT p.Id, t.Id FROM Property p LEFT JOIN Translation t ON t.PropertyId = p.Id RIGHT JOIN City c ON c.Id <> p.CityId" +
                " WHERE p.Id > @Id GROUP BY p.Id, t.Id ORDER BY Date"
            },
            {
                new("Property", "p")
                {
                    AppliedTables = new DbAppliedTable[]
                    {
                        new(DbApplyType.Outer, new("PropertyOwner", "po"), "po"),
                        new(DbApplyType.Cross, new("PropertyType", "pt"), "pt")
                    }
                },
                "SELECT * FROM Property p OUTER APPLY (SELECT * FROM PropertyOwner po) po " +
                "CROSS APPLY (SELECT * FROM PropertyType pt) pt"
            },
            {
                new("Property", "p")
                {
                    Filter = new StubDbFilter("p.Id > @Id", new DbParameter("Id", 15)),
                    AppliedTables = new DbAppliedTable[]
                    {
                        new(DbApplyType.Cross, new("PropertyOwner", "po"), "po")
                    }
                },
                "SELECT * FROM Property p CROSS APPLY (SELECT * FROM PropertyOwner po) po" +
                " WHERE p.Id > @Id"
            },
            {
                new("Property")
                {
                    Orders = new DbOrder[]
                    {
                        new("CrmId", DbOrderType.Ascending)
                    }
                },
                "SELECT * FROM Property ORDER BY CrmId ASC"
            },
            {
                new("Property", "p")
                {
                    Orders = new DbOrder[]
                    {
                        new("p.CrmId", DbOrderType.Descending),
                        new("Id", DbOrderType.Ascending),
                        new("Price")
                    }
                },
                "SELECT * FROM Property p ORDER BY p.CrmId DESC, Id ASC, Price"
            },
            {
                new("Property")
                {
                    Orders = new DbOrder[]
                    {
                        new("Id"),
                        new("Name", DbOrderType.Descending)
                    },
                    Offset = 7
                },
                "SELECT * FROM Property ORDER BY Id, Name DESC OFFSET 7"
            },
            {
                new("Property", "p")
                {
                    Top = 7,
                    Offset = 5071,
                    SelectedFields = new("p.Id", "t.Id"),
                    Filter = new StubDbFilter("p.Id > @Id", new DbParameter("Id", 15)),
                    JoinedTables = new DbJoinedTable[]
                    {
                        new(DbJoinType.Left, "Translation", "t", new StubDbFilter("t.PropertyId = p.Id")),
                        new(DbJoinType.Right, "City", "c", new StubDbFilter("c.Id <> p.CityId"))
                    },
                    Orders = new DbOrder[]
                    {
                        new("p.Id", DbOrderType.Descending),
                        new("c.Id")
                    }
                },
                "SELECT p.Id, t.Id FROM Property p LEFT JOIN Translation t ON t.PropertyId = p.Id RIGHT JOIN City c ON c.Id <> p.CityId" +
                " WHERE p.Id > @Id ORDER BY p.Id DESC, c.Id OFFSET 5071 ROWS FETCH NEXT 7 ROWS ONLY"
            }
        };
}