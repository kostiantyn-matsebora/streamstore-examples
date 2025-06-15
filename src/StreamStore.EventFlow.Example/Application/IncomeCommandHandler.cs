
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace StreamStore.EventFlow.Example
{
	public class IncomeCommandHandler : CommandHandler<BankAccountAggregate, AccountId, IExecutionResult, IncomeCommand>
	{
		public override Task<IExecutionResult> ExecuteCommandAsync(
			BankAccountAggregate aggregate,
			IncomeCommand command,
			CancellationToken cancellationToken)
		{
			var executionResult = aggregate.Income(command.Amount);
			return Task.FromResult(executionResult);
		}
	}
}