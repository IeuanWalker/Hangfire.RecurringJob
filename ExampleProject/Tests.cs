using IeuanWalker.Hangfire.RecurringJob.Attributes;

namespace ExampleProject;

[RecurringJob]
public class RecurringJob1
{
	public Task Execute()
	{
		throw new NotImplementedException();
	}
}

[RecurringJob("* * * *")]
public class RecurringJob2
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}

[RecurringJob("* * * *", "Priority")]
public class RecurringJob3
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}

[RecurringJob]
[RecurringJob("*/5 * * * *", "GMT Standard Time", "Priority", "DataRetention")]
public class RecurringJob4
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}

/// <summary>
/// Fix for reported issue - https://github.com/IeuanWalker/Hangfire.RecurringJob/issues/5
/// </summary>
[RecurringJob("*/5 * * * *", "GMT Standard Time", "Priority", "Data\"Retention")]
public class RecurringJob5
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}

//[RecurringJob]
//public class RecurringJob6
//{
//	public void MissingExecuteMethod()
//	{
//		throw new NotImplementedException();
//	}
//}

//[RecurringJob("*/5 * * * *", "ABC", "Priority", "DataRetention")]
//public class RecurringJob7
//{
//	public void Execute()
//	{
//		throw new NotImplementedException();
//	}
//}

//[RecurringJob("*/5 * * * *", "ABC", "Priority", "DataRetention")]
//public class RecurringJob8
//{
//	public void AllErrors()
//	{
//		throw new NotImplementedException();
//	}
//}