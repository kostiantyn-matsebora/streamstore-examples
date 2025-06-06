using System.CommandLine;
using System.Diagnostics.CodeAnalysis;
using StreamStore.ExampleBase;
using StreamStore.S3.AWS;
using StreamStore.S3.B2;

namespace StreamStore.S3.Example
{
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Janitor>();

            await builder
                .ConfigureExampleApplication(c => 
                    c.AddStorage(S3Storages.AWS, x => x.WithSingleMode(UseAWSStorage))
                     .AddStorage(S3Storages.B2, x => x.WithSingleMode(UseB2Storage)))
                .InvokeAsync(args);
        }

        static void UseAWSStorage(IHostApplicationBuilder builder)
        {
            builder
                .Services
                .AddStreamStore(x =>
                    x.ConfigureStreamPersistence(c => 
                        c.AddAmazonS3()));
        }

        static void UseB2Storage(IHostApplicationBuilder builder)
        {
			builder
				.Services
				.AddStreamStore(x =>
					x.ConfigureStreamPersistence(c => c.AddB2()));
		}
    }
}