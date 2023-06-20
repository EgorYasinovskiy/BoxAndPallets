namespace BoxAndPallets
{
	public class Box : InventoryObject
	{
		public DateOnly? Produced { get; set; }
		private readonly DateOnly? _shelfLife;
		public DateOnly ShelfLife
		{
			get
			{
				return _shelfLife ?? Produced.Value.AddDays(100);
			}
		}

		public Box() : base() { }

		public Box(DateOnly produced,int id, int widht, int lenght, int height, int weight) : base(id, widht, lenght, height, weight)
		{
			Produced = produced;
		}

		public Box(DateOnly produced?, DateOnly shelfLife, int id, int widht, int lenght, int height, int weight) : this(produced, id, widht, lenght,height, weight)
		{
			_shelfLife = shelfLife;
		}
	}
}
