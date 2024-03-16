using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

public class Day4 {
    public static int PartOneSolution(StreamReader sr)
    {
        string s;
        int sum = 0;
        while ((s = sr.ReadLine()) != null)
        {
            string pattern = "Card \\d+:";
            string[] parts = Regex.Replace(s, pattern, "").Split(" | ");
            HashSet<string> haystack = new HashSet<string>(
                parts[1].Split(" ")
                .Where(n => n.Length > 0)
            );
            string[] needles = parts[0].Split(" ")
                .Where(n => n.Length > 0).ToArray();
            int hits = 0;
            foreach (string needle in needles)
            {
                if (haystack.Contains(needle))
                {
                    hits++;
                }
            }
            Console.WriteLine($"Hits: {hits}");
            if (hits == 0)
            {
                continue;
            }
            sum += (int)Math.Pow(2, hits-1);
        }
        return sum;
    }
    public static int PartTwoSolution(StreamReader sr)
    {
        return 0;
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
        Day4.PartOneTest();
        Day4.PartOne();
        // Day4.PartTwoTest();
        // Day4.PartTwo();
    }
}
