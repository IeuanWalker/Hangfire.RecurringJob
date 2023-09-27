using Microsoft.Extensions.DependencyInjection;

namespace TestSolution;
public class MockStartup
{
	readonly IServiceCollection _serviceCollection;

	public MockStartup(IServiceCollection serviceCollection)
	{
		_serviceCollection = serviceCollection;
	}

	public void Method1()
	{
		_serviceCollection.RegisterRecurringJobsFromTestSolution();
	}
}
