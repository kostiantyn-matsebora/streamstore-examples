using System.Diagnostics.CodeAnalysis;
using EventFlow.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using StreamStore.EventFlow.Example.Persistence;
using StreamStore.NoSql.Cassandra;
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

			builder.Logging
				 .AddSimpleConsole(configure =>
				 {
					 configure.SingleLine = true;
					 configure.ColorBehavior = LoggerColorBehavior.Enabled;
					 configure.IncludeScopes = true;
				 });

			builder.Services.AddSingleton(AccountId.New);

			builder.Services.AddEventFlow(c =>
				c.UseStreamStorageEventStore(x =>

				   x.UseSqlite(c => c.WithConnectionString("Data Source = streamstore; Version = 3;"))

					// If you want to use Cassandra comment line above and uncomment line below. 
					// You can use docker-compose to run Cassandra cluster locally.
				    // x.UseCassandra(c => c.ConfigureCluster(x => x.AddContactPoint("localhost")))
				 )
				 .UseInMemoryReadStoreFor<BankAccountReadModel>()
				 .UseInMemorySnapshotPersistence()
				 .AddEvents(
					  typeof(IncomeTransactionCommited)
					 , typeof(ExpenseTransactionCommited))
				 .AddCommands(
						typeof(IncomeCommand)
					   , typeof(ExpenseCommand))
				 .AddCommandHandlers(
					typeof(IncomeCommandHandler)
				   , typeof(ExpenseCommandHandler))
			);

			builder.Services.AddHostedService<ProvisioningService>();
			builder.Services.AddHostedService<IncomeService>();
			builder.Services.AddHostedService<ExpenseService>();
			builder.Services.AddHostedService<BalanceService>();

			// Uncomment if you decide to use Cassandra
			// ProvisionCassandraKeyspace();

			var host = builder.Build();
			await host.RunAsync();
		}


		static void ProvisionCassandraKeyspace()
		{
			new CassandraKeyspace(KeyspaceConfiguration.Default, x => x.AddContactPoint("localhost")).EnsureExists();
		}
	}
}