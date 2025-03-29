using Microsoft.CodeAnalysis;

namespace Packt.Shared;

[Generator]
public class MessageSourceGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext execContext)
    {
        IMethodSymbol mainMethod = execContext.Compilation
            .GetEntryPoint(execContext.CancellationToken);

        string typeName = mainMethod.ContainingType.Name;
        string sourceCode = $@"// source-generated code
static partial class {typeName}
{{
    static partial void Message(string message)
    {{
        System.Console.WriteLine($""Generator says: '{{message}}'"");
    }}
}}";

        execContext.AddSource($"{typeName}.Methods.g.cs", sourceCode);
    }

    public void Initialize(GeneratorInitializationContext initContext)
    {
        // this source generator does not need any initialization.
    }
}
