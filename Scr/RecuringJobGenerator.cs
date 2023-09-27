﻿using System.Collections.Immutable;
using System.Text;
using IeuanWalker.Hangfire.Attributes;
using IeuanWalker.Hangfire.Helpers;
using IeuanWalker.Hangfire.Interfaces;
using IeuanWalker.Hangfire.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace IeuanWalker.Hangfire;

[Generator(LanguageNames.CSharp)]
public class RecuringJobGenerator : IIncrementalGenerator
{
	static readonly StringBuilder b = new();
	static string? assemblyName;
	const string attribShortName = "RecurringJob";

	/// <summary>
	/// Starts the generator
	/// </summary>
	/// <param name="context"></param>
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		// Constant classes/ interfaces for the users to use
		context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
			"RecurringJobAttribute.g.cs",
			SourceText.From(RecurringJobAttribute.Attribute, Encoding.UTF8)));
		context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
			"IRecurringJob.g.cs",
			SourceText.From(RecurringJobInterface.InterfaceVoid, Encoding.UTF8)));
		context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
			"IRecurringJobAsync.g.cs",
			SourceText.From(RecurringJobInterface.InterfaceAsync, Encoding.UTF8)));

		// Generator implementation
		//var provider = context.SyntaxProvider
		//	.CreateSyntaxProvider(Match, Transform)
		//	.Where(static r => r is not null)
		//	.Collect();

		//context.RegisterSourceOutput(provider, Generate!);
	}

	/// <summary>
	/// Find all valid classes with the <see cref="RecurringJobAttribute"/> attribute
	/// </summary>
	/// <param name="node"></param>
	/// <param name="_"></param>
	static bool Match(SyntaxNode node, CancellationToken _)
	{
		if (node is not ClassDeclarationSyntax cds || cds.TypeParameterList is not null)
		{
			return false;
		}

		return cds.AttributeLists.Any(al => al.Attributes.Any(a => a.Name.ExtractName()?.Equals(attribShortName) ?? false));
	}

	/// <summary>
	/// Transforms all matched classes into <see cref="JobModel"/>s
	/// </summary>
	/// <param name="context"></param>
	/// <param name="_"></param>
	/// <exception cref="NullReferenceException"></exception>
	/// <exception cref="Exception"></exception>
	static JobModel? Transform(GeneratorSyntaxContext context, CancellationToken _)
	{
		var service = context.SemanticModel.GetDeclaredSymbol(context.Node);

		if (service?.IsAbstract is null or true)
		{
			return null;
		}

		assemblyName = context.SemanticModel.Compilation.AssemblyName;

		string fullClassName = service.ToDisplayString() ?? throw new NullReferenceException();

		var attrib = (context.Node as ClassDeclarationSyntax)!
			.AttributeLists
			.SelectMany(al => al.Attributes)
			.First(a => a.Name.ExtractName()!.Equals(attribShortName));

		if (attrib.ArgumentList!.Arguments.Count != 1 && attrib.ArgumentList!.Arguments.Count != 2)
		{
			throw new Exception($"{attribShortName} must have 2 paramenters");
		}

		var expressions = attrib
		   .ArgumentList!
		   .Arguments
		   .OfType<AttributeArgumentSyntax>()
		   .Select(x => x.Expression)
		   .ToList();

		string jobId = string.Empty;
		string cron = string.Empty;
		if (expressions.Count == 1)
		{
			cron = (string)context.SemanticModel.GetOperation(expressions[0])!.ConstantValue!.Value!;
		}
		else
		{
			jobId = (string)context.SemanticModel.GetOperation(expressions[0])!.ConstantValue!.Value!;
			cron = (string)context.SemanticModel.GetOperation(expressions[1])!.ConstantValue!.Value!;
		}

		if (string.IsNullOrEmpty(jobId))
		{
			jobId = fullClassName;
		}

		return new(fullClassName, jobId, cron, "", "");
	}

	/// <summary>
	/// Generates registration code for all <see cref="JobModel"/>
	/// </summary>
	/// <param name="context"></param>
	/// <param name="jobs"></param>
	static void Generate(SourceProductionContext context, ImmutableArray<JobModel> jobs)
	{
		if (!jobs.Any())
		{
			return;
		}

		b.Clear().Append(
"namespace ").Append(assemblyName).Append(@";

using Hangfire;
using Microsoft.Extensions.DependencyInjection;

public static class RecurringJobRegistrationExtensions
{
    public static IServiceCollection RegisterRecurringJobsFrom").Append(assemblyName?.Sanitize(string.Empty) ?? "Assembly").Append(@"(this IServiceCollection sc)
    {
");
		foreach (var job in jobs.OrderBy(r => r!.JobId))
		{
			b.Append("\t\tRecurringJob.AddOrUpdate<").Append(job.FullClassName).Append(">(\"").Append(job.JobId).Append("\"").Append(", x => x.Execute(), \"").Append(job.Cron).Append("\");").Append("\r\n");
		}
		b.Append(@"
        return sc;
    }
}");
		string test = b.ToString();
		context.AddSource("RecurringJobRegistrationExtensions.g.cs", SourceText.From(b.ToString(), Encoding.UTF8));
	}
}