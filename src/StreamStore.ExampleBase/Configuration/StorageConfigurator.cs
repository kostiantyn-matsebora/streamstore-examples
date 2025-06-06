using System;
using Microsoft.Extensions.Hosting;
using StreamStore.Storage;

namespace StreamStore.ExampleBase.Configuration
{
    public sealed class StorageConfigurator
    {
        Action<IHostApplicationBuilder>? single;
        Action<IHostApplicationBuilder>? multi;

        public StorageConfigurator WithSingleMode(Action<IHostApplicationBuilder> configure)
        {
            single = configure;
            return this;
        }

        public StorageConfigurator WithMultitenancy(Action<IHostApplicationBuilder> configure)
        {
            multi = configure;
            return this;
        }

        internal void ConfigureStorage(StreamStorageMode mode, IHostApplicationBuilder hostApplicationBuilder)
        {
            if (mode == StreamStorageMode.Single)
            {
                single!(hostApplicationBuilder);
            }
            else
            {
                multi!(hostApplicationBuilder);
            }
        }
    }
}
