using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Day14
{
    public static long PartOneSolution(StreamReader sr)
    {
        string s;
        int row = 0;
        Dictionary<(int, int), CellState> grid = new Dictionary<(int, int), CellState>();
        while ((s = sr.ReadLine()) != null)
        {
            for (int column = 0; column < s.Length; column++)
            {
                grid.Add((row, column), FromChar(s[column]));
            }
            row++;
        }
        Grid g = new Grid(grid, row, grid.Count / (row));

        g.Tilt(Tilt.UP);
        g.Print();

        return g.GetTotalLoad();
    }

    public static long PartTwoSolution(StreamReader sr)
    {
        string s;
        int row = 0;
        Dictionary<(int, int), CellState> grid = new Dictionary<(int, int), CellState>();
        while ((s = sr.ReadLine()) != null)
        {
            for (int column = 0; column < s.Length; column++)
            {
                grid.Add((row, column), FromChar(s[column]));
            }
            row++;
        }
        Grid g = new Grid(grid, row, grid.Count / (row));

        string firstRecurring = "";
        HashSet<string> seen = new HashSet<string>();
        while (true)
        {
            g.Cycle();
            if (seen.Contains(g.ToString()))
            {
                firstRecurring = g.ToString();
                break;
            }
            else
            {
                seen.Add(g.ToString());
            }
        }
        g.Reset();
        int count = 0;
        int first = -1;
        int second = -1;
        while (first < 0 || second < 0)
        {
            count++;
            g.Cycle();
            if (g.ToString() == firstRecurring)
            {
                if (first == -1)
                {
                    first = count;
                }
                else
                {
                    second = count;
                    break;
                }

            }
        }
        Console.WriteLine($"First: {first}, Second: {second} cycle length: {second - first + 1}");
        int cycleLength = second - first;
        int targetIteration = (1000000000 - first) % cycleLength;

        g.Reset();
        for (int i = 0; i < first + targetIteration; i++)
        {
            Console.WriteLine($"{i} {g.GetTotalLoad()}");
            g.Cycle();
        }
        return g.GetTotalLoad();
    }

    private class Grid
    {
        public Dictionary<(int, int), CellState> grid { get; set; }
        private Dictionary<(int, int), CellState> _grid;
        private int _columns;
        private int _rows;
        private Tilt _tilt;

        public Grid(Dictionary<(int, int), CellState> grid, int rows, int columns)
        {
            this.grid = grid;
            this._grid = new Dictionary<(int, int), CellState>(grid);
            this._rows = rows;
            this._columns = columns;
        }

        public int GetWidth()
        {
            return this._columns;
        }

        public int GetHeight()
        {
            return this._rows;
        }

        public void Reset()
        {
            grid = _grid;
            _grid = new Dictionary<(int, int), CellState>(_grid);
        }

        public int GetTotalLoad()
        {
            int result = 0;
            for (int i = 0; i < GetHeight(); i++)
            {
                for (int k = 0; k < GetWidth(); k++)
                {
                    if (grid[(i, k)] != CellState.MOVABLE_ROCK)
                    {
                        continue;
                    }
                    result += GetHeight() - i;
                }
            }
            return result;
        }

        public void Print()
        {
            for (int i = 0; i < GetWidth(); i++)
            {
                for (int k = 0; k < GetHeight(); k++)
                {
                    Console.Write(ToChar(grid[(i, k)]));
                }
                Console.WriteLine();
            }
        }

        public string ToString()
        {
            string s = "";
            for (int i = 0; i < GetHeight(); i++)
            {
                for (int k = 0; k < GetWidth(); k++)
                {
                    s += ToChar(grid[(i, k)]);
                }
            }
            s += _tilt;
            return s;
        }

        public void Cycle()
        {
            foreach (Day14.Tilt tilt in Enum.GetValues(typeof(Day14.Tilt)))
            {
                Tilt(tilt);
            }
        }

        public void Tilt(Tilt tilt)
        {
            _tilt = tilt;
            if (tilt == Day14.Tilt.UP)
            {
                bool stable = true;
                do
                {
                    stable = true;
                    for (int i = 0; i < GetHeight(); i++)
                    {
                        for (int k = 0; k < GetWidth(); k++)
                        {
                            if (grid[(i, k)] != CellState.MOVABLE_ROCK || i == 0)
                            {
                                continue;
                            }
                            if (grid[(i - 1, k)] != CellState.FLOOR)
                            {
                                continue;
                            }
                            grid[(i - 1, k)] = CellState.MOVABLE_ROCK;
                            grid[(i, k)] = CellState.FLOOR;
                            stable = false;
                        }
                    }
                }
                while (!stable);
            }

            if (tilt == Day14.Tilt.RIGHT)
            {
                bool stable = true;
                do
                {
                    stable = true;
                    for (int k = GetWidth() - 1; k >= 0; k--)
                    {
                        for (int i = 0; i < GetHeight(); i++)
                        {
                            if (grid[(i, k)] != CellState.MOVABLE_ROCK || k == GetWidth() - 1)
                            {
                                continue;
                            }
                            if (grid[(i, k + 1)] != CellState.FLOOR)
                            {
                                continue;
                            }
                            grid[(i, k + 1)] = CellState.MOVABLE_ROCK;
                            grid[(i, k)] = CellState.FLOOR;
                            stable = false;
                        }
                    }
                }
                while (!stable);
            }

            if (tilt == Day14.Tilt.DOWN)
            {
                bool stable = true;
                do
                {
                    stable = true;
                    for (int i = GetHeight() - 1; i >= 0; i--)
                    {
                        for (int k = 0; k < GetWidth(); k++)
                        {
                            if (grid[(i, k)] != CellState.MOVABLE_ROCK || i == GetHeight() - 1)
                            {
                                continue;
                            }
                            if (grid[(i + 1, k)] != CellState.FLOOR)
                            {
                                continue;
                            }
                            grid[(i + 1, k)] = CellState.MOVABLE_ROCK;
                            grid[(i, k)] = CellState.FLOOR;
                            stable = false;
                        }
                    }
                }
                while (!stable);
            }

            if (tilt == Day14.Tilt.LEFT)
            {
                bool stable = true;
                do
                {
                    stable = true;
                    for (int k = 0; k < GetWidth(); k++)
                    {
                        for (int i = 0; i < GetHeight(); i++)
                        {
                            if (grid[(i, k)] != CellState.MOVABLE_ROCK || k == 0)
                            {
                                continue;
                            }
                            if (grid[(i, k - 1)] != CellState.FLOOR)
                            {
                                continue;
                            }
                            grid[(i, k - 1)] = CellState.MOVABLE_ROCK;
                            grid[(i, k)] = CellState.FLOOR;
                            stable = false;
                        }
                    }
                }
                while (!stable);
            }
        }
    }

    enum CellState
    {
        FLOOR, MOVABLE_ROCK, STATIC_ROCK,
    }

    enum Tilt
    {
        UP, LEFT, DOWN, RIGHT,
    }

    private static CellState FromChar(char c)
    {
        switch (c)
        {
            case '.':
                return CellState.FLOOR;

            case '#':
                return CellState.STATIC_ROCK;

            case 'O':
                return CellState.MOVABLE_ROCK;

            default:
                throw new Exception($"unknown CellState '{c}'");

        }
    }
    private static char ToChar(CellState s)
    {
        switch (s)
        {
            case CellState.FLOOR:
                return '.';

            case CellState.STATIC_ROCK:
                return '#';

            case CellState.MOVABLE_ROCK:
                return 'O';
            default:
                throw new Exception($"not a valid CellState {s}");
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
        // Day14.PartOneTest();
        // Day14.PartOne();
        // Day14.PartTwoTest();
        Day14.PartTwo();
    }
}
