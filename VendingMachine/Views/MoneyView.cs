using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine {
	public class MoneyView : IVendingMachineView {
		public VendingMachine Machine { get; set; }
		public bool KeyHandler() {
			var key = Console.ReadKey(true);
			int parsedKey;
			if(int.TryParse(key.KeyChar.ToString(), out parsedKey) && parsedKey < VendingMachine.Money.Length) {
				Machine.MoneyPool += VendingMachine.Money[parsedKey - 1];
			} else {
				switch(key.Key) {
					case ConsoleKey.Escape:
					case ConsoleKey.X:
						Machine.CurrentMenu = Machine.MainMenu;
						break;
				}
			}
			return true;
		}

		public void Render() {
			Console.Clear();
			Console.WriteLine($"Current balance: {Machine.MoneyPool}");
			int index = 1;
			foreach(var coin in VendingMachine.Money) {
				Console.WriteLine($"Press {index} to add {coin}kr to the balance.");
				index++;
			}
			Console.WriteLine($"X or Esc - return to the main menu\n");
		}
	}
}
