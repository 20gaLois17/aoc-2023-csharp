using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

public class Day6 {
    public static int PartOneSolution(StreamReader sr)
    {
        string s;
        List<Tuple<int,int>> races = new List<Tuple<int,int>>();
        List<string> times = new List<string>();
        List<string> distances = new List<string>();
        while ((s = sr.ReadLine()) != null)
        {
            if (s.Contains("Time: "))
            {
                IEnumerable<string> _times = Regex.Replace(s, "Time: ", "")
                    .Trim()
                    .Split(" ")
                    .Where(t => t != "");
                foreach (string time in _times)
                {
                    times.Add(time);
                }

            } else {
                IEnumerable<string> _distances = Regex.Replace(s, "Distance: ", "")
                    .Trim()
                    .Split(" ")
                    .Where(d => d != "");
                foreach (string distance in _distances)
                {
                    distances.Add(distance);
                }
            }
        }
        for (int i = 0; i < times.Count; i++)
        {
            races.Add(new Tuple<int,int>(Int32.Parse(times[i]), Int32.Parse(distances[i])));
        }
        int[] count = new int[races.Count];
        for (int i = 0; i < races.Count; i++)
        {
            for (int j = 1; j < races[i].Item1; j++)
            {
                if (j * (races[i].Item1-j) > races[i].Item2)
                {
                    count[i]++;
                }
            }
        }
        return count.Aggregate(1, (acc, x) => acc * x);
    }

    public static int PartTwoSolution(StreamReader sr)
    {
        int sum = 0;
        return sum;
    }

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
        Day6.PartOneTest();
        Day6.PartOne();
        // Day6.PartTwoTest();
        // Day6.PartTwo();
    }
}
