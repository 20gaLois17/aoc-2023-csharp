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
        int c = 0;
        string label = "";
        int hash = 0;
        Dictionary<int, Box> boxes = new Dictionary<int, Box>();

        while ((c = sr.Read()) > -1)
        {
            if (c == (int)'=' || c == (int)'-')
            {
                switch (c)
                {
                    case (int)'=':
                        if (!boxes.ContainsKey(hash))
                        {
                            Box box = new Box();
                            int focalLength = sr.Read() - 48;
                            Lense lense = new Lense(label, focalLength);
                            box.AddLense(lense);
                            boxes.Add(hash, box);
                        }
                        else
                        {
                            int focalLength = sr.Read() - 48;
                            Lense lense = new Lense(label, focalLength);
                            boxes[hash].AddLense(lense);
                        }
                        sr.Read();
                        hash = 0;
                        label = "";
                        continue;

                    case (int)'-':
                        if (boxes.ContainsKey(hash))
                        {
                            boxes[hash].RemoveLense(label);
                        }
                        sr.Read();
                        hash = 0;
                        label = "";
                        continue;
                }
            }
            label += (char)c;
            hash += c;
            hash = (hash * 17) % 256;
        }
        int result = 0;
        foreach (KeyValuePair<int, Box> kvp in boxes)
        {
            Lense[] lenses = kvp.Value.GetLenses().ToArray();
            for (int i = 0; i < lenses.Length; i++)
            {
                result += (kvp.Key + 1) * (i + 1) * lenses[i].GetFocalLength();
            }
        }
        return result;
    }

    private class Box
    {
        private List<Lense> _lenses;

        public Box()
        {
            _lenses = new List<Lense>();
        }

        public void RemoveLense(string label)
        {
            int idx = _lenses.FindIndex(x => x.GetLabel() == label);
            if (idx > -1)
            {
                _lenses.RemoveAt(idx);
            }
        }

        public void AddLense(Lense lense)
        {
            int idx = _lenses.FindIndex(x => x.GetLabel() == lense.GetLabel());
            if (idx > -1)
            {
                _lenses[idx] = lense;
            }
            else
            {
                _lenses.Add(lense);
            }
        }

        public List<Lense> GetLenses()
        {
            return _lenses;
        }

        public override string ToString()
        {
            string s = "";
            foreach (Lense lense in _lenses)
            {
                s += $"[{lense}]";
            }
            return s;
        }
    }

    private class Lense
    {
        private string _label;
        private int _focalLength;

        public Lense(string label, int focalLength)
        {
            _label = label;
            _focalLength = focalLength;
        }

        public override string ToString()
        {
            return _label + _focalLength;
        }

        public int GetFocalLength()
        {
            return _focalLength;
        }

        public string GetLabel()
        {
            return _label;
        }
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
        Day15.PartOneTest();
        Day15.PartOne();
        Day15.PartTwoTest();
        Day15.PartTwo();
    }
}
