using StreamStore.ExampleBase;

namespace StreamStore.Sql.Example
{
	internal interface ISqlExampleStorage: IExampleStorage
	{
		string ConnectionString { get; }
	}
}
