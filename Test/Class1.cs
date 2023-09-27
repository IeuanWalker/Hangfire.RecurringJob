using IeuanWalker.Hangfire.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace TestSolution;

[RecurringJob("DeleteOldReportsAtMidnight", "* *678 * *")]
public class DeleteOldReports
{
    public void Execute()
    {
    }
}

[RecurringJob("* *678 * *")]
public class SendConfirmationEmails
{
    public void Execute()
    {
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


