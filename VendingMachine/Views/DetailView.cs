using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine {
	public class DetailView : IVendingMachineView {
		public VendingMachine Machine { get; set; }
		public Product Product { get; set; }
		public int Amount { get; set; }
		public DetailView(KeyValuePair<Product, int> product, VendingMachine machine) {
			this.Product = product.Key;
			this.Amount = product.Value;
			this.Machine = machine;
		}

		public bool KeyHandler() {
			var key = Console.ReadKey(true);
			switch(key.Key) {
				case ConsoleKey.B:
					if(Machine.MoneyPool < Product.Price) {
						Console.WriteLine("Insert more money to purchase this item.");
						Console.WriteLine("Do you want to insert money now? (y/n)");
						ConsoleKeyInfo cki;
						do {
							cki = Console.ReadKey(true);
						}
						while(!(cki.Key == ConsoleKey.N || cki.Key == ConsoleKey.Y));
						switch(cki.Key) {
							case ConsoleKey.N:
								this.Machine.CurrentMenu = Machine.MainMenu;
								break;
							case ConsoleKey.Y:
								this.Machine.CurrentMenu = Machine.MoneyMenu;
								break;
						}
					} else if(Amount == 0) {
						Console.WriteLine("There are none of this item left in the machine.");
						Console.ReadKey();
						this.Machine.CurrentMenu = Machine.MainMenu;
					} else {
						Machine.MoneyPool -= Product.Price;
						Machine.Products[Product] -= 1;
						Amount -= 1;
						this.Render();
						Console.WriteLine("Because you are filled with instiable need to use the product, you use it right away!");
						Product.Use();
						Console.ReadKey();
					}
					break;
				case ConsoleKey.Escape:
				case ConsoleKey.X:
					Machine.CurrentMenu = Machine.MainMenu;
					break;
			}
			return true;
		}

		public void Render() {
			Console.Clear();
			Console.WriteLine($"Product Name: {Product.Name}");
			Console.WriteLine($"Product Type: {Product.Type}");
			Console.WriteLine($"Price: {Product.Price}");
			Console.WriteLine($"Amount left: {Amount}");
			if(Product is Snack) {
				if((Product as Snack).HasNuts) {
					Console.WriteLine($"Contains Nuts.");
				}
			}

			Console.WriteLine($"\nCurrent balance: {Machine.MoneyPool}");
			Console.WriteLine($"\nB - Buy one of this product");
			Console.WriteLine($"X or Esc - return to the main menu\n");
		}
	}
}
