using System;
using System.IO;
using System.Collections.Generic;

public class Day11
{
    public static long PartOneSolution(StreamReader sr)
    {
        HashSet<int> rowIndexWithGalaxy = new HashSet<int>();
        HashSet<int> colIndexWithGalaxy = new HashSet<int>();
        List<Tuple<int, int>> galaxies = new List<Tuple<int, int>>();
        string s;
        for (int rowIndex = 0; (s = sr.ReadLine()) != null; rowIndex++)
        {
            for (int colIndex = 0; colIndex < s.Length; colIndex++)
            {
                if (s[colIndex] == '#')
                {
                    rowIndexWithGalaxy.Add(rowIndex);
                    colIndexWithGalaxy.Add(colIndex);
                    galaxies.Add(new Tuple<int, int>(rowIndex, colIndex));
                }
            }
        }
        long result = 0;
        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                long distance = GetDistance(
                    galaxies[i],
                    galaxies[j],
                    rowIndexWithGalaxy,
                    colIndexWithGalaxy
                );
                result += distance;
            }
        }
        return result;
    }

    public static long PartTwoSolution(StreamReader sr)
    {
        HashSet<int> rowIndexWithGalaxy = new HashSet<int>();
        HashSet<int> colIndexWithGalaxy = new HashSet<int>();
        List<Tuple<int, int>> galaxies = new List<Tuple<int, int>>();
        string s;
        for (int rowIndex = 0; (s = sr.ReadLine()) != null; rowIndex++)
        {
            for (int colIndex = 0; colIndex < s.Length; colIndex++)
            {
                if (s[colIndex] == '#')
                {
                    rowIndexWithGalaxy.Add(rowIndex);
                    colIndexWithGalaxy.Add(colIndex);
                    galaxies.Add(new Tuple<int, int>(rowIndex, colIndex));
                }
            }
        }
        long result = 0;
        for (int i = 0; i < galaxies.Count; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                long distance = GetDistance(
                    galaxies[i],
                    galaxies[j],
                    rowIndexWithGalaxy,
                    colIndexWithGalaxy,
                    1000000
                );
                result += distance;
            }
        }

        return result;
    }

    private static long GetDistance(
        Tuple<int, int> g1,
        Tuple<int, int> g2,
        HashSet<int> rowIdxWithGalaxy,
        HashSet<int> colIdxWithGalaxy,
        int extraTraversalCost = 2
    )
    {
        int fromRow = Math.Min(g1.Item1, g2.Item1);
        int toRow = Math.Max(g1.Item1, g2.Item1);

        int fromCol = Math.Min(g1.Item2, g2.Item2);
        int toCol = Math.Max(g1.Item2, g2.Item2);

        int distance = 0;
        for (int row = fromRow + 1; row <= toRow; row++)
        {
            int traversalCost = rowIdxWithGalaxy.Contains(row) ? 1 : extraTraversalCost;
            distance += traversalCost;
        }
        for (int col = fromCol + 1; col <= toCol; col++)
        {
            int traversalCost = colIdxWithGalaxy.Contains(col) ? 1 : extraTraversalCost;
            distance += traversalCost;
        }
        return distance;
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
        Day11.PartOneTest();
        Day11.PartOne();
        Day11.PartTwoTest();
        Day11.PartTwo();
    }
}
