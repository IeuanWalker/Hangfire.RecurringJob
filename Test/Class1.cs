using Hangfire;
using Microsoft.Extensions.DependencyInjection;

namespace TestSolution;

[RecurringJob]
public class DeleteOldReports
{
    public void Execute()
    {
    }
}

[RecurringJob("* * * *")]
public class SendConfirmationEmails
{
    public void Execute()
    {
        Cron.Daily();
    }
}

public class Class2
{
    readonly IServiceCollection _serviceCollection;
    public Class2(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }
    public void Method1()
    {
        _serviceCollection.RegisterServicesFromTestSolution();
    }
}



