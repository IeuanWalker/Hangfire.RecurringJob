namespace IeuanWalker.Hangfire.Interfaces;

public static class RecurringJobInterface
{
	public const string InterfaceVoid =
@"namespace IeuanWalker.Hangfire.RecurringJob;

public interface IRecurringJob
{
    void Execute();
}";

	public const string InterfaceAsync =
@"namespace IeuanWalker.Hangfire.RecurringJob;

public interface IRecurringJobAsync
{
    Task Execute();
}";
}