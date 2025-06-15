
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace StreamStore.EventFlow.Example
{
	public class AddNumberCommandHandler : CommandHandler<SumAggregate, SumId, IExecutionResult, AddNumberCommand>
	{
		public override Task<IExecutionResult> ExecuteCommandAsync(
			SumAggregate aggregate,
			AddNumberCommand command,
			CancellationToken cancellationToken)
		{
			var executionResult = aggregate.AddNumber(command.Number);
			return Task.FromResult(executionResult);
		}
	}
}