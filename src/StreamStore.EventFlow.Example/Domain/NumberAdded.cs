using EventFlow.Aggregates;

namespace StreamStore.EventFlow.Example
{
	public class NumberAdded : AggregateEvent<SumAggregate, SumId>
	{
		public NumberAdded(int number)
		{
			Number = number;
		}

		public int Number { get; }
	}
}
