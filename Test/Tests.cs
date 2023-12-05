using IeuanWalker.Hangfire.Attributes;

namespace TestSolution;

[RecurringJob]
public class RecurringJob1
{
	public Task Execute()
	{
		throw new NotImplementedException();
	}
}

//[RecurringJob("* * * *")]
public class RecurringJob2
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}

//[RecurringJob("* * * *")]
public class RecurringJob3
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}

//[RecurringJob("* * * *")]
public class RecurringJob4
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}