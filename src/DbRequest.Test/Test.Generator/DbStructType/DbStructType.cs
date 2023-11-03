using System;
using PrimeFuncPack.UnitTest;

namespace GarageGroup.Infra.Sql.Api.Core.DbRequest.Test;

[DbEntity]
internal readonly partial struct DbStructType
{
    [DbSelect("QueryAll")]
    public short Id { get; init; }

    [DbSelect("QueryAll")]
    public bool? IsActual { get; init; }

    [DbSelect("QueryAll")]
    internal DateTimeOffset CreateAt { get; init; }

    [DbSelect("QueryAll")]
    public DateOnly? ProductDate { get; init; }

    [DbSelect("QueryTotalCount", FieldName = "COUNT(*)")]
    public int? TotalCount { get; init; }

    [DbSelect("QueryAll")]
    public decimal Price { get; init; }

    [DbSelect("QueryAll")]
    public RefType AddionalData { get; init; }
}