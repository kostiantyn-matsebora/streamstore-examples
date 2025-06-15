using EventFlow.Core;

namespace StreamStore.EventFlow.Example
{
	public class AccountId : Identity<AccountId>
	{
		public AccountId(string value) : base(value) { }
	}
}
