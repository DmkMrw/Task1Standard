using System.Data;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Task1Standard
{
	class Program
	{
		static void Main(string[] args)
		{
			Count("22 + 33,22 * 33 - 12 - 0001b + 0xFF");
		}
		public static void Count(string arg)
		{
			string[] argSplit = arg.Split(" ");

			Regex intRegex = new Regex(@"^[0-9]+$");
			Regex hexRegex = new Regex(@"[0-9]+[a-z]+[A-Z]+");
			Regex decimalRegex = new Regex(@"[0-9]+\W[0-9]+");
			Regex binRegex = new Regex(@"[0-1]+[b]");
			Regex signsRegex = new Regex(@"^\W$");

			string correctFormValue = "";
			decimal number;
			decimal result = 0;

			foreach (var item in argSplit)
			{
				if (intRegex.IsMatch(item))
				{
					correctFormValue += item;
				}
				else if (hexRegex.IsMatch(item))
				{
					correctFormValue += Convert.ToInt32(item, 16);
				}
				else if (decimalRegex.IsMatch(item))
				{
					correctFormValue += item;
				}
				else if (binRegex.IsMatch(item))
				{
					correctFormValue += Convert.ToInt32(item.Remove(item.Length - 1), 2);
				}
				else if (signsRegex.IsMatch(item) && item == "*")
				{
					correctFormValue += " * ";
				}
				else if (signsRegex.IsMatch(item) && item == "+")
				{
					correctFormValue += " + ";
				}
				else if (signsRegex.IsMatch(item) && item == "-")
				{
					correctFormValue += " - ";
				}
			}

			string[] argumentsForOperation = correctFormValue.Split(" ");


			for (int i = 0; i < argumentsForOperation.Length; i++)
			{
				if (decimal.TryParse(argumentsForOperation[i], out number))
				{
					result += decimal.Parse(argumentsForOperation[i]);
				}
				else if (argumentsForOperation[i] == "+")
				{
					result += decimal.Parse(argumentsForOperation[i + 1]);
					i++;
				}
				else if (argumentsForOperation[i] == "-")
				{
					result -= decimal.Parse(argumentsForOperation[i + 1]);
					i++;
				}
				else if (argumentsForOperation[i] == "*")
				{
					result = result * decimal.Parse(argumentsForOperation[i + 1]);
					i++;
				}
			}
			Console.WriteLine("Result: " + result);
		}
	}
}
