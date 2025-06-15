using EventFlow.Aggregates;
using EventFlow.Aggregates.ExecutionResults;

namespace StreamStore.EventFlow.Example
{
	public class BankAccountAggregate : AggregateRoot<BankAccountAggregate, AccountId>, IEmit<IncomeTransactionCommited>
	{

		long balance = 0;

		public BankAccountAggregate(AccountId id) : base(id) { }

		// Method invoked by our command
		public IExecutionResult Income(uint amount)
		{
			Emit(new IncomeTransactionCommited(amount));

			return ExecutionResult.Success();
		}

		public IExecutionResult Expense(uint amount)
		{
			Emit(new ExpenseTransactionCommited(amount));

			return ExecutionResult.Success();
		}

		// We apply the event as part of the event sourcing system. EventFlow
		// provides several different methods for doing this, e.g. state objects,
		// the Apply method is merely the simplest
		public void Apply(IncomeTransactionCommited aggregateEvent)
		{
			balance = balance + aggregateEvent.Amount;
		}

		public void Apply(ExpenseTransactionCommited aggregateEvent)
		{
			balance = balance - aggregateEvent.Amount;
		}
	}
}
