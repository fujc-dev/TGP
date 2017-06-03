using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGP.Stack
{

	public class Thing
	{
		public String Name { get; set; }
	}

	public class Animal : Thing
	{
		public Animal() { }

		public Animal(String name, int weight)
		{
			base.Name = name;
			this.Weight = weight;
		}

		public int Weight;
	}

	public class Vegetable : Thing
	{
		public Vegetable() { }
		public Vegetable(String name, int length)
		{
			base.Name = name;
			this.Length = length;
		}
		public int Length;
	}

	class Program
	{
		static void Main(string[] args)
		{
			Animal pointer = new Animal("张三", 20);

			Go3(pointer);

			Console.WriteLine(pointer.Name);

			int x = 0, y = 0;
			x++;
			Console.WriteLine(x);
			++x;
			Console.WriteLine(x);

		}

		public static void Go3(Animal pointer)
		{
			pointer = new Animal("李四", pointer.Weight);

			Console.WriteLine(pointer.Name);
		}

		public void Go1()
		{
			Thing x = new Animal();

			Switcharoo(ref x);

			Console.WriteLine("x is Animal    :   " + (x is Animal).ToString());

			Console.WriteLine("x is Vegetable :   " + (x is Vegetable).ToString());

		}

		public void Go2()
		{
			Thing x = new Animal();

			Switcharoo(x);

			Console.WriteLine("x is Animal    :   " + (x is Animal).ToString());

			Console.WriteLine("x is Vegetable :   " + (x is Vegetable).ToString());

		}

		public void Switcharoo(ref Thing pValue)
		{
			pValue = new Vegetable();
		}

		public void Switcharoo(Thing pValue)
		{
			pValue = new Vegetable();
		}
	}
}
