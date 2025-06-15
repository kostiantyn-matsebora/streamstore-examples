
using EventFlow.Queries;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StreamStore.EventFlow.Example
{
	internal class BalanceService : BackgroundService
	{
		readonly IQueryProcessor queryProcessor;
		readonly ILogger<BalanceService> logger;
		readonly AccountId accountId;

		public BalanceService(IQueryProcessor queryProcessor, AccountId accountId, ILogger<BalanceService> logger)
		{
			this.queryProcessor = queryProcessor ?? throw new ArgumentNullException(nameof(queryProcessor));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.accountId = accountId ?? throw new ArgumentNullException(nameof(accountId));
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{

			logger.LogInformation($"Start checking balance of bank account #{accountId}");

			do {
				logger.LogDebug("Checking balance of account...");

				var account = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<BankAccountReadModel>(accountId), CancellationToken.None);

				if (account == null)
				{
					logger.LogDebug("Account is empty.");
				} else
				{
					logger.LogDebug($"Balance: {account.Balance}");
					logger.LogDebug($"Number of transactions: {account.TransactionNumber}");
				}

				await Task.Delay(2000);

			} while (!stoppingToken.IsCancellationRequested);

		}
	}
}
