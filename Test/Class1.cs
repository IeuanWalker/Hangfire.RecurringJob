using IeuanWalker.Hangfire.RecurringJob;
using Microsoft.Extensions.DependencyInjection;

namespace TestSolution;

[RecurringJob]
public class DeleteOldReports : IRecurringJobAsync
{
    public Task Execute()
    {
        throw new NotImplementedException();
    }
}

[RecurringJob("* * * *")]
public class SendConfirmationEmails : IRecurringJob
{
    public void Execute()
    {
        throw new NotImplementedException();
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
        _serviceCollection.RegisterRecurringJobsFromTestSolution();
    }
}