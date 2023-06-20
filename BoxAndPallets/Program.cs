using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;

namespace BoxAndPallets
{
	public class Program
	{
		private List<Pallet> _pallets;
		private List<Box> _boxes;
		static void Main(string[] args)
		{
		}

		public void CreateBox()
		{
			Console.WriteLine("Введите через пробел id, длину, ширину, высоту , вес коробки, дату ее производсва или срок ее годности в формате dd.mm.yyyy.\n" +
				"Если вы ввели дату производства коробки последним параметром укажите 0, иначе 1");
			var parameters = Console.ReadLine().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			DateOnly date;
			if (parameters.Length != 7)
			{
				Console.WriteLine("Ошибка. Неверное количество параметров");
				return;
			}
			else if(parameters.Take(5).All(x=>uint.TryParse(x, out _)) && uint.TryParse(parameters[^1], out _))
			{
				Console.WriteLine("Ошибка. Первые 5 параметров и последний должны быть целыми неотрицательными числами!");
				return;
			}
			else if (DateOnly.TryParse(parameters[5], out date))
			{
				Console.WriteLine("Ошибка. Не удается распознать дату.");
				return;
			}
			if (parameters[^1] == "0")
			{
				_boxes.Add(new Box(date, int.Parse(parameters[0]), int.Parse(parameters[2]), int.Parse(parameters[1]), int.Parse(parameters[3]), int.Parse(parameters[1])));
			}
			else
			{
				_boxes.Add(new Box(null,date, int.Parse(parameters[0]), int.Parse(parameters[2]), int.Parse(parameters[1]), int.Parse(parameters[3]), int.Parse(parameters[1])));
			}
			
			



		}
		public void CreatePallete()
		{

		}
		public void PrintGroups()
		{

		}
		public void PrintMaxShelfLife()
		{

		}
	}
}