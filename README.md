# Hangfire.RecurringJob

This is a package that automatically generates the hanfire recurring jobs `AddOrUpdate` code, using source generators.

## How to use it?
1. Install the [NuGet package](#) into your project.
```
Install-Package IeuanWalker.Hangfire.RecurringJob
```

2. Add the `RecurringJob` attribute to a class, and create a `Execute()` method.
```csharp
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
[RecurringJob("*/5 * * * *", "GMT", "Priority", "DataRetention")]
public class RecurringJob4
{
	public void Execute()
	{
		throw new NotImplementedException();
	}
}
```
3. Register the recurring jobs
> Once a `RecurringJob` attribute has been added to a class in your project a exention method for `IApplicationBuilder` will automatically be created.
> The extention method name convention is AddRecurringJobsFrom + your assebly name.
```csharp
app.AddRecurringJobsFromExampleProject();
```

## Example
Here is an example of what it looks like in use - 
> Left is example code, right is the generated code

![image](https://github.com/IeuanWalker/Hangfire.RecurringJob.Generator/assets/6544051/cef12771-5178-46cf-9264-dbb54654efc6)


