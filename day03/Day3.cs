using System;
using System.IO;
using System.Collections.Generic;

public class Day3 {


	static StreamReader GetInput(string path)
	{
		try
		{
			return File.OpenText(path);
		}

		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex.Message}");
		}
		return null;
	}

	public static int PartOneSolution(StreamReader sr)
	{
		int result = 0;
		string s;

		// safe the positions of all symbols
		HashSet<(int, int)> symbolPositions = new HashSet<(int, int)>();
		int posY = 0;
		List<NumPos> nums = new List<NumPos>();
		while ((s = sr.ReadLine()) != null)
		{
			string num = "";
			for (int i = 0; i < s.Length; i++)
			{
				// TODO: parse all numbers and safe their positions and lengths
				if (Char.IsDigit(s[i]))
				{
					num += s[i];
				}
				else
				{
					if (num.Length > 0)
					{
						NumPos n = new NumPos();
						n.Value = int.Parse(num);
						n.Position = (i-num.Length, posY);
						nums.Add(n);
						num = "";
					}
					if (s[i] != '.')
					{
						symbolPositions.Add((i, posY));
					}
				}
			}
			if (num.Length > 0)
			{
				NumPos n = new NumPos();
				n.Value = int.Parse(num);
				n.Position = (s.Length-num.Length, posY);
				nums.Add(n);
				num = "";
			}
			posY++;
		}
		foreach (NumPos num in nums)
		{
			foreach ((int, int) position in num.GetCorridor(posY, posY))
			{
				if (symbolPositions.Contains(position))
				{
					Console.WriteLine($"{num.Value} is valid");
					result += num.Value;
					break;
				}
			}
		}
		return result;
	}

	public class NumPos
	{
		public int Value { get; set; }
		public (int, int) Position { get; set; }

		public NumPos()
		{
		}

		public List<(int, int)> GetCorridor(int width, int height)
		{
			List<(int, int)> corridor = new List<(int, int)>();

			var pos = this.Position;
			int val = this.Value;

			/**
			 *  x
			 *  x 1 2 3
			 *  x
			 */
			if (pos.Item1-1 >= 0 && pos.Item2-1 >= 0)
			{
				corridor.Add((pos.Item1-1, pos.Item2-1));
			}
			if (pos.Item1-1 >= 0)
			{
				corridor.Add((pos.Item1-1, pos.Item2));
			}
			if (pos.Item1-1 >= 0 && pos.Item2+1 < height)
			{
				corridor.Add((pos.Item1-1, pos.Item2+1));
			}

			/**
			 *  x x x
			 *  1 2 3
			 *  x x x
			 */
			do
			{
				if (pos.Item2-1 >= 0)
				{
					corridor.Add((pos.Item1, pos.Item2-1));
				}
				if (pos.Item2+1 < height)
				{
					corridor.Add((pos.Item1, pos.Item2+1));
				}
				if (val > 9)
				{
					pos.Item1++;
				}
			} while ((val = val / 10) > 0);

			/**
			 *        x
			 *  1 2 3 x
			 *        x
			 */
			if (pos.Item1+1 < width && pos.Item2-1 >= 0)
			{
				corridor.Add((pos.Item1+1, pos.Item2-1));
			}
			if (pos.Item1+1 < width)
			{
				corridor.Add((pos.Item1+1, pos.Item2));
			}
			if (pos.Item1+1 < width && pos.Item2+1 < height)
			{
				corridor.Add((pos.Item1+1, pos.Item2+1));
			}
			return corridor;
		}
	}

	public static int PartTwoSolution(StreamReader sr)
	{
		return 1;
	}

	public static void PartOneTest()
	{
		StreamReader sr = GetInput("test-input.txt");
		Console.WriteLine($"Part One Test: {PartOneSolution(sr)}");
	}

	public static void PartOne()
	{
		StreamReader sr = GetInput("input.txt");
		Console.WriteLine($"Part One: {PartOneSolution(sr)}");
	}

	public static void PartTwoTest()
	{
		StreamReader sr = GetInput("test-input.txt");
		Console.WriteLine($"Part Two Test: {PartTwoSolution(sr)}");
	}

	public static void PartTwo()
	{
		StreamReader sr = GetInput("input.txt");
		Console.WriteLine($"Part Two: {PartTwoSolution(sr)}");
	}
}

class Program
{
	static void Main()
	{
		//Day3.PartOneTest();
		Day3.PartOne(); // 540324 is wrong
		//Day3.PartTwoTest();
		//Day3.PartTwo();
	}
}
