using System;
using System.IO;
using System.Collections.Generic;

public class Day2 {


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


	static int GetBagLimit(string color)
	{
		Dictionary <string, int> bag = new Dictionary<string, int>
		{
			["red"]   = 12,
			["green"] = 13,
			["blue"]  = 14,
		};
		try
		{
			return bag[color];
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error: {ex}");
		}
		return 0;
	}

	public static int PartOneSolution(StreamReader sr)
	{
		int result = 0;
		HashSet<int> invalidIds = new HashSet<int>();

		string s;
		while ((s = sr.ReadLine()) != null)
		{
			string[] game = s.Split(":");
			int id = int.Parse(game[0].Split(" ")[1]);
			result += id;

			string[] rounds = game[1].Split(";");
			foreach (string round in rounds)
			{
				string[] colors = round.Split(",");
				foreach (string color in colors)
				{
					string[] ball = color.Trim().Split(" ");
					int num = int.Parse(ball[0].Trim());
					if (num > GetBagLimit(ball[1].Trim()))
					{
						invalidIds.Add(id);
					}
				}
			}
		}
		foreach (int id in invalidIds)
		{
			result -= id;
		}
		return result;
	}

	public static int PartTwoSolution(StreamReader sr)
	{
		int result = 0;

		string s;
		while ((s = sr.ReadLine()) != null)
		{
			Dictionary <string, int> maxBallCount = new Dictionary<string, int>
			{
				["red"]   = 0,
				["green"] = 0,
				["blue"]  = 0,
			};
			string[] game = s.Split(":");
			string[] rounds = game[1].Split(";");
			foreach (string round in rounds)
			{
				string[] colors = round.Split(",");
				foreach (string color in colors)
				{
					string[] ball = color.Trim().Split(" ");
					int num = int.Parse(ball[0].Trim());
					string col = ball[1].Trim();

					if (num > maxBallCount[col])
					{
						maxBallCount[col] = num;
					}
				}
			}
			int power = 1;
			foreach (KeyValuePair<string, int> kvp in maxBallCount)
			{
				power *= kvp.Value;
			}
			result += power;
		}

		return result;
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
		Day2.PartOneTest();
		Day2.PartOne();
		Day2.PartTwoTest();
		Day2.PartTwo();
	}
}
