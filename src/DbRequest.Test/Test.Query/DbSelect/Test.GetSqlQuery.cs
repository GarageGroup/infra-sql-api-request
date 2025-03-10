using System;
using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

partial class DbSelectQueryTest
{
    [Theory]
    [InlineData(SqlDialect.PostgreSql, "PostgreSql")]
    [InlineData((SqlDialect)15, "15")]
    public static void GetSqlQuery_DialectIsNotSupported_ExpectNotSupportedException(
        SqlDialect dialect, string expectedName)
    {
        var source = new DbSelectQuery(
            tableName: "SomeTable");

        var ex = Assert.Throws<NotSupportedException>(Test);

        Assert.Contains("SELECT", ex.Message, StringComparison.InvariantCulture);
        Assert.Contains(expectedName, ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            _ = source.GetSqlQuery(dialect);
    }

    [Fact]
    public static void GetSqlQuery_JoinTypeIsOutOfRange_ExpectArgumentOutOfRangeException()
    {
        const int joinType = -5;

        var source = new DbSelectQuery("City")
        {
            JoinedTables =
            [
                new((DbJoinType)joinType, "Country", "c", new StubInvariantDbFilter("c.Id = 1"))
            ]
        };

        var ex = Assert.Throws<ArgumentOutOfRangeException>(Test);
        Assert.Contains(joinType.ToString(), ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            _ = source.GetSqlQuery(SqlDialect.TransactSql);
    }

    [Fact]
    public static void GetSqlQuery_OrderTypeIsOutOfRange_ExpectArgumentOutOfRangeException()
    {
        const int orderType = -3;

        var source = new DbSelectQuery("Country")
        {
            Orders =
            [
                new("Name", (DbOrderType)orderType)
            ]
        };

        var ex = Assert.Throws<ArgumentOutOfRangeException>(Test);
        Assert.Contains(orderType.ToString(), ex.Message, StringComparison.InvariantCulture);

        void Test()
            =>
            source.GetSqlQuery(SqlDialect.TransactSql);
    }

    [Theory]
    [MemberData(nameof(SqlQueryTestData))]
    public static void GetSqlQuery_TypesAreInRangeAndDialectIsSupported_ExpectCorrectSqlQuery(
        DbSelectQuery source, SqlDialect dialect, string expected)
    {
        var actual = source.GetSqlQuery(dialect);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<DbSelectQuery, SqlDialect, string> SqlQueryTestData
        =>
        new()
        {
            {
                new("Country"),
                SqlDialect.TransactSql,
                "SELECT * FROM Country"
            },
            {
                new("Country")
                {
                    Top = 5
                },
                SqlDialect.TransactSql,
                "SELECT TOP 5 * FROM Country"
            },
            {
                new("Property", "p")
                {
                    Filter = new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "HasBalcony = 1"
                        })
                },
                SqlDialect.TransactSql,
                "SELECT * FROM Property p WHERE HasBalcony = 1"
            },
            {
                new("Property", "\t")
                {
                    SelectedFields = new("Id", "SUM(Price) AS Price"),
                    Filter = new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "Id > @Id AND Name = @Name AND Price > 0"
                        },
                        parameters: [new("HasBalcony", false), new("Name", null)]),
                    GroupByFields = new("Id")
                },
                SqlDialect.TransactSql,
                "SELECT Id, SUM(Price) AS Price FROM Property WHERE Id > @Id AND Name = @Name AND Price > 0 GROUP BY Id"
            },
            {
                new("Property", "p")
                {
                    JoinedTables =
                    [
                        new(DbJoinType.Inner, "Translation", "t", "t.PropertyId = p.Id")
                    ]
                },
                SqlDialect.TransactSql,
                "SELECT * FROM Property p INNER JOIN Translation t ON t.PropertyId = p.Id"
            },
            {
                new("Property", "p")
                {
                    Filter = new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "p.Id > @Id"
                        },
                        parameters: [new("Id", 15)]),
                    JoinedTables =
                    [
                        new(DbJoinType.Left, "Translation", "t", "t.PropertyId = p.Id"),
                        new(DbJoinType.Right, "City", "c", "c.Id <> p.CityId")
                    ]
                },
                SqlDialect.TransactSql,
                "SELECT * FROM Property p LEFT JOIN Translation t ON t.PropertyId = p.Id RIGHT JOIN City c ON c.Id <> p.CityId WHERE p.Id > @Id"
            },
            {
                new("Property", "p")
                {
                    SelectedFields = new("p.Id", "\t", "t.Id", null!),
                    Filter = new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] =  "p.Id > @Id"
                        },
                        parameters: [new("Id", 15)]),
                    JoinedTables =
                    [
                        new(DbJoinType.Left, "Translation", "t", "t.PropertyId = p.Id"),
                        new(DbJoinType.Right, "City", "c", "c.Id <> p.CityId")
                    ],
                    GroupByFields = new("p.Id", null!, "t.Id", "\n\r"),
                    Orders = [new("Date")]
                },
                SqlDialect.TransactSql,
                "SELECT p.Id, t.Id FROM Property p LEFT JOIN Translation t ON t.PropertyId = p.Id RIGHT JOIN City c ON c.Id <> p.CityId" +
                " WHERE p.Id > @Id GROUP BY p.Id, t.Id ORDER BY Date"
            },
            {
                new("Property", "p")
                {
                    AppliedTables =
                    [
                        new(DbApplyType.Outer, new("PropertyOwner", "po"), "po"),
                        new(DbApplyType.Cross, new("PropertyType", "pt"), "pt")
                    ]
                },
                SqlDialect.TransactSql,
                "SELECT * FROM Property p OUTER APPLY (SELECT * FROM PropertyOwner po) po " +
                "CROSS APPLY (SELECT * FROM PropertyType pt) pt"
            },
            {
                new("Property", "p")
                {
                    Filter = new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "p.Id > @Id"
                        },
                        parameters: [new("Id", 15)]),
                    AppliedTables =
                    [
                        new(DbApplyType.Cross, new("PropertyOwner", "po"), "po")
                    ]
                },
                SqlDialect.TransactSql,
                "SELECT * FROM Property p CROSS APPLY (SELECT * FROM PropertyOwner po) po" +
                " WHERE p.Id > @Id"
            },
            {
                new("Property")
                {
                    Orders =
                    [
                        new("CrmId", DbOrderType.Ascending)
                    ]
                },
                SqlDialect.TransactSql,
                "SELECT * FROM Property ORDER BY CrmId ASC"
            },
            {
                new("Property", "p")
                {
                    Orders =
                    [
                        new("p.CrmId", DbOrderType.Descending),
                        new("Id", DbOrderType.Ascending),
                        new("Price")
                    ]
                },
                SqlDialect.TransactSql,
                "SELECT * FROM Property p ORDER BY p.CrmId DESC, Id ASC, Price"
            },
            {
                new("Property")
                {
                    Orders =
                    [
                        new("Id"),
                        new("Name", DbOrderType.Descending)
                    ],
                    Offset = 7
                },
                SqlDialect.TransactSql,
                "SELECT * FROM Property ORDER BY Id, Name DESC OFFSET 7"
            },
            {
                new("Property", "p")
                {
                    Top = 7,
                    Offset = 5071,
                    SelectedFields = new("p.Id", "t.Id"),
                    Filter = new StubDbFilter(
                        queries: new Dictionary<SqlDialect, string>
                        {
                            [SqlDialect.TransactSql] = "p.Id > @Id"
                        },
                        parameters: [new("Id", 15)]),
                    JoinedTables =
                    [
                        new(
                            DbJoinType.Left,
                            "Translation",
                            "t",
                            new StubDbFilter(
                                queries: new Dictionary<SqlDialect, string>
                                {
                                    [SqlDialect.TransactSql] = "t.PropertyId = p.Id AND t.Price > @Price"
                                },
                                parameters: [new("Price", 27500)])),
                        new(DbJoinType.Right, "City", "c", "c.Id <> p.CityId")
                    ],
                    Orders =
                    [
                        new("p.Id", DbOrderType.Descending),
                        new("c.Id")
                    ]
                },
                SqlDialect.TransactSql,
                "SELECT p.Id, t.Id FROM Property p LEFT JOIN Translation t ON t.PropertyId = p.Id AND t.Price > @Price" +
                " RIGHT JOIN City c ON c.Id <> p.CityId" +
                " WHERE p.Id > @Id ORDER BY p.Id DESC, c.Id OFFSET 5071 ROWS FETCH NEXT 7 ROWS ONLY"
            }
        };
}