using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace IeuanWalker.Hangfire.Helpers;

static class NameSyntaxExtentions
{
	internal static string? ExtractName(this NameSyntax? name)
	{
		return name switch
		{
			SimpleNameSyntax ins => ins.Identifier.Text,
			QualifiedNameSyntax qns => qns.Right.Identifier.Text,
			_ => null
		};
	}
}