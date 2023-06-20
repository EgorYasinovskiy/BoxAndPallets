namespace BoxAndPallets
{
	public abstract class InventoryObject
	{
		public InventoryObject(int id, int widht, int lenght, int height, int weight)
		{
			Id = id;
			Widht = widht;
			Lenght = lenght;
			Height = height;
			Weight = weight;
		}

		public InventoryObject() { }

		public int Id { get; set; }
		public int Widht { get; set; }
		public int Lenght { get; set; }

		public int Height { get; set; }

		public virtual int Weight { get; set; }
		
		public virtual int GetVolume()
		{
			return Widht*Height*Lenght;
		}
	}
}
