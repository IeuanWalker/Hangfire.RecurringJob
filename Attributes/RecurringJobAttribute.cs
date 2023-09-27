namespace IeuanWalker.Hangfire.Attributes
{
    public static class RecurringJobAttribute
    {
        public const string Attribute =
@"namespace IeuanWalker.Hangfire.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class RecurringJobAttribute : Attribute
{
    private readonly string? _name;
    private readonly string _cron;

    public RecurringJobAttribute(string name, string cron)
    {
        _name = name;
        _cron = cron;
    }

    public RecurringJobAttribute(string cron)
    {
        _cron = cron;
    }
}";
    }
}
