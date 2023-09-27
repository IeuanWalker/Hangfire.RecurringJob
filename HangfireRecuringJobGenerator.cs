using HangfireRecuringJob;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Immutable;
using System.Text;

namespace IeuanWalker.Hangfire;

[Generator(LanguageNames.CSharp)]
public class HangfireRecuringJobGenerator : IIncrementalGenerator
{
    private static readonly StringBuilder b = new();
    private static string? _assemblyName;
    private const string _attribShortName = "HangfireRecurringJob";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider
            .CreateSyntaxProvider(Match, Transform)
            .Where(static r => r is not null)
            .Collect();

        context.RegisterSourceOutput(provider, Generate!);
    }
    static bool Match(SyntaxNode node, CancellationToken _)
    {
        if (node is not ClassDeclarationSyntax cds || cds.TypeParameterList is not null)
        {
            return false;
        }

        return cds.AttributeLists.Any(al => al.Attributes.Any(a => ExtractName(a.Name)?.Equals(_attribShortName) ?? false));


    }

    static Registration? Transform(GeneratorSyntaxContext context, CancellationToken _)
    {
        var service = context.SemanticModel.GetDeclaredSymbol(context.Node);

        if (service?.IsAbstract is null or true)
            return null;

        _assemblyName = context.SemanticModel.Compilation.AssemblyName;


        string className = service.ToDisplayString() ?? throw new NullReferenceException();

        var attrib = (context.Node as ClassDeclarationSyntax)!
            .AttributeLists
            .SelectMany(al => al.Attributes)
            .First(a => ExtractName(a.Name)!.Equals(_attribShortName));

        if (attrib.ArgumentList!.Arguments.Count != 2)
        {
            throw new Exception($"{_attribShortName} must have 2 paramenters");
        }

        var expressions = attrib
           .ArgumentList!
           .Arguments
           .OfType<AttributeArgumentSyntax>()
           .Select(x => x.Expression)
           .ToList();

        var cron = (string)context.SemanticModel.GetOperation(expressions[0])!.ConstantValue!.Value!;
        var name = (string)context.SemanticModel.GetOperation(expressions[1])!.ConstantValue!.Value!;

        if (string.IsNullOrEmpty(name))
        {
            name = className;
        }

        return new(name, cron);
    }

    static void Generate(SourceProductionContext context, ImmutableArray<Registration> registrations)
    {
        if (!registrations.Any())
            return;

        b.Clear().Append(
"namespace ").Append(_assemblyName).Append(@";

using Hangfire;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection RegisterServicesFrom").Append(_assemblyName?.Sanitize(string.Empty) ?? "Assembly").Append(@"(this IServiceCollection sc)
    {
");
        foreach (var reg in registrations.OrderBy(r => r!.Name))
        {
            b.Append("RecurringJob.AddOrUpdate<").Append(reg.Name).Append(">(\"").Append(reg.Name).Append("\"").Append(", x => x.DeleteOldRegistrations(),\"").Append(reg.Cron).Append("\");");
        }
        b.Append(@"
        return sc;
    }
}");

        var test = b.ToString();
        context.AddSource("ServiceRegistrations.g.cs", SourceText.From(b.ToString(), Encoding.UTF8));
    }

    private sealed class Registration(string name, string cron)
    {
        public string Name { get; } = name;
        public string Cron { get; } = cron;
    };


    private static string? ExtractName(NameSyntax? name)
    {
        return name switch
        {
            SimpleNameSyntax ins => ins.Identifier.Text,
            QualifiedNameSyntax qns => qns.Right.Identifier.Text,
            _ => null
        };
    }
}
