using System.Text.Json;

namespace BoxAndPallets
{
	public static class PalletSerializer
	{
		public static void SerializeToFile(IEnumerable<Pallet> pallets, string path)
		{
			using var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
			JsonSerializer.Serialize(stream, pallets);
		}

		public static IEnumerable<Pallet> DeserializeFromFile(string path)
		{
			if (!File.Exists(path))
			{
				Console.WriteLine("Specified file dit not found");
				return null;
			}

			using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
			return JsonSerializer.Deserialize<IEnumerable<Pallet>>(stream);
		}

		public static string Serialize(IEnumerable<Pallet> pallets)
		{
			return JsonSerializer.Serialize<IEnumerable<Pallet>>(pallets);
		}
	}
}
