using System;
using System.Collections.Generic;
using Xunit;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

public static class DbBigEntityTest
{
    [Fact]
    public static void ReadEntity_DbItemIsNull_ExpectArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(Test);
        Assert.Equal("dbItem", ex.ParamName);

        static void Test()
            =>
            _ = DbBigEntity.ReadEntity(null!);
    }

    [Fact]
    public static void ReadEntity_DbItemIsNotNull_ExpectCorrectInitializedEntity()
    {
        var orDefaultValues = new Dictionary<string, DbValue?>
        {
            ["Field00"] = StubDbValue.CreateString("Zero"),
            ["Field01"] = StubDbValue.CreateString("One"),
            ["Field02"] = StubDbValue.CreateString("Two"),
            ["Field03"] = StubDbValue.CreateString("Three"),
            ["Field04"] = StubDbValue.CreateString("Four"),
            ["Field05"] = StubDbValue.CreateString("Five"),
            ["Field06"] = StubDbValue.CreateString("Six"),
            ["Field07"] = StubDbValue.CreateString("Seven"),
            ["Field08"] = StubDbValue.CreateString("Eight"),
            ["Field09"] = StubDbValue.CreateString("Nine"),
            ["Field10"] = StubDbValue.CreateString("Ten"),
            ["Field11"] = StubDbValue.CreateString("Eleven"),
            ["Field12"] = StubDbValue.CreateString("Twelve"),
            ["Field13"] = StubDbValue.CreateString("Thirteen"),
            ["Field14"] = StubDbValue.CreateString("Fourteen"),
            ["Field15"] = StubDbValue.CreateString("Fifteen"),
            ["Field16"] = StubDbValue.CreateString("Sixteen"),
            ["Field17"] = StubDbValue.CreateString("Seventeen")
        };

        var dbItem = StubDbItem.Create(new Dictionary<string, DbValue>(0), orDefaultValues);
        var actual = DbBigEntity.ReadEntity(dbItem);

        var expected = new DbBigEntity
        {
            Field00 = "Zero",
            Field01 = "One",
            Field02 = "Two",
            Field03 = "Three",
            Field04 = "Four",
            Field05 = "Five",
            Field06 = "Six",
            Field07 = "Seven",
            Field08 = "Eight",
            Field09 = "Nine",
            Field10 = "Ten",
            Field11 = "Eleven",
            Field12 = "Twelve",
            Field13 = "Thirteen",
            Field14 = "Fourteen",
            Field15 = "Fifteen",
            Field16 = "Sixteen",
            Field17 = "Seventeen"
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public static void GetQuery00_ExpectCorrectQuery()
    {
        var actual = DbBigEntity.Query00;

        var expected = new DbSelectQuery("SomeEntity", "e")
        {
            SelectedFields = new("e.Field00"),
            GroupByFields = default,
            JoinedTables = default
        };

        Assert.StrictEqual(expected, actual);
    }

    [Fact]
    public static void GetQuery01_ExpectCorrectQuery()
    {
        var actual = DbBigEntity.Query01;

        var expected = new DbSelectQuery("SomeEntity", "e")
        {
            SelectedFields = new("e.Field00", "t01.Field01"),
            GroupByFields = new("t01.Field01"),
            JoinedTables =
            [
                new(DbJoinType.Inner, "Table01", "t01", new DbRawFilter("t01.Id = e.Id01"))
            ]
        };

        Assert.StrictEqual(expected, actual);
    }

    [Fact]
    public static void GetQuery16_ExpectCorrectQuery()
    {
        var actual = DbBigEntity.Query16;

        var expected = new DbSelectQuery("SomeEntity", "e")
        {
            SelectedFields =
            [
                "t01.Field01", "t02.Field02", "t03.Field03", "t04.Field04",
                "t05.Field05", "t06.Field06", "t07.Field07", "t08.Field08",
                "t09.Field09", "t10.Field10", "t11.Field11", "t12.Field12",
                "t13.Field13", "t14.Field14", "t15.Field15", "t16.Field16"
            ],
            GroupByFields =
            [
                "t01.Field01", "t02.Field02", "t03.Field03", "t04.Field04",
                "t05.Field05", "t06.Field06", "t07.Field07", "t08.Field08",
                "t09.Field09", "t10.Field10", "t11.Field11", "t12.Field12",
                "t13.Field13", "t14.Field14", "t15.Field15", "t16.Field16"
            ],
            JoinedTables =
            [
                new(DbJoinType.Inner, "Table01", "t01", new DbRawFilter("t01.Id = e.Id01")),
                new(DbJoinType.Left, "Table02", "t02", new DbRawFilter("t02.Id = e.Id02")),
                new(DbJoinType.Right, "Table03", "t03", new DbRawFilter("t03.Id = e.Id03")),
                new(DbJoinType.Inner, "Table04", "t04", new DbRawFilter("t04.Id = e.Id04")),
                new(DbJoinType.Left, "Table05", "t05", new DbRawFilter("t05.Id = e.Id05")),
                new(DbJoinType.Left, "Table06", "t06", new DbRawFilter("t06.Id = e.Id06")),
                new(DbJoinType.Right, "Table07", "t07", new DbRawFilter("t07.Id = e.Id07")),
                new(DbJoinType.Inner, "Table08", "t08", new DbRawFilter("t08.Id = e.Id08")),
                new(DbJoinType.Inner, "Table09", "t09", new DbRawFilter("t09.Id = e.Id09")),
                new(DbJoinType.Left, "Table10", "t10", new DbRawFilter("t10.Id = e.Id10")),
                new(DbJoinType.Right, "Table11", "t11", new DbRawFilter("t11.Id = e.Id11")),
                new(DbJoinType.Left, "Table12", "t12", new DbRawFilter("t12.Id = e.Id12")),
                new(DbJoinType.Inner, "Table13", "t13", new DbRawFilter("t13.Id = e.Id13")),
                new(DbJoinType.Right, "Table14", "t14", new DbRawFilter("t14.Id = e.Id14")),
                new(DbJoinType.Inner, "Table15", "t15", new DbRawFilter("t15.Id = e.Id15")),
                new(DbJoinType.Left, "Table16", "t16", new DbRawFilter("t16.Id = e.Id16"))
            ]
        };

        Assert.StrictEqual(expected, actual);
    }

    [Fact]
    public static void GetQuery17_ExpectCorrectQuery()
    {
        var actual = DbBigEntity.Query17;

        var expected = new DbSelectQuery("SomeEntity", "e")
        {
            SelectedFields =
            [
                "t01.Field01", "t02.Field02", "t03.Field03", "t04.Field04",
                "t05.Field05", "t06.Field06", "t07.Field07", "t08.Field08",
                "t09.Field09", "t10.Field10", "t11.Field11", "t12.Field12",
                "t13.Field13", "t14.Field14", "t15.Field15", "t16.Field16",
                "t17.Field17"
            ],
            GroupByFields =
            [
                "t01.Field01", "t02.Field02", "t03.Field03", "t04.Field04",
                "t05.Field05", "t06.Field06", "t07.Field07", "t08.Field08",
                "t09.Field09", "t10.Field10", "t11.Field11", "t12.Field12",
                "t13.Field13", "t14.Field14", "t15.Field15", "t16.Field16",
                "t17.Field17"
            ],
            JoinedTables =
            [
                new(DbJoinType.Inner, "Table01", "t01", new DbRawFilter("t01.Id = e.Id01")),
                new(DbJoinType.Left, "Table02", "t02", new DbRawFilter("t02.Id = e.Id02")),
                new(DbJoinType.Right, "Table03", "t03", new DbRawFilter("t03.Id = e.Id03")),
                new(DbJoinType.Inner, "Table04", "t04", new DbRawFilter("t04.Id = e.Id04")),
                new(DbJoinType.Left, "Table05", "t05", new DbRawFilter("t05.Id = e.Id05")),
                new(DbJoinType.Left, "Table06", "t06", new DbRawFilter("t06.Id = e.Id06")),
                new(DbJoinType.Right, "Table07", "t07", new DbRawFilter("t07.Id = e.Id07")),
                new(DbJoinType.Inner, "Table08", "t08", new DbRawFilter("t08.Id = e.Id08")),
                new(DbJoinType.Inner, "Table09", "t09", new DbRawFilter("t09.Id = e.Id09")),
                new(DbJoinType.Left, "Table10", "t10", new DbRawFilter("t10.Id = e.Id10")),
                new(DbJoinType.Right, "Table11", "t11", new DbRawFilter("t11.Id = e.Id11")),
                new(DbJoinType.Left, "Table12", "t12", new DbRawFilter("t12.Id = e.Id12")),
                new(DbJoinType.Inner, "Table13", "t13", new DbRawFilter("t13.Id = e.Id13")),
                new(DbJoinType.Right, "Table14", "t14", new DbRawFilter("t14.Id = e.Id14")),
                new(DbJoinType.Inner, "Table15", "t15", new DbRawFilter("t15.Id = e.Id15")),
                new(DbJoinType.Left, "Table16", "t16", new DbRawFilter("t16.Id = e.Id16")),
                new(DbJoinType.Inner, "Table17", "t17", new DbRawFilter("t17.Id = e.Id17"))
            ]
        };

        Assert.StrictEqual(expected, actual);
    }
}