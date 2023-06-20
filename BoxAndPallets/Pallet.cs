using System.Collections.ObjectModel;

namespace BoxAndPallets
{
	public class Pallet : InventoryObject
	{
		private List<Box> _innerBoxes;
		public ReadOnlyCollection<Box> InnerBoxes { get => _innerBoxes.AsReadOnly(); }
		private int _weight;
		public override int GetVolume()
		{
			return base.GetVolume() + _innerBoxes.Sum(x => x.GetVolume());
		}
		public override int Weight
		{
			get
			{
				return _weight + _innerBoxes.Sum(x => x.Weight);
			}
			set
			{
				_weight = value;
			}
		}
		public DateOnly? ShelfLife
		{
			get
			{
				return _innerBoxes.Any() ? _innerBoxes.Min(x=>x.ShelfLife) : null;
			}
		}
		public void AddBox(Box box)
		{
			if (box.Lenght <= this.Lenght && box.Widht <= this.Widht)
			{
				_innerBoxes.Add(box);
			}
			else
			{
				Console.WriteLine("Box is too big for this pallete");
			}
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
