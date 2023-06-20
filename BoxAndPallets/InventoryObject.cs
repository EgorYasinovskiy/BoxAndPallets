namespace BoxAndPallets
{
	public abstract class InventoryObject
	{
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
