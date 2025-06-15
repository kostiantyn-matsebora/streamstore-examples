using EventFlow.Aggregates.ExecutionResults;
using EventFlow.Commands;

namespace StreamStore.EventFlow.Example
{
	public class ExpenseCommand : Command<BankAccountAggregate, AccountId, IExecutionResult>
	{
		public ExpenseCommand(
			AccountId aggregateId,
			uint amount)
			: base(aggregateId)
		{
			Amount = amount;
		}

		public uint Amount { get; }
	}
}
