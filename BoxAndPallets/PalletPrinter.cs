namespace BoxAndPallets
{
	public static class PalletPrinter
	{
		public static void PrintGroupedAndSorted(IEnumerable<Pallet> pallets)
		{
			var groups = pallets.
				Where(x => x.ShelfLife.HasValue)
				.GroupBy(x => x.ShelfLife)
				.OrderBy(x => x.Key);
			foreach(var group in groups)
			{
				Console.WriteLine("ShelfLife {0}",group.Key);
				Console.WriteLine( "\t{0}",PalletSerializer.Serialize(group.OrderBy(x => x.Weight))); 
			}		
		}

		public static void PrintWithMaxShelfTime(IEnumerable<Pallet> pallets)
		{
			var maxShelfTime = pallets.SelectMany(x => x.InnerBoxes).OrderByDescending(x => x.ShelfLife).Take(3);
			pallets = pallets.Where(x => x.InnerBoxes.Any(x => maxShelfTime.Contains(x)));
			Console.WriteLine(pallets.OrderBy(x => x.GetVolume()));
		}
	}
}
