using System.Collections.ObjectModel;

namespace BoxAndPallets
{
	public class Pallet : InventoryObject
	{
		private List<Box> _innerBoxes;
		public ReadOnlyCollection<Box> InnerBoxes { get => _innerBoxes.AsReadOnly(); }

		public Pallet(int id, int widht, int lenght, int height) : base(id, widht, lenght, height, 30)
		{
			_innerBoxes= new List<Box>();
		}

		public override int GetVolume()
		{
			return base.GetVolume() + _innerBoxes.Sum(x => x.GetVolume());
		}
		public override int Weight
		{
			get
			{
				return base.Weight + _innerBoxes.Sum(x => x.Weight);
			}
		}
		public DateOnly? ShelfLife
		{
			get
			{
				return _innerBoxes.Any() ? _innerBoxes.Min(x => x.ShelfLife) : null;
			}
		}
		public bool TryAddBox(Box box)
		{
			if (box.Lenght <= this.Lenght && box.Widht <= this.Widht)
			{
				_innerBoxes.Add(box);
				return true;
			}
			Console.WriteLine("Box is too big for this pallete");
			return false;

		}
		public Box PopBox(int id)
		{
			if (!_innerBoxes.Any())
			{
				Console.WriteLine("There is no boxes");
				return null;
			}
			else if (_innerBoxes.Any(x => x.Id == id))
			{
				var box = _innerBoxes.First(x => x.Id == id);
				_innerBoxes.Remove(box);
				return box;
			}
			else return null;
		}
	}

}
