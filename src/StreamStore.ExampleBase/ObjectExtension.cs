using System;
using StreamStore.Extensions;
using StreamStore.Storage;

namespace StreamStore.ExampleBase
{
	internal static class ObjectExtension
	{
		public static StreamStorageMode ToStorageMode(this object value)
		{
			value.ThrowIfNull(nameof(value));
			switch (value.ToString())
			{
				case "single":
					return StreamStorageMode.Single;
				case "multitenancy":
					return StreamStorageMode.Multitenant;
				default:
					throw new NotSupportedException($"{value} mode is not supported.");
			}
		}
	}
}
