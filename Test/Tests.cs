using IeuanWalker.Hangfire.RecurringJob;

namespace TestSolution;

[RecurringJob]
public class RecurringJob1 : IRecurringJobAsync
{
	public Task Execute()
	{
		throw new NotImplementedException();
	}
}

[RecurringJob("* * * *")]
public class RecurringJob2 : IRecurringJob
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}

[RecurringJob("* * * *")]
public class RecurringJob3 : IRecurringJob
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}

[RecurringJob("* * * *")]
public class RecurringJob4 : IRecurringJob
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}