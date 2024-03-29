using System;
using PrimeFuncPack.UnitTest;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

[DbEntity("Product", "p")]
[DbJoin(DbJoinType.Inner, "Unit", "u", "u.Id = p.UnitId")]
internal sealed partial record class DbRefRecord
{
    [DbSelect("QueryAll", GroupBy = true)]
    [DbSelect("QueryId", FieldName = "p.Id")]
    public long Id { get; init; }

    [DbSelect("QueryAll")]
    public byte? Byte { get; init; }

    [DbSelect("QueryAll", "p", "Time")]
    public DateTime EntityTime { get; init; }

    [DbSelect("QueryAll")]
    public DateTimeOffset? UpdatedAt { get; init; }

    [DbSelect("QueryAll", TableName = "Product")]
    public double? Price { get; init; }

    [DbSelect("QueryAll", "u")]
    public float Sum { get; init; }

    public StructType AdditionalStructData { get; init; }

    [DbSelect("QueryAll")]
    public RecordType? AdditionalRefData { get; init; }
}