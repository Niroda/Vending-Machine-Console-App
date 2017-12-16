using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine {
	public class VendingMachine {
		#region Statics
		public static int[] Money = { 1, 5, 10, 20, 50, 100, 500, 1000 };
		#endregion
		#region Properties and Fields
		public IVendingMachineView MainMenu { get; set; }
		public IVendingMachineView MoneyMenu { get; set; }
		public IVendingMachineView CurrentMenu { get; set; }
		public int MoneyPool { get; set; }
		public Dictionary<Product, int> Products { get; set; }
		#endregion
		#region Constructors
		public VendingMachine() {
			this.Products = new Dictionary<Product, int> {
				{ new Drink() { Name = "Coffee", Price = 8 }, 10 },
				{ new Drink() { Name = "Tea", Price = 8 }, 10 },
				{ new Drink() { Name = "Pepsi Cola", Price = 8 }, 10 },
				{ new Drink() { Name = "Pepsi Cola (Sugar Free)", Price = 8 }, 10 },
				{ new Drink() { Name = "Pepsi Maxi", Price = 8 }, 10 },
				{ new Drink() { Name = "Coca Cola", Price = 8 }, 10 },
				{ new Drink() { Name = "Coca Cola Life", Price = 8 }, 10 },
				{ new Drink() { Name = "Coca Cola Light", Price = 8 }, 10 },
				{ new Drink() { Name = "Coca Cola Zero", Price = 8 }, 10 },
				{ new Drink() { Name = "Dr. Pepper", Price = 8 }, 10 },
				{ new Drink() { Name = "Dr. Pepper Cherry", Price = 8 }, 10 },
				{ new Drink() { Name = "Fanta", Price = 8 }, 10 },
				{ new Snack() { Name = "Apple", Price = 10, HasNuts = false }, 10 },
				{ new Snack() { Name = "Sandwich", Price = 10, HasNuts = false }, 10 },
				{ new Snack() { Name = "Snickers Bar", Price = 10, HasNuts = true }, 10 }
			};
			//// Example of a lambda query of Products - "get all items in Products that are Snacks, and have nuts in them"
			//var test = this.Products.Where(kv => kv.Key is Snack && ((Snack)kv.Key).HasNuts);
			this.MainMenu = new MainView() {Machine = this, Offset = 0 };
			this.MoneyMenu = new MoneyView() { Machine = this };
			this.CurrentMenu = this.MainMenu;
			this.MoneyPool = 0;
		}
		#endregion
		#region Methods
		// Show current menu in console
		public void Render() {
			CurrentMenu.Render();
		}

		public bool KeyHandler() {
			return CurrentMenu.KeyHandler();
		}

		public void ReturnChange() {
			Console.WriteLine("The machine spits out change in your face.\nYou get hit by:");
			int remainder = this.MoneyPool;
			for(int i = Money.Length-1; i >= 0; i--) {
				int amount = remainder / Money[i];
				remainder = remainder % Money[i];
				if(amount > 0) {
					Console.WriteLine($"\t{amount} {Money[i]} kr coins");
				}
			}
			this.MoneyPool = 0;
		}
		#endregion
	}
}
