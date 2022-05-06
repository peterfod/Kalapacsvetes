using System;
using System.Collections.Generic;
using System.IO;

namespace Kalapacsvetes
{
	//2. feladat
	public class Sportolo
	{
		public string Helyezes { get; set; }
		public double Eredmeny { get; set; }
		public string Nev { get; set; }
		public string Orszag { get; set; }
		public string Helyszin { get; set; }
		public string Datum { get; set; }

		public Sportolo(string sor)
		{
			string[] reszek = sor.Split(';');
			Helyezes = reszek[0];
			Eredmeny = Convert.ToDouble(reszek[1]);
			Nev = reszek[2];
			Orszag = reszek[3];
			Helyszin = reszek[4];
			Datum = reszek[5];
		}
	}
	class Kalapacsvetes
	{
		static void Main(string[] args)
		{
			//3. feladat
			List<Sportolo> lista = new List<Sportolo>();
			StreamReader sr = new StreamReader("kalapacsvetes.txt");
			string elsosor = sr.ReadLine();
			while (!sr.EndOfStream)
			{
				Sportolo sor = new Sportolo(sr.ReadLine());
				lista.Add(sor);
			}
			sr.Close();

			//4. feladat
			Console.WriteLine($"4. feladat: {lista.Count} dobás eredménye található.");

			//5. feladat
			double eredmeny = 0;
			int magyarokSzama = 0;
			foreach (var item in lista)
			{
				if (item.Orszag.Contains("HUN"))
				{
					eredmeny += item.Eredmeny;
					magyarokSzama++;
				}
			}
			double atlag = eredmeny / magyarokSzama;
			Console.WriteLine($"5. feladat: A magyar sportolók átlagosan {atlag:.##} métert dobtak.");

			//6. feladat //1998
			Console.WriteLine($"6. feladat: Adjon meg egy évszámot: ");
			string ev = Console.ReadLine();
			bool vanEv = false;
			int dobasokSzama = 0;
			var sportolok = new List<string>();
			foreach (var item in lista)
			{
				if (item.Datum.Substring(0, 4) == ev)
				{
					vanEv = true;
					dobasokSzama++;
					sportolok.Add(item.Nev);
				}
			}
			if (vanEv)
			{
				Console.WriteLine($"\t{dobasokSzama} darab dobás került be ebben az évben.");
				foreach (var item in sportolok)
				{
					Console.WriteLine($"\t{item}");
				}
			}
			else
			{
				Console.WriteLine("\tEgy dobás sem került be ebben az évben.");
			}

			//7. feladat
			Dictionary<string, int> statisztika = new Dictionary<string, int>();
			foreach (var kulcs in lista)
			{
				if (statisztika.ContainsKey(kulcs.Orszag))
				{
					statisztika[kulcs.Orszag]++;
				}
				else
				{
					statisztika.Add(kulcs.Orszag, 1);
				}
			}
			Console.WriteLine("7. feladat: Statisztika");
			foreach (KeyValuePair<string, int> item in statisztika)
			{
				Console.WriteLine($"\t{item.Key} - {item.Value} dobás");
			}

			//8. feladat
			StreamWriter sw = new StreamWriter("magyarok.txt");
			sw.WriteLine(elsosor);
			foreach (var item in lista)
			{
				if (item.Orszag.Contains("HUN"))
				{
					sw.WriteLine($"{item.Helyezes};{item.Eredmeny};{item.Nev};{item.Orszag};{item.Helyszin};{item.Datum}");
				}
			}
			sw.Flush();
			sw.Close();
			Console.ReadKey();
		}
	}
}
