using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

public class Day4 {
    public static int PartOneSolution(StreamReader sr)
    {
        int sum = 0;
        List<int> card_scores = GetCardScores(sr);
        foreach (var card_score in card_scores)
        {
            if (card_score > 0)
            {
                sum += (int)Math.Pow(2, card_score - 1);
            }
        }
        return sum;
    }

    public static int PartTwoSolution(StreamReader sr)
    {
        int sum = 0;
        List<int> card_scores = GetCardScores(sr);
        int[] card_deck = new int[card_scores.Count];

        for (int i = 0; i < card_deck.Length; i++)
        {
            card_deck[i] = 1;
        }
        for (int i = 0; i < card_deck.Length; i++)
        {
            for (int j = i+1; j < card_deck.Length && j-(i+1) < card_scores[i]; j++)
            {
                card_deck[j] += card_deck[i];
            }
        }
        foreach (var count in card_deck)
        {
            sum += count;
        }

        return sum;
    }

    static List<int> GetCardScores(StreamReader sr)
    {
        string s;
        List<int> card_scores = new List<int>();
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
            card_scores.Add(hits);
        }
        return card_scores;
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
        Day4.PartTwoTest();
        Day4.PartTwo();
    }
}
