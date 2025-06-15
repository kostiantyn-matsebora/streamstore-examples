using EventFlow;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace StreamStore.EventFlow.Example
{
	internal class IncomeService : BackgroundService
	{
		readonly ICommandBus commandBus;
		readonly ILogger<IncomeService> logger;
		readonly AccountId accountId;


		public IncomeService(ICommandBus commandBus, AccountId accountId, ILogger<IncomeService> logger)
		{
			this.commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.accountId = accountId ?? throw new ArgumentNullException(nameof(accountId));
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			logger.LogInformation($"Start adding money to account #{accountId}");

			do
			{
				uint amount = (uint)new Random().Next(50, 100);

				logger.LogDebug($"Adding money to account, amount={amount}.");

				var command = new IncomeCommand(accountId, amount);

				await commandBus.PublishAsync(command, stoppingToken);
				
				await Task.Delay(500);

			} while (!stoppingToken.IsCancellationRequested);
		}
	}
}
