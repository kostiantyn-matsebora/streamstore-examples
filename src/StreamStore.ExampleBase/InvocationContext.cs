using System.Diagnostics.CodeAnalysis;
using StreamStore.Storage;

namespace StreamStore.ExampleBase
{
    [ExcludeFromCodeCoverage]
    public sealed class InvocationContext
    {
        public InvocationContext(StreamStorageMode mode, string storage)
        {
            this.Mode = mode;
            this.Storage = storage;
        }

        public string Storage { get;  }
        public StreamStorageMode Mode { get; }
    }
}
