
using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace StreamStore.EventFlow.Example
{
	public class ExpenseCommandHandler : CommandHandler<BankAccountAggregate, AccountId, IExecutionResult, ExpenseCommand>
	{
		public override Task<IExecutionResult> ExecuteCommandAsync(
			BankAccountAggregate aggregate,
			ExpenseCommand command,
			CancellationToken cancellationToken)
		{
			var executionResult = aggregate.Expense(command.Amount);
			return Task.FromResult(executionResult);
		}
	}
}