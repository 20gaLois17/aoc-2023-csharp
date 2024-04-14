using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Day12
{
    public static long PartOneSolution(StreamReader sr)
    {
        string s;
        int result = 0;
        while ((s = sr.ReadLine()) != null)
        {
            int count = 0;
            List<SpringState> springs = new List<SpringState>();
            string line = s.Split(' ')[0];
            foreach (char c in line)
            {
                springs.Add(FromChar(c));
            }
            string[] sDamages = s.Split(' ')[1].Split(',');
            int[] damages = new int[sDamages.Length];
            for (int i = 0; i < sDamages.Length; i++)
            {
                damages[i] = (int.Parse(sDamages[i]));
            }
            Backtrack(springs.ToArray(), damages, ref count);
            Console.WriteLine($"Count: {count}");
            result += count;
        }
        return result;
    }

    public static long PartTwoSolution(StreamReader sr)
    {
        string s;
        int result = 0;
        while ((s = sr.ReadLine()) != null)
        {
            int count = 0;
            List<SpringState> springs = new List<SpringState>();
            string line = s.Split(' ')[0];
            foreach (char c in line)
            {
                springs.Add(FromChar(c));
            }
            string[] sDamages = s.Split(' ')[1].Split(',');
            int[] damages = new int[sDamages.Length];
            for (int i = 0; i < sDamages.Length; i++)
            {
                damages[i] = (int.Parse(sDamages[i]));
            }
            SpringState[] reference = springs.ToArray();
            for (int i = 0; i < 4; i++)
            {
                springs.Add(SpringState.Unknown);
                for (int j = 0; j < reference.Length; j++)
                {
                    springs.Add(reference[j]);
                }
            }
            int[] newDamages = new int[damages.Length * 5];
            for (int i = 0; i < 5 * damages.Length; i++)
            {
                newDamages[i] = damages[i % damages.Length];
            }

            PrintSpringState(springs.ToArray());
            Backtrack(springs.ToArray(), newDamages, ref count);
            Console.WriteLine($"Count: {count}");
            result += count;
        }
        return result;
    }

    enum SpringState
    {
        Unknown,
        Damaged,
        Intact,
        DamagedImmutable,
        IntactImmutable
    }

    static SpringState FromChar(char c)
    {
        switch (c)
        {
            case '?':
                return SpringState.Unknown;
            case '#':
                return SpringState.DamagedImmutable;
            case '.':
                return SpringState.IntactImmutable;
            default:
                throw new Exception("Invalid input");
        }
    }

    static string ToString(SpringState P)
    {
        switch (P)
        {
            case SpringState.Unknown:
                return "?";
            case SpringState.Damaged:
                return "#";
            case SpringState.DamagedImmutable:
                return "#";
            case SpringState.Intact:
                return ".";
            case SpringState.IntactImmutable:
                return ".";
            default:
                throw new Exception("Invalid state");
        }
    }

    static void PrintSpringState(SpringState[] P)
    {
        foreach (SpringState s in P)
        {
            Console.Write(ToString(s));
        }
    }

    static void Backtrack(SpringState[] P, int[] c, ref int count)
    {
        // PrintSpringState(P);
        if (Accept(P, c))
        {
            // Console.WriteLine(" Accepted");
            count++;
            return;
        }
        if (Reject(P, c))
        {
            // Console.WriteLine(" Rejected");
            return;
        }
        else
        {
            // Console.WriteLine();
        }
        SpringState[] s = First(P, c);
        while (s != null)
        {
            Backtrack(s, c, ref count);
            s = Next(s, c);
        }
    }

    static bool Reject(SpringState[] P, int[] c)
    {
        int totalDamaged = c.Sum();
        int totalUnknown = P.Count(s => s == SpringState.Unknown);
        int totalDamagedSet = P.Count(s => s == SpringState.Damaged || s == SpringState.DamagedImmutable);
        if (totalDamagedSet > totalDamaged)
        {
            return true;
        }
        if (totalDamagedSet + totalUnknown < totalDamaged)
        {
            return true;
        }

        int damagedCount = 0;
        int unknownCount = 0;
        int cIdx = 0;

        foreach (SpringState s in P)
        {
            if (s == SpringState.DamagedImmutable || s == SpringState.Damaged)
            {
                damagedCount++;
            }
            if (s == SpringState.Unknown)
            {
                unknownCount++;
            }
            if (s == SpringState.Intact || s == SpringState.IntactImmutable)
            {
                // Console.WriteLine($"damagedCount: {damagedCount} unkownCount: {unknownCount}");
                if (unknownCount > 0)
                {
                    return false;
                }
                if (damagedCount > 0)
                {
                    // cannot be a valid solution
                    if (damagedCount > c[cIdx])
                    {
                        return true;
                    }
                    if (damagedCount + unknownCount < c[cIdx])
                    {
                        return true;
                    }
                    if (cIdx < c.Length - 1)
                    {
                        cIdx++;
                    }
                }
                damagedCount = 0;
                unknownCount = 0;
            }
        }
        return false;
    }

    static bool Accept(SpringState[] P, int[] c)
    {
        if (P.Contains(SpringState.Unknown))
        {
            return false;
        }
        int currentIdx = 0;
        int damagedCount = 0;
        List<int> damagedGroups = new List<int>();
        for (int i = 0; i < P.Length; i++)
        {
            if (P[i] == SpringState.DamagedImmutable || P[i] == SpringState.Damaged)
            {
                damagedCount++;
            }
            if (P[i] == SpringState.Intact || P[i] == SpringState.IntactImmutable)
            {
                if (damagedCount > 0)
                {
                    damagedGroups.Add(damagedCount);
                    damagedCount = 0;
                    if (currentIdx < c.Length - 1)
                    {
                        currentIdx++;
                    }
                }
            }
        }
        if (damagedCount > 0)
        {
            damagedGroups.Add(damagedCount);
        }
        if (damagedGroups.Count != c.Length)
        {
            return false;
        }
        for (int j = 0; j < c.Length; j++)
        {
            if (damagedGroups[j] != c[j])
            {
                return false;
            }
        }
        return true;
    }

    static SpringState[] First(SpringState[] P, int[] c)
    {
        SpringState[] Pfirst = new SpringState[P.Length];
        P.CopyTo(Pfirst, 0);

        int firstUnknown = Array.IndexOf(P, SpringState.Unknown);

        if (firstUnknown == -1)
        {
            return null;
        }
        Pfirst[firstUnknown] = SpringState.Damaged;

        return Pfirst;
    }

    // this function should keep progressing the current branch only
    static SpringState[] Next(SpringState[] P, int[] c)
    {
        SpringState[] Pnext = new SpringState[P.Length];
        P.CopyTo(Pnext, 0);
        int lastDamaged = -1;
        int lastIntact = -1;
        for (int i = 0; i < P.Length; i++)
        {
            if (P[i] == SpringState.DamagedImmutable || P[i] == SpringState.IntactImmutable)
            {
                continue;
            }
            if (P[i] == SpringState.Damaged)
            {
                lastDamaged = i;
                continue;
            }
            if (P[i] == SpringState.Intact)
            {
                lastIntact = i;
                continue;
            }
        }
        if (lastDamaged > lastIntact)
        {
            Pnext[lastDamaged] = SpringState.Intact;
            return Pnext;
        }
        return null;
    }

    public static void PartOneTest()
    {
        StreamReader sr = GetInput("test-input2.txt");
        Console.WriteLine($"Part One Test: {PartOneSolution(sr)}");
    }

    public static void PartOne()
    {
        StreamReader sr = GetInput("input.txt");
        Console.WriteLine($"Part One: {PartOneSolution(sr)}");
    }

    public static void PartTwoTest()
    {
        StreamReader sr = GetInput("test-input2.txt");
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
        // Day12.PartOneTest();
        // Day12.PartOne();
        // Day12.PartTwoTest();
        Day12.PartTwo();
    }
}
