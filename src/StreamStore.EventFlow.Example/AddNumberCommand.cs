using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace StreamStore.EventFlow.Example
{
	public class AddNumberCommand : Command<SumAggregate, SumId, IExecutionResult>
	{
		public AddNumberCommand(
			SumId aggregateId,
			int number)
			: base(aggregateId)
		{
			Number = number;
		}

		public int Number { get; }
	}
}
