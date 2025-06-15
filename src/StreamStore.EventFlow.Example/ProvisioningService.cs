using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StreamStore.Provisioning;

namespace StreamStore.EventFlow.Example
{
	internal class ProvisioningService : BackgroundService
	{
		readonly ISchemaProvisioner provisioner;
		readonly ILogger<ProvisioningService> logger;

		public ProvisioningService(ISchemaProvisioner provisioner, ILogger<ProvisioningService> logger)
		{
			this.provisioner = provisioner ?? throw new ArgumentNullException(nameof(provisioner));
			this.logger = logger ?? throw new ArgumentNullException(nameof(provisioner)); ;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			logger.LogInformation("Provisioning schema");
			await provisioner.ProvisionSchemaAsync(stoppingToken);
		}
	}
}
