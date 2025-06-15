using EventFlow;
using EventFlow.Queries;
using Microsoft.Extensions.Hosting;
using StreamStore.Provisioning;

namespace StreamStore.EventFlow.Example
{
	internal class Service : BackgroundService
	{
		readonly ISchemaProvisioner provisioner;
		readonly ICommandBus commandBus;
		readonly IQueryProcessor queryProcessor;		
		readonly SumId aggregateId = new SumId("sum-719FFC65-09A4-49D5-A973-03103F90A3C4".ToLower());

		public Service(ISchemaProvisioner provisioner, ICommandBus commandBus, IQueryProcessor queryProcessor)
		{
			this.provisioner = provisioner;
			this.commandBus = commandBus;
			this.queryProcessor = queryProcessor;
		}


		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			Console.WriteLine("Provisioning storage");
			await provisioner.ProvisionSchemaAsync(stoppingToken);

			do
			{
				Console.WriteLine("Reading aggregate");
				var model = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SumReadModel>(aggregateId), CancellationToken.None);

				if (model != null)
				{
					Console.WriteLine($"SequenceNumber: {model.SequenceNumber}");
					Console.WriteLine($"Sum: {model.Sum}");
				}

				int number = new Random().Next(1, 100);

				Console.WriteLine($"Adding: {number}");
				var command = new AddNumberCommand(aggregateId, number);
				await commandBus.PublishAsync(command, stoppingToken);
				
				await Task.Delay(1000);
			} while (!stoppingToken.IsCancellationRequested);
		}
	}
}
