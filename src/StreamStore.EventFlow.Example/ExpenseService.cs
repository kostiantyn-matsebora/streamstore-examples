using EventFlow;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StreamStore.EventFlow.Example
{
	internal class ExpenseService : BackgroundService
	{
		readonly ICommandBus commandBus;
		readonly ILogger<ExpenseService> logger;
		readonly AccountId accountId;

		public ExpenseService(ICommandBus commandBus, AccountId accountId, ILogger<ExpenseService> logger)
		{
			this.commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.accountId = accountId ?? throw new ArgumentNullException(nameof(accountId));
		}
		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			logger.LogInformation($"Start spending money from account #{accountId}");

			do
			{
				uint amount = (uint)new Random().Next(5, 200);

				var command = new ExpenseCommand(accountId, amount);

				logger.LogDebug($"Removing money from account, amount={amount}.");

				await commandBus.PublishAsync(command, stoppingToken);

				await Task.Delay(1000);

			} while (!stoppingToken.IsCancellationRequested);
		}
	}
}
