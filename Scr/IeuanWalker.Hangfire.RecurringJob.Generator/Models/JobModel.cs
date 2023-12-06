namespace IeuanWalker.Hangfire.RecurringJob.Generator.Models;

sealed class JobModel
{
	public JobModel(string fullClassName, string jobId, string cron, string queue, string timeZone)
	{
		FullClassName = fullClassName;
		JobId = jobId;
		Cron = cron;
		Queue = queue;
		TimeZone = timeZone;
	}

	public string FullClassName { get; }
	public string JobId { get; }
	public string Cron { get; }
	public string Queue { get; }
	public string TimeZone { get; }
}