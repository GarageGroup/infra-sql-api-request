using System;

namespace GarageGroup.Infra;

partial class DbQueryExtensions
{
    internal static NotSupportedException CreateNotSupportedException(this SqlDialect dialect, string operation)
        =>
        new($"The operation '{operation}' is either not supported or not yet implemented for the SQL dialect '{dialect}'.");
}