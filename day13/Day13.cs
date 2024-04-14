using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Day13
{
    public static long PartOneSolution(StreamReader sr)
    {
        string s;
        List<string> rows = new List<string>();
        List<string> columns = new List<string>();
        List<int> results = new List<int>();
        while ((s = sr.ReadLine()) != null)
        {
            if (s.Length > 0)
            {
                rows.Add(s);
            }
            else
            {
                for (int i = 0; i < rows[0].Length; i++)
                {
                    string column = "";
                    foreach (string line in rows)
                    {
                        column += line[i];
                    }
                    columns.Add(column);
                }

                results.Add(
                        Math.Max(
                            100 * HasMatch(rows.ToArray()),
                            HasMatch(columns.ToArray())
                            )
                        );

                rows.Clear();
                columns.Clear();
            }
        }

        foreach (int result in results)
        {
            Console.WriteLine($"result {result}");
        }
        return results.Sum();
    }

    private static int HasMatch(string[] lines)
    {
        int offset = 1;
        while (offset < lines.Length)
        {
            int upper = offset;
            bool isMatch = true;
            for (int lower = offset - 1; lower >= 0; lower--)
            {
                if (upper == lines.Length)
                {
                    if (isMatch)
                    {
                        return offset;
                    }
                    else
                    {
                        break;
                    }
                }
                if (CountSmudges(lines[lower], lines[upper]) > 0)
                {
                    Console.WriteLine($"{lower}: {lines[lower]} != {upper}: {lines[upper]}");
                    isMatch = false;
                    break;
                }
                else
                {
                    Console.WriteLine($"{lower}: {lines[lower]} == {upper}: {lines[upper]}");
                    upper++;
                }
            }
            if (isMatch)
            {
                return offset;
            }
            else
            {
                offset++;
            }
        }
        return 0;
    }

    // there are only two values, so a bitmap would have been much better
    public static long PartTwoSolution(StreamReader sr)
    {
        string s;
        List<string> rows = new List<string>();
        List<string> columns = new List<string>();
        List<int> results = new List<int>();
        while ((s = sr.ReadLine()) != null)
        {
            if (s.Length > 0)
            {
                rows.Add(s);
            }
            else
            {
                for (int i = 0; i < rows[0].Length; i++)
                {
                    string column = "";
                    foreach (string line in rows)
                    {
                        column += line[i];
                    }
                    columns.Add(column);
                }

                results.Add(
                        Math.Max(
                            100 * HasMatch2(rows.ToArray()),
                            HasMatch2(columns.ToArray())
                            )
                        );

                rows.Clear();
                columns.Clear();
            }
        }

        foreach (int result in results)
        {
            Console.WriteLine($"result {result}");
        }
        return results.Sum();
    }

    private static int HasMatch2(string[] lines)
    {
        int offset = 1;
        while (offset < lines.Length)
        {
            int upper = offset;
            int smudges = 0;
            bool isMatch = true;
            for (int lower = offset - 1; lower >= 0; lower--)
            {
                if (upper == lines.Length)
                {
                    if (isMatch)
                    {
                        return offset;
                    }
                    else
                    {
                        break;
                    }
                }
                smudges += CountSmudges(lines[lower], lines[upper]);
                if (smudges > 1)
                {
                    isMatch = false;
                    break;
                }
                else
                {
                    upper++;
                }
            }
            if (isMatch && smudges == 1)
            {
                return offset;
            }
            else
            {
                offset++;
            }
        }
        return 0;
    }

    private static int CountSmudges(string first, string second)
    {
        int smudges = 0;
        for (int i = 0; i < first.Length; i++)
        {
            if (first[i] != second[i])
            {
                smudges++;
            }
        }
        return smudges;
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
        // Day13.PartOneTest();
        // Day13.PartOne();
        // Day13.PartTwoTest();
        Day13.PartTwo();
    }
}
