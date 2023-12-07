using IeuanWalker.Hangfire.RecurringJob.Attributes;

namespace ExampleProject;

[RecurringJob("* * * *")]
public class RecurringJob1
{
	public Task Execute()
	{
		throw new NotImplementedException();
	}
}

[RecurringJob("* * * *", "Priority")]
public class RecurringJob2
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}

[RecurringJob]
[RecurringJob("*/5 * * * *", "GMT", "Priority", "DataRetention")]
public class RecurringJob3
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}

[RecurringJob("* * * *")]
public class RecurringJob4
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}