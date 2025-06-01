using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using StreamStore.ExampleBase;
using StreamStore.Sql.Sqlite;
using StreamStore.Sql.PostgreSql;
using System.CommandLine;


namespace StreamStore.Sql.Example
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            await builder
                .ConfigureExampleApplication(c =>
                    c.EnableMultitenancy()
                     .AddStorage(SqlStorages.SQLite,
                                  x => 
                                    x.WithSingleMode(ConfigureSqliteSingle)
                                     .WithMultitenancy(ConfigureSqliteMultitenancy))
                     .AddStorage(SqlStorages.PostgreSQL,
                                   x => 
                                       x.WithSingleMode(ConfigurePostgresSingle)
                                        .WithMultitenancy(ConfigurePostgresMultitenancy)))
                .InvokeAsync(args);
        }

        static void ConfigureSqliteSingle(IHostApplicationBuilder builder)
        {
            var storage = new SqliteExampleStorage(Tenants.Default);
            storage.EnsureExists();

            builder
                .Services
                .ConfigureStreamStore(x =>
                    x.EnableSchemaProvisioning()
                     .WithSingleStorage(c =>
                        c.UseSqliteStorage(x => x.ConfigureStorage(c =>
                            c.WithConnectionString(storage.ConnectionString)))));
        }

        static void ConfigurePostgresSingle(IHostApplicationBuilder builder)
        {
            var storage = new PostgresExampleStorage(Tenants.Default);
            storage.EnsureExists();

            builder
                .Services
                .ConfigureStreamStore(x =>
                    x.EnableSchemaProvisioning()
                     .WithSingleStorage(c =>
                        c.UsePostgresStorage(x => x.ConfigureStorage(c => 
                            c.WithConnectionString(storage.ConnectionString)))));
        }

        static void ConfigureSqliteMultitenancy(IHostApplicationBuilder builder)
        {
            var connectionString1 = EnsureStorageExists(new SqliteExampleStorage(Tenants.Tenant1));
            var connectionString2 = EnsureStorageExists(new SqliteExampleStorage(Tenants.Tenant2));
            var connectionString3 = EnsureStorageExists(new SqliteExampleStorage(Tenants.Tenant3));

            builder
                .Services
                .ConfigureStreamStore(x =>
                    x.EnableSchemaProvisioning()
                     .WithMultitenancy(c => 
                            c.WithTenants(Tenants.Tenant1, Tenants.Tenant2, Tenants.Tenant3)
                             .UseSqliteStorage(x => 
                                    x.WithConnectionString(Tenants.Tenant1, connectionString1)
                                     .WithConnectionString(Tenants.Tenant2, connectionString2)
                                     .WithConnectionString(Tenants.Tenant3, connectionString3))));
        }
        static void ConfigurePostgresMultitenancy(IHostApplicationBuilder builder)
        {
            var connectionString1 = EnsureStorageExists(new PostgresExampleStorage(Tenants.Tenant1));
            var connectionString2 = EnsureStorageExists(new PostgresExampleStorage(Tenants.Tenant2));
            var connectionString3 = EnsureStorageExists(new PostgresExampleStorage(Tenants.Tenant3));

            builder
                .Services
                .ConfigureStreamStore(x =>
                    x.EnableSchemaProvisioning()
                     .WithMultitenancy(c =>
                            c.WithTenants(Tenants.Tenant1, Tenants.Tenant2, Tenants.Tenant3)
                             .UsePostgresStorage(x =>
                                    x.WithConnectionString(Tenants.Tenant1, connectionString1)
                                     .WithConnectionString(Tenants.Tenant2, connectionString2)
                                     .WithConnectionString(Tenants.Tenant3, connectionString3))));
        }

        static string EnsureStorageExists(ISqlExampleStorage storage)
        {
            var result = storage.EnsureExists();
            if (result == false)
            {
                throw new InvalidOperationException($"Failed to create storage {storage.ConnectionString}");
            }

            return storage.ConnectionString;
        }
    }
}
