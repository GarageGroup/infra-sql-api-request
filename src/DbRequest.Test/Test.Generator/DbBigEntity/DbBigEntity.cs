namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

[DbEntity("SomeEntity", "e")]
[DbJoin(DbJoinType.Inner, "Table01", "t01", "t01.Id = e.Id01")]
[DbJoin(DbJoinType.Left, "Table02", "t02", "t02.Id = e.Id02")]
[DbJoin(DbJoinType.Right, "Table03", "t03", "t03.Id = e.Id03")]
[DbJoin(DbJoinType.Inner, "Table04", "t04", "t04.Id = e.Id04")]
[DbJoin(DbJoinType.Left, "Table05", "t05", "t05.Id = e.Id05")]
[DbJoin(DbJoinType.Left, "Table06", "t06", "t06.Id = e.Id06")]
[DbJoin(DbJoinType.Right, "Table07", "t07", "t07.Id = e.Id07")]
[DbJoin(DbJoinType.Inner, "Table08", "t08", "t08.Id = e.Id08")]
[DbJoin(DbJoinType.Inner, "Table09", "t09", "t09.Id = e.Id09")]
[DbJoin(DbJoinType.Left, "Table10", "t10", "t10.Id = e.Id10")]
[DbJoin(DbJoinType.Right, "Table11", "t11", "t11.Id = e.Id11")]
[DbJoin(DbJoinType.Left, "Table12", "t12", "t12.Id = e.Id12")]
[DbJoin(DbJoinType.Inner, "Table13", "t13", "t13.Id = e.Id13")]
[DbJoin(DbJoinType.Right, "Table14", "t14", "t14.Id = e.Id14")]
[DbJoin(DbJoinType.Inner, "Table15", "t15", "t15.Id = e.Id15")]
[DbJoin(DbJoinType.Left, "Table16", "t16", "t16.Id = e.Id16")]
[DbJoin(DbJoinType.Inner, "Table17", "t17", "t17.Id = e.Id17")]
internal sealed partial record class DbBigEntity
{
    [DbSelect("Query01", "e")]
    [DbSelect("Query00", "e")]
    public string? Field00 { get; init; }

    [DbSelect("Query17", "t01", GroupBy = true)]
    [DbSelect("Query16", "t01", GroupBy = true)]
    [DbSelect("Query01", "t01", GroupBy = true)]
    public string? Field01 { get; init; }

    [DbSelect("Query17", "t02", GroupBy = true)]
    [DbSelect("Query16", "t02", GroupBy = true)]
    public string? Field02 { get; init; }

    [DbSelect("Query17", "t03", GroupBy = true)]
    [DbSelect("Query16", "t03", GroupBy = true)]
    public string? Field03 { get; init; }

    [DbSelect("Query17", "t04", GroupBy = true)]
    [DbSelect("Query16", "t04", GroupBy = true)]
    public string? Field04 { get; init; }

    [DbSelect("Query17", "t05", GroupBy = true)]
    [DbSelect("Query16", "t05", GroupBy = true)]
    public string? Field05 { get; init; }

    [DbSelect("Query17", "t06", GroupBy = true)]
    [DbSelect("Query16", "t06", GroupBy = true)]
    public string? Field06 { get; init; }

    [DbSelect("Query17", "t07", GroupBy = true)]
    [DbSelect("Query16", "t07", GroupBy = true)]
    public string? Field07 { get; init; }

    [DbSelect("Query17", "t08", GroupBy = true)]
    [DbSelect("Query16", "t08", GroupBy = true)]
    public string? Field08 { get; init; }

    [DbSelect("Query17", "t09", GroupBy = true)]
    [DbSelect("Query16", "t09", GroupBy = true)]
    public string? Field09 { get; init; }

    [DbSelect("Query17", "t10", GroupBy = true)]
    [DbSelect("Query16", "t10", GroupBy = true)]
    public string? Field10 { get; init; }

    [DbSelect("Query17", "t11", GroupBy = true)]
    [DbSelect("Query16", "t11", GroupBy = true)]
    public string? Field11 { get; init; }

    [DbSelect("Query17", "t12", GroupBy = true)]
    [DbSelect("Query16", "t12", GroupBy = true)]
    public string? Field12 { get; init; }

    [DbSelect("Query17", "t13", GroupBy = true)]
    [DbSelect("Query16", "t13", GroupBy = true)]
    public string? Field13 { get; init; }

    [DbSelect("Query17", "t14", GroupBy = true)]
    [DbSelect("Query16", "t14", GroupBy = true)]
    public string? Field14 { get; init; }

    [DbSelect("Query17", "t15", GroupBy = true)]
    [DbSelect("Query16", "t15", GroupBy = true)]
    public string? Field15 { get; init; }

    [DbSelect("Query17", "t16", GroupBy = true)]
    [DbSelect("Query16", "t16", GroupBy = true)]
    public string? Field16 { get; init; }

    [DbSelect("Query17", "t17", GroupBy = true)]
    public string? Field17 { get; init; }
}