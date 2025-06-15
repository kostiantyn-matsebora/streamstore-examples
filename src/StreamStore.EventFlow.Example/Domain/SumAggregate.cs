using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;

namespace StreamStore.EventFlow.Example
{
	public class SumAggregate : AggregateRoot<SumAggregate, SumId>, IEmit<NumberAdded>
	{

		int sum = 0;

		public SumAggregate(SumId id) : base(id) { }

		// Method invoked by our command
		public IExecutionResult AddNumber(int number)
		{
			Emit(new NumberAdded(number));

			return ExecutionResult.Success();
		}

		// We apply the event as part of the event sourcing system. EventFlow
		// provides several different methods for doing this, e.g. state objects,
		// the Apply method is merely the simplest
		public void Apply(NumberAdded aggregateEvent)
		{
			sum = sum + aggregateEvent.Number;
		}
	}
}
