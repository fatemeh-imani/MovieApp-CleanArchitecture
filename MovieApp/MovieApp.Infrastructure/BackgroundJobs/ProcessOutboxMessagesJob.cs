using MediatR;
using Microsoft.EntityFrameworkCore;
using MovieApp.Infrastructure.Persistence;
using MovieApp.Infrastructure.Persistence.Entites;
using MovieApp.SharedKernel.Event;
using Newtonsoft.Json;
using Quartz;

namespace MovieApp.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public sealed class ProcessOutboxMessagesJob(
    ApplicationDbContext dbContext,
    IPublisher publisher) : IJob
{
    public async Task Execute(
        IJobExecutionContext context)
    {
        var messages = await dbContext
            .Set<OutboxMessage>()
            .Where(x => x.ProcessedOn == null)
            .OrderBy(x => x.OccurredOn)
            .Take(20)
            .ToListAsync();

        foreach (var message in messages)
        {
            var domainEvent =
                JsonConvert.DeserializeObject<IDomainEvent>(
                    message.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainEvent is null)
            {
                continue;
            }

            await publisher.Publish(domainEvent);

            message.ProcessedOn = DateTime.UtcNow;
        }

        await dbContext.SaveChangesAsync();
    }
}