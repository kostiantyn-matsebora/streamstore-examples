using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace StreamStore.EventFlow.Example
{
	public class SumReadModel : IReadModel, IAmReadModelFor<SumAggregate, SumId, NumberAdded>
	{
		public int Sum { get; private set; } = 0;
		public int SequenceNumber { get; private set; } = 0;
		public Task ApplyAsync(IReadModelContext context, IDomainEvent<SumAggregate, SumId, NumberAdded> domainEvent, CancellationToken cancellationToken)
		{
			Sum = Sum + domainEvent.AggregateEvent.Number;
			SequenceNumber = domainEvent.AggregateSequenceNumber;
			return Task.CompletedTask;
		}
	}
}
