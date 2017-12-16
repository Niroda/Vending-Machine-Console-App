using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine {
	public class MainView : IVendingMachineView {
		private static int pageSize = 9;
		private int offset = 0;
		public int Offset
		{
			get { return offset; }
			set
			{
				if(value < 0) {
					offset = 0;
				} else if(value > Machine.Products.Count) {

				} else {
					offset = value;
				}
			}
		}
		public VendingMachine Machine { get; set; }

		public bool KeyHandler() {
			var key = Console.ReadKey(true);
			int parsedKey;
			if(int.TryParse(key.KeyChar.ToString(), out parsedKey)) {
				Machine.CurrentMenu = new DetailView(Machine.Products.ElementAt(parsedKey + offset - 1), this.Machine);
			} else {
				switch(key.Key) {
					case ConsoleKey.PageUp:
						this.Offset -= pageSize;
						break;
					case ConsoleKey.PageDown:
						this.Offset += pageSize;
						break;
					case ConsoleKey.A:
						Machine.CurrentMenu = new MoneyView() { Machine = this.Machine };
						break;
					case ConsoleKey.Escape:
					case ConsoleKey.X:
						return false;
				}
			}
			return true;
		}

		public void Render() {
			Console.Clear();
			Console.WriteLine("Available products:");
			int count = Machine.Products.Count;
			for(int i = 0; i < pageSize; i++) {
				if(i + offset >= count) {
					Console.WriteLine();
					continue;
				}
				Product prod = Machine.Products.Keys.ElementAt(i + offset);
				int amount = Machine.Products[prod];
				Console.WriteLine($"{i + 1}. {prod.ToString().PadRight(40, ' ')} - {prod.Price} kr, {amount} left");
			}

			Console.WriteLine($"\nCurrent balance: {Machine.MoneyPool}");
			Console.WriteLine($"\nPress the number key of the product you wish to buy");
			Console.WriteLine($"PgUp - Show previous page.");
			Console.WriteLine($"PgDn - Show next page.");
			Console.WriteLine($"A - Add more money to the machine");
			Console.WriteLine($"X or Esc - Leave the machine and recieve change\n");
		}
	}
}
