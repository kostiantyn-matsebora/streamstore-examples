using System.Data.SQLite;
using StreamStore.Extensions;

namespace StreamStore.Sql.Example
{
	internal class SqliteExampleStorage: ISqlExampleStorage
	{
		readonly string storageName;
		private bool disposedValue;

		public string ConnectionString { get; }

		public SqliteExampleStorage(string storageName)
		{
			this.storageName = storageName.ThrowIfNull(nameof(storageName));
			ConnectionString = $"Data Source = {storageName}; Version = 3;";
		}

		public bool EnsureExists()
		{
			SQLiteConnection.CreateFile(storageName);
			return true;
		}

		private void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					try
					{
						File.Delete(storageName);
					}
					catch
					{
						// ignored
					}
				}
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
