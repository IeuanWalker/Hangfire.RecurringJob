﻿using Hangfire.States;

namespace IeuanWalker.Hangfire.RecurringJob.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class RecurringJobAttribute : Attribute
{
	/// <summary>
	/// The identifier of the RecurringJob
	/// </summary>
	public string? JobId { get; set; }

	/// <summary>
	/// Cron expressions
	/// </summary>
	public string Cron { get; set; }

	/// <summary>
	/// Queue name
	/// </summary>
	public string Queue { get; set; }

	/// <summary>
	/// Converts to <see cref="TimeZoneInfo"/> via method <seealso cref="TimeZoneInfo.FindSystemTimeZoneById(string)"/>,
	/// default value is <see cref="TimeZoneInfo.Utc"/>
	/// </summary>
	public string TimeZone { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="RecurringJobAttribute"/>
	/// </summary>
	public RecurringJobAttribute() : this("0 0 * * *", EnqueuedState.DefaultQueue) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="RecurringJobAttribute"/>
	/// </summary>
	/// <param name="cron">Cron expressions</param>
	public RecurringJobAttribute(string cron) : this(cron, EnqueuedState.DefaultQueue) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="RecurringJobAttribute"/>
	/// </summary>
	/// <param name="cron">Cron expressions</param>
	/// <param name="queue">Queue name</param>
	public RecurringJobAttribute(string cron, string queue) : this(cron, "UTC", queue) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="RecurringJobAttribute"/>
	/// </summary>
	/// <param name="cron">Cron expressions</param>
	/// <param name="timeZone">Converts to <see cref="TimeZoneInfo"/> via method <seealso cref="TimeZoneInfo.FindSystemTimeZoneById(string)"/>.</param>
	/// <param name="queue">Queue name</param>
	public RecurringJobAttribute(string cron, string timeZone, string queue, string? jobId = null)
	{
		Cron = cron;
		TimeZone = timeZone;
		Queue = queue;
		JobId = jobId;
	}
}