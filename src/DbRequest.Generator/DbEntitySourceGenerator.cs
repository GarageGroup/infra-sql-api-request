using System.Linq;
using Microsoft.CodeAnalysis;

namespace GarageGroup.Infra;

[Generator(LanguageNames.CSharp)]
internal sealed class DbEntitySourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterSourceOutput(context.CompilationProvider, InnerGenerateSource);

        static void InnerGenerateSource(SourceProductionContext context, Compilation compilation)
        {
            foreach (var dbEntityType in compilation.GetDbEntityTypes(context.CancellationToken))
            {
                var readEntitySourceCode = dbEntityType.BuildReadEntitySourceCode();
                context.AddSource($"{dbEntityType.FileName}.ReadEntity.g.cs", readEntitySourceCode);

                if (dbEntityType.SelectQueries.Any() is false)
                {
                    continue;
                }

                var querySourceCode = dbEntityType.BuildQuerySourceCode();
                context.AddSource($"{dbEntityType.FileName}.Query.g.cs", querySourceCode);
            }
        }
    }
}
