using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace ExampleProject;

public class MockStartup
{
	readonly IServiceCollection _serviceCollection;

	public MockStartup(IServiceCollection serviceCollection)
	{
		_serviceCollection = serviceCollection;
	}

	public void Method1()
	{
		_serviceCollection.RegisterRecurringJobsFromExampleProject();

		RecurringJob.AddOrUpdate<RecurringJob1>("RecurringJob1", "", x => x.Execute(), "0 0 * * *", new RecurringJobOptions
		{
			TimeZone = TimeZoneInfo.FindSystemTimeZoneById("UTC")
		});
	}
}