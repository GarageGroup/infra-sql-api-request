using System;
using Microsoft.CodeAnalysis;
using PrimeFuncPack;

namespace GarageGroup.Infra;

partial class SourceGeneratorExtensions
{
    internal static bool IsType(this ITypeSymbol? typeSymbol, string namespaceName, string typeName)
    {
        if (typeSymbol is null)
        {
            return false;
        }

        return CodeAnalysisExtensions.IsType(typeSymbol, namespaceName, typeName);
    }

    internal static bool IsAnyType(this ITypeSymbol? typeSymbol, string namespaceName, params string[] typeNames)
    {
        if (typeSymbol is null)
        {
            return false;
        }

        return CodeAnalysisExtensions.IsAnyType(typeSymbol, namespaceName, typeNames);
    }

    internal static object? GetAttributeValue(this AttributeData attributeData, int constructorArgumentOrder)
    {
        if (constructorArgumentOrder < 0 || constructorArgumentOrder >= attributeData.ConstructorArguments.Length)
        {
            return null;
        }

        return attributeData.ConstructorArguments[constructorArgumentOrder].Value;
    }

    internal static object? GetAttributeValue(this AttributeData attributeData, int constructorArgumentOrder, string propertyName)
        =>
        attributeData.GetAttributeValue(constructorArgumentOrder) ?? attributeData.GetAttributePropertyValue(propertyName);

    internal static object? GetAttributePropertyValue(this AttributeData attributeData, string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return null;
        }

        foreach (var namedArgument in attributeData.NamedArguments)
        {
            if (string.Equals(namedArgument.Key, propertyName, StringComparison.Ordinal))
            {
                return namedArgument.Value.Value;
            }
        }

        return null;
    }

    internal static bool IsSystemType(this ITypeSymbol typeSymbol, string typeName)
        =>
        typeSymbol.IsType("System", typeName);

    internal static bool IsAnySystemType(this ITypeSymbol typeSymbol, params string[] typeNames)
        =>
        typeSymbol.IsAnyType("System", typeNames);

    internal static DisplayedTypeData GetDisplayedData(this ITypeSymbol typeSymbol)
        =>
        CodeAnalysisExtensions.GetDisplayedData(typeSymbol, withNullableSuffix: false);
}
