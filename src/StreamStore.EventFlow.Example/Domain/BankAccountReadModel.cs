using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace StreamStore.EventFlow.Example
{
	public class BankAccountReadModel : IReadModel, 
		IAmReadModelFor<BankAccountAggregate, AccountId, IncomeTransactionCommited>,
		IAmReadModelFor<BankAccountAggregate, AccountId, ExpenseTransactionCommited>
	{
		public long Balance { get; private set; } = 0;
		public int TransactionNumber { get; private set; } = 0;
		public Task ApplyAsync(IReadModelContext context, IDomainEvent<BankAccountAggregate, AccountId, IncomeTransactionCommited> domainEvent, CancellationToken cancellationToken)
		{
			Balance = Balance + domainEvent.AggregateEvent.Amount;
			TransactionNumber++;
			return Task.CompletedTask;
		}

		public Task ApplyAsync(IReadModelContext context, IDomainEvent<BankAccountAggregate, AccountId, ExpenseTransactionCommited> domainEvent, CancellationToken cancellationToken)
		{
			Balance = Balance - domainEvent.AggregateEvent.Amount;
			TransactionNumber++;
			return Task.CompletedTask;
		}
	}
}
