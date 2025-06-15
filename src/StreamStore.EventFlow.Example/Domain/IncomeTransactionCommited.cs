using EventFlow.Aggregates;

namespace StreamStore.EventFlow.Example
{
	public class IncomeTransactionCommited : AggregateEvent<BankAccountAggregate, AccountId>
	{
		public IncomeTransactionCommited(uint amount)
		{
			Amount = amount;
		}

		public uint Amount { get; }
	}
}
