using EventFlow.Core;

namespace StreamStore.EventFlow.Example
{
	public class SumId : Identity<SumId>
	{
		public SumId(string value) : base(value) { }
	}
}
