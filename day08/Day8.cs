using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Day8
{
    public static int PartOneSolution(StreamReader sr)
    {
        string s;
        string sequence = sr.ReadLine();
        sr.ReadLine();
        Dictionary<string, Tuple<string, string>> rules = new Dictionary<string, Tuple<string, string>>();
        while ((s = sr.ReadLine()) != null)
        {
            string start = s.Substring(0, 3);
            string left = s.Substring(7, 3);
            string right = s.Substring(12, 3);
            rules.Add(start, new Tuple<string, string>(left, right));
        }
        string current = "AAA";
        int count = 0;
        int idx = 0;
        while (current != "ZZZ")
        {
            int target = idx % (sequence.Length);
            current = sequence[target] == 'L' ? rules[current].Item1 : rules[current].Item2;
            idx++;
            count++;
        }
        return count;
    }

    public static int PartTwoSolution(StreamReader sr)
    {
        return 0;
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
}

class Program
{
    static void Main()
    {
        Day8.PartOneTest();
        Day8.PartOne();
        // Day8.PartTwoTest();
        // Day8.PartTwo();
    }
}
