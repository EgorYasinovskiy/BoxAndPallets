namespace BoxAndPallets
{
	public static class Program
	{
		private static List<Pallet> _pallets;
		private static List<Box> _boxes;
		private static Dictionary<string, Action> _actionsMap = new Dictionary<string, Action>()
		{
			{"b",CreateBox},
			{"p",CreatePallete },
			{"pg", PrintGroups},
			{"pms", PrintMaxShelfLife},
			{"mb", MoveBox},
			{"save", Save },
			{"q",Quit }
		};

		private static void Quit()
		{
			Environment.Exit(0);
		}

		public static void Main(string[] args)
		{
			if (args.Length == 0 || args[0] == "help")
				PrintHelp();
			else if (args.Length == 2 && args[0] == "l")
			{
				_pallets = PalletSerializer.DeserializeFromFile(args[1]).ToList();
				_boxes = _pallets.SelectMany(x => x.InnerBoxes).ToList();
			}
			else if (args.Length == 1 && args[0] == "new")
			{
				Console.WriteLine("Ошибка. Неизвестный ключ.");
				PrintHelp();
			}
			while (true)
			{
				PrintCommand();
				var command = Console.ReadLine();
				if (_actionsMap.ContainsKey(command))
				{
					_actionsMap[command].Invoke();
				}
			}
		}

		private static void CreateBox()
		{
			Console.WriteLine("Введите через пробел id, длину, ширину, высоту , вес коробки, дату ее производсва или срок ее годности в формате dd.mm.yyyy.\n" +
				"Если вы ввели дату производства коробки последним параметром укажите 0, иначе 1");
			var parameters = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			DateOnly date;
			if (parameters.Length != 7)
			{
				Console.WriteLine("Ошибка. Неверное количество параметров");
				return;
			}
			else if (parameters.Take(5).Any(x => !uint.TryParse(x, out _)) || !uint.TryParse(parameters[^1], out _))
			{
				Console.WriteLine("Ошибка. Первые 5 параметров и последний должны быть целыми неотрицательными числами!");
				return;
			}
			else if (DateOnly.TryParse(parameters[5], out date))
			{
				Console.WriteLine("Ошибка. Не удается распознать дату.");
				return;
			}
			var id = int.Parse(parameters[0]);
			var lenght = int.Parse(parameters[1]);
			var width = int.Parse(parameters[2]);
			var height = int.Parse(parameters[3]);
			var weight = int.Parse(parameters[4]);

			if (_boxes.Any(x => x.Id == id))
			{
				Console.WriteLine("Ошибка. Данный Id коробки уже занят");
				return;
			}

			// Неплохим вариантом было бы создать класс BoxCreateParams и передавать его в качестве аргументов конструктора вместо этого мракобесия
			if (parameters[^1] == "0")
			{
				_boxes.Add(new Box(date, id, width, lenght, height, weight));
			}

			else
			{
				_boxes.Add(new Box(null, date, id, width, lenght, height, weight));
			}
		}
		private static void CreatePallete()
		{
			Console.WriteLine("Введите через пробел id, длину, ширину и высоту палета");
			var parameters = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (parameters.Length != 4)
			{
				Console.WriteLine("Ошибка. Неверное количество параметров");
				return;
			}
			else if (parameters.Any(x => !uint.TryParse(x, out _)))
			{
				Console.WriteLine("Все параметры должны быть неотрицательными целыми числами.");
				return;
			}
			var id = int.Parse(parameters[0]);
			var length = int.Parse(parameters[1]);
			var width = int.Parse(parameters[2]);
			var height = int.Parse(parameters[3]);

			if (_pallets.Any(x => x.Id == id))
			{
				Console.WriteLine("Ошибка. Данный id для палеты уже занят");
				return;
			}

			_pallets.Add(new Pallet(id, width, length, height));

		}

		private static void PrintGroups()
		{
			PalletPrinter.PrintGroupedAndSorted(_pallets);
		}

		private static void PrintMaxShelfLife()
		{
			PalletPrinter.PrintWithMaxShelfTime(_pallets);
		}

		private static void PrintHelp()
		{
			Console.WriteLine("new - создать новый список\n" +
				"l {path} - загрузить новый из файла");
		}

		private static void PrintCommand()
		{
			var line = _actionsMap.Select(x => $"{x.Key} - {x.Value.Method.Name}");
		}

		private static void MoveBox()
		{
			Console.WriteLine("Введите через пробел id коробки, которую вы хотите перемести и id палета, куда должны попасть коробка");
			var parameters = Console.ReadLine().Split(new[] {' '},StringSplitOptions.RemoveEmptyEntries);
			if(parameters.Length!=0 || parameters.Any(x=>uint.TryParse(x, out _)))
			{
				Console.WriteLine("Ошибка ввода. Укажите 2 неотрицательных целых числа.");
				return;
			}
			var boxId = int.Parse(parameters[0]);
			var palleteId = int.Parse(parameters[1]);

			var pal = _pallets.FirstOrDefault(x => x.Id == palleteId);
			if(pal== null)
			{
				Console.WriteLine("Ошибка. Палета с указанным Id не найдена");
				return;
			}
			
			var box = _boxes.FirstOrDefault(x=>x.Id == boxId);
			if(box== null)
			{
				Console.WriteLine("Ошибка. Коробка с укзаанным Id не найдена.");
			}

			var containingPal = _pallets.FirstOrDefault(x => x.InnerBoxes.Any(x => x.Id == boxId));
			
			if (containingPal != null)
			{
				Console.WriteLine("Коробка будет перемещена из паллета с Id {0}", containingPal.Id);
				containingPal.PopBox(boxId);
			}
			pal.TryAddBox(box);
		}

		private static void Save()
		{
			Console.WriteLine("Введите путь к файлу сохранения.");
			var path = Console.ReadLine();
			if(string.IsNullOrEmpty(path))
			{
				Console.WriteLine("Ошибка. Путь к файлу не указан");

			}	
			PalletSerializer.SerializeToFile(_pallets,path);
		}
	}
}