using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Day9
{
    public static int PartOneSolution(StreamReader sr)
    {
        string s;
        List<int[]> histories = new List<int[]>();
        while ((s = sr.ReadLine()) != null)
        {
            string[] split = s.Split(' ');
            int[] history = new int[split.Length];
            for (int i = 0; i < split.Length; i++)
            {
                history[i] = int.Parse(split[i]);
            }
            histories.Add(history);
        }
        int sum = 0;
        foreach (int[] history in histories)
        {
            sum += GetScore(history);
        }
        return sum;
    }

    private static int GetScore(int[] history)
    {
        if (IsAllZero(history))
        {
            return 0;
        }
        else
        {
            return history[history.Length - 1]
                + GetScore(GetDifferences(history));
        }
    }

    public static long PartTwoSolution(StreamReader sr)
    {
        return 0;
    }

    private static int[] GetDifferences(int[] history)
    {
        int[] differences = new int[history.Length - 1];
        for (int i = 0; i < history.Length - 1; i++)
        {
            differences[i] = history[i + 1] - history[i];
        }
        return differences;
    }

    private static bool IsAllZero(int[] history)
    {
        // check if every element in the list is 0
        for (int i = 0; i < history.Length; i++)
        {
            if (history[i] != 0)
            {
                return false;
            }
        }
        return true;
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
        Day9.PartOneTest();
        Day9.PartOne();
        // Day9.PartTwoTest();
        // Day9.PartTwo();
    }
}
