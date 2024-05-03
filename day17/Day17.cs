using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Day17
{
    public const int MAX_DIRECTION_HOLD = 3;
    public static long PartOneSolution(StreamReader sr)
    {
        string s;
        Grid grid = new Grid();
        int rowidx = 0;
        while ((s = sr.ReadLine()) != null)
        {
            grid._cols = s.Length;
            for (int colidx = 0; colidx < s.Length; colidx++)
            {
                int heatloss;
                int.TryParse(s[colidx].ToString(), out heatloss);
                grid.AddCityBlock(rowidx, colidx, heatloss);
            }
            rowidx++;
        }
        grid._rows = rowidx;

        HashSet<(int, int, int, int)> visited = new HashSet<(int, int, int, int)>();
        // <row, col, direction, moves in direction, total heatloss>
        List<(int, int, int, int, int)> queue = new List<(int, int, int, int, int)>();
        queue.Add((0, 0, -1, 0, 0));
        visited.Add((0, 0, -1, 0));

        while (queue.Count > 0)
        {
            var (row, col, dir, dircount, heatloss) = Dequeue(queue);
            // Console.WriteLine($"[({row}, {col}) {dir}, {dircount}, {heatloss}]");
            foreach (Direction nextDir in Enum.GetValues(typeof(Direction)))
            {
                // we cannot turn by 180 degrees
                bool invalidDir = false;
                switch (dir)
                {
                    case (int)Direction.Right:
                        if (nextDir == Direction.Left)
                        {
                            invalidDir = true;
                        }
                        break;

                    case (int)Direction.Left:
                        if (nextDir == Direction.Right)
                        {
                            invalidDir = true;
                        }
                        break;

                    case (int)Direction.Up:
                        if (nextDir == Direction.Down)
                        {
                            invalidDir = true;
                        }
                        break;

                    case (int)Direction.Down:
                        if (nextDir == Direction.Up)
                        {
                            invalidDir = true;
                        }
                        break;

                    default:
                        break;
                }
                if (invalidDir)
                {
                    continue;
                }
                bool isSameDir = (int)nextDir == dir;
                if (isSameDir && dircount == 3)
                {
                    // we cannot make more than 3 moves in the same direction
                    continue;
                }

                (int y, int x) = FromDirection(nextDir);
                int nextrow = row + y;
                int nextcol = col + x;

                // Console.WriteLine($"({nextrow}, {nextcol})");
                if (visited.Contains((nextrow, nextcol, (int)nextDir, dircount)))
                {
                    continue;
                }
                if (!grid._cityBlocks.ContainsKey((nextrow, nextcol)))
                {
                    continue;
                }

                int totalHeatloss = heatloss + grid._cityBlocks[(nextrow, nextcol)];
                if (nextrow == grid._rows - 1 && nextcol == grid._cols - 1)
                {
                    return totalHeatloss;
                }
                queue.Add(
                    (
                        nextrow,
                        nextcol,
                        (int)nextDir,
                        isSameDir ? dircount + 1 : 1,
                        totalHeatloss
                    )
                );
                // Console.WriteLine($"add to queue: ({nextrow}, {nextcol})");
                visited.Add((nextrow, nextcol, (int)nextDir, isSameDir ? dircount + 1 : 1));
            }
            // Console.ReadLine();
        }
        return -1;
    }

    public static (int, int, int, int, int) Dequeue(List<(int, int, int, int, int)> queue)
    {
        int idxLowestHeatloss = -1;
        int lowestHeatloss = Int32.MaxValue;
        for (int i = 0; i < queue.Count; i++)
        {
            int heatloss = queue[i].Item5;
            if (heatloss < lowestHeatloss)
            {
                lowestHeatloss = heatloss;
                idxLowestHeatloss = i;
            }
        }
        var result = queue[idxLowestHeatloss];
        queue.Remove(queue[idxLowestHeatloss]);
        return result;
    }

    public enum Direction
    {
        Right, Down, Left, Up
    }

    public static (int, int) FromDirection(Direction d)
    {
        switch (d)
        {
            case Direction.Right:
                return (0, 1);

            case Direction.Down:
                return (1, 0);

            case Direction.Left:
                return (0, -1);

            case Direction.Up:
                return (-1, 0);

            default:
                throw new Exception($"Cannot infer direction from {d}");
        }
    }

    private class Grid
    {
        public int _rows;
        public int _cols;
        public Dictionary<(int, int), int> _cityBlocks = new Dictionary<(int, int), int>();

        public void AddCityBlock(int row, int col, int heatloss)
        {
            _cityBlocks.Add((row, col), heatloss);
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
        Day17.PartOneTest();
        Day17.PartOne();
        // Day17.PartTwoTest();
        // Day17.PartTwo();
    }
}
