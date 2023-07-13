using Quartz;

namespace WebApi.jobs;

public static class JobExtensions
{
    public static void AddJobs(this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            var jobKey = new JobKey("CleanUpSignalRConnectionsJob");
            q.AddJob<CleanUpSignalRConnectionsJob>(opts => opts.WithIdentity(jobKey));
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("CleanUpSignalRConnectionsJob-trigger")
                .WithCronSchedule("0 1 * * *"));
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }
}