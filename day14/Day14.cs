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

    private class Grid
    {
        public Dictionary<(int, int), CellState> grid { get; }
        private int _columns;
        private int _rows;

        public Grid(Dictionary<(int, int), CellState> grid, int rows, int columns)
        {
            this.grid = grid;
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

        public void Tilt(Tilt tilt)
        {
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
                            if (grid[(i - 1, k)] == CellState.FLOOR)
                            {
                                grid[(i - 1, k)] = CellState.MOVABLE_ROCK;
                                grid[(i, k)] = CellState.FLOOR;
                                stable = false;
                            }
                        }
                    }
                }
                while (!stable);
            }
        }
    }

    enum CellState
    {
        FLOOR,
        MOVABLE_ROCK,
        STATIC_ROCK,
    }

    enum Tilt
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
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
        Day14.PartOneTest();
        Day14.PartOne();
        // Day14.PartTwoTest();
        // Day14.PartTwo();
    }
}
