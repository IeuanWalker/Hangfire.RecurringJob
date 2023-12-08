using Microsoft.AspNetCore.Builder;

namespace ExampleProject;

public class MockStartup
{
	readonly IApplicationBuilder _app;

	public MockStartup(IApplicationBuilder app)
	{
		_app = app;
	}

	public void Startup()
	{
		_app.AddRecurringJobsFromExampleProject();
	}
}