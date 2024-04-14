using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class Day5 {
    public static long PartOneSolution(StreamReader sr)
    {
        string s;
        StateMachine m = new StateMachine();
        List<long> unmapped = new List<long>();
        List<long> mapped = new List<long>();

        while ((s = sr.ReadLine()) != null)
        {
            m.setStateFromLine(s);
            switch (m.GetState())
            {
                case StateMachine.State.Init:
                    string[] seeds = Regex.Replace(s, "seeds: ", "").Split(" ");
                    foreach (string seed in seeds)
                    {
                        unmapped.Add(long.Parse(seed));
                    }
                    break;

                case StateMachine.State.Idle:
                    break;

                case StateMachine.State.Map:
                    string[] values = s.Split(" ");
                    long destination = long.Parse(values[0]);
                    long source = long.Parse(values[1]);
                    long range = long.Parse(values[2]);
                    long[] candidates = unmapped.ToArray();
                    unmapped.Clear();
                    foreach (long candidate in candidates)
                    {
                        if (candidate >= source && candidate <= source+range)
                        {
                            mapped.Add(destination + candidate - source);
                        } else
                        {
                            unmapped.Add(candidate);
                        }
                    }

                    break;

                case StateMachine.State.Collect:
                    foreach (long item in unmapped)
                    {
                        mapped.Add(item);
                    }
                    unmapped = new List<long>(mapped.ToArray());
                    mapped.Clear();
                    break;
            }
        }
        unmapped.Sort();
        return unmapped[0];
    }

    public static long PartTwoSolution(StreamReader sr)
    {
        string s;
        StateMachine m = new StateMachine();
        List<long> unmapped = new List<long>();
        List<long> mapped = new List<long>();

        while ((s = sr.ReadLine()) != null)
        {
            m.setStateFromLine(s);
            switch (m.GetState())
            {
                case StateMachine.State.Init:
                    string[] seeds = Regex.Replace(s, "seeds: ", "").Split(" ");
                    for (int i = 0; i+1 < seeds.Length; i += 2)
                    {
                        int count = int.Parse(seeds[i+1]);
                        while (count > 0)
                        {
                            unmapped.Add(long.Parse(seeds[i]) + count);
                            count--;
                        }
                    }
                    break;

                case StateMachine.State.Idle:
                    break;

                case StateMachine.State.Map:
                    string[] values = s.Split(" ");
                    long destination = long.Parse(values[0]);
                    long source = long.Parse(values[1]);
                    long range = long.Parse(values[2]);
                    long[] candidates = unmapped.ToArray();
                    unmapped.Clear();
                    foreach (long candidate in candidates)
                    {
                        if (candidate >= source && candidate <= source+range)
                        {
                            mapped.Add(destination + candidate - source);
                        } else
                        {
                            unmapped.Add(candidate);
                        }
                    }

                    break;

                case StateMachine.State.Collect:
                    foreach (long item in unmapped)
                    {
                        mapped.Add(item);
                    }
                    unmapped = new List<long>(mapped.ToArray());
                    mapped.Clear();
                    break;
            }
        }
        unmapped.Sort();
        return unmapped[0];
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
        Day5.PartOneTest();
        Day5.PartOne();
        Day5.PartTwoTest();
        // Day5.PartTwo();
    }
}
