namespace BoxAndPallets
{
	public class Box : InventoryObject
	{
		public DateOnly? Produced { get; set; }
		private DateOnly? _shelfLife { get; set; }
		public DateOnly ShelfLife
		{
			get
			{
				return _shelfLife ?? Produced.Value.AddDays(100);
			}
		}
	}
}
