using EventFlow.Aggregates;

namespace StreamStore.EventFlow.Example
{
	public class ExpenseTransactionCommited : AggregateEvent<BankAccountAggregate, AccountId>
	{
		public ExpenseTransactionCommited(uint amount)
		{
			Amount = amount;
		}

		public uint Amount { get; }
	}
}