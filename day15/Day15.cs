using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Day15
{
    public static long PartOneSolution(StreamReader sr)
    {
        int c;
        int sum = 0;
        int current = 0;
        while ((c = sr.Read()) > -1)
        {
            // 44 ~ ',', 10 ~ '\n'
            if (c == 44 || c == 10)
            {
                sum += current;
                current = 0;
                continue;
            }
            current += c;
            current = (current * 17) % 256;
        }
        return sum;
    }

    public static long PartTwoSolution(StreamReader sr)
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
        // Day15.PartOneTest();
        Day15.PartOne();
        // Day15.PartTwoTest();
        // Day15.PartTwo();
    }
}
