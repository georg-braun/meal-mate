using Microsoft.AspNetCore.SignalR;
using Quartz;
using WebApi.hubs;

namespace WebApi.jobs;

public class CleanUpSignalRConnectionsJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var expiredConnections =
            MealMateHub.Connections.Where(_ => _.ConnectedAt < DateTime.UtcNow.AddDays(-1)).ToList();
        Console.WriteLine("Cleaning up old SignalR connections...");
        foreach (var connection in expiredConnections)
        {
            await _hubContext.Groups.RemoveFromGroupAsync(connection.ConnectionId, connection.GroupId);
            MealMateHub.Connections.Remove(connection);
        }
    }

    private readonly IHubContext<MealMateHub> _hubContext;

    public CleanUpSignalRConnectionsJob(IHubContext<MealMateHub> hubContext)
    {
        _hubContext = hubContext;
    }
}