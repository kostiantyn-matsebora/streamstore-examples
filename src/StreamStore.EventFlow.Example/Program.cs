using System.Diagnostics.CodeAnalysis;
using EventFlow.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StreamStore.Sql.Sqlite;
using StreamStore.Storage.EventFlow;


namespace StreamStore.EventFlow.Example
{
	[ExcludeFromCodeCoverage]
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = Host.CreateApplicationBuilder(args);
			builder.Services.AddHostedService<Service>();
			builder.Services.AddEventFlow(c =>
				c
				 .UseStreamStorageEventStore(x => x.UseSqlite(c => c.WithConnectionString("Data Source = streamstore; Version = 3;")))
				 .UseInMemoryReadStoreFor<SumReadModel>()
				 .UseInMemorySnapshotPersistence()
				 .AddEvents(typeof(NumberAdded))
				 .AddCommands(typeof(AddNumberCommand))
				 .AddCommandHandlers(typeof(AddNumberCommandHandler))
			);

			var host = builder.Build();
			await host.RunAsync();
		}

	}
}