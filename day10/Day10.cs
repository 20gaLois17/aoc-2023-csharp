using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

public class Day10
{
    public static int PartOneSolution(StreamReader sr)
    {
        string s;
        int boardWidth = 0;
        int boardIndex = 0;
        int startPosition = 0;
        int stepCount = 0;
        Dictionary<int, char> board = new Dictionary<int, char>();
        while ((s = sr.ReadLine()) != null)
        {
            boardWidth = s.Length;
            foreach (char c in s)
            {
                if (c == 'S')
                {
                    startPosition = boardIndex;
                }
                board.Add(boardIndex++, c);
            }
        }
        Direction[] validFirstDirections = GetValidFirstDirections(startPosition, boardWidth);
        List<Direction> startingDirections = new List<Direction>();
        foreach (Direction validFirstDirection in validFirstDirections)
        {
            int stepPosition = StepTo(startPosition, boardWidth, validFirstDirection);
            Direction[] viableDirections = GetViableIncomingDirections(StepTo(startPosition, boardWidth, validFirstDirection), board);
            if (viableDirections.Length > 0)
            {
                int directionIndex = Array.IndexOf(viableDirections, validFirstDirection);
                if (directionIndex != -1)
                {
                    startingDirections.Add(validFirstDirection);
                }
            }
        }
        stepCount++;
        Step[] steps = new Step[2];
        for (int k = 0; k < 2; k++)
        {
            steps[k] = new Step(StepTo(startPosition, boardWidth, startingDirections[k]), GetReverse(startingDirections[k]));
        }
        while (steps[0].position != steps[1].position)
        {
            for (int k = 0; k < 2; k++)
            {
                Direction[] viableDirections = GetViableOutgoingDirections(steps[k].position, board);
                int idx = Array.IndexOf(viableDirections, steps[k].cameFrom);
                Direction goTo = viableDirections[(idx + 1) % 2];
                steps[k] = new Step(StepTo(steps[k].position, boardWidth, goTo), GetReverse(goTo));
            }
            stepCount++;
        }
        return stepCount;
    }

    public static long PartTwoSolution(StreamReader sr)
    {
        string s;
        while ((s = sr.ReadLine()) != null)
        {
            Console.WriteLine(s);
        }
        return 0;
    }

    enum Direction
    {
        NORTH,
        EAST,
        SOUTH,
        WEST
    }

    private class Step
    {
        public int position;
        public Direction cameFrom;

        public Step(int position, Direction cameFrom)
        {
            this.position = position;
            this.cameFrom = cameFrom;
        }
    }

    private static int StepTo(int position, int boardWidth, Direction direction)
    {
        switch (direction)
        {
            case Direction.NORTH:
                return position - boardWidth;
            case Direction.EAST:
                return position + 1;
            case Direction.SOUTH:
                return position + boardWidth;
            case Direction.WEST:
                return position - 1;
            default:
                return position;
        }
    }

    // find valid directions which are connected to the given positions by pipes
    private static Tuple<Direction, Direction> GetValidDirections(int position, int boardWidth)
    {
        // there are edge cases in which we are only allowed to check 3 of 4 possible directions
        Direction[] validDirections = new Direction[2];
        return new Tuple<Direction, Direction>(validDirections[0], validDirections[1]);
    }

    private static Direction[] GetViableIncomingDirections(int position, Dictionary<int, char> board)
    {
        switch (board[position])
        {
            case '|':
                return new Direction[] { Direction.NORTH, Direction.SOUTH };
            case '-':
                return new Direction[] { Direction.EAST, Direction.WEST };
            case 'F':
                return new Direction[] { Direction.NORTH, Direction.WEST };
            case 'L':
                return new Direction[] { Direction.SOUTH, Direction.WEST };
            case '7':
                return new Direction[] { Direction.NORTH, Direction.EAST };
            case 'J':
                return new Direction[] { Direction.SOUTH, Direction.EAST };
            default:
                return new Direction[] { };
        }
    }
    private static Direction[] GetViableOutgoingDirections(int position, Dictionary<int, char> board)
    {
        switch (board[position])
        {
            case '|':
                return new Direction[] { Direction.NORTH, Direction.SOUTH };
            case '-':
                return new Direction[] { Direction.EAST, Direction.WEST };
            case 'F':
                return new Direction[] { Direction.SOUTH, Direction.EAST };
            case 'L':
                return new Direction[] { Direction.NORTH, Direction.EAST };
            case '7':
                return new Direction[] { Direction.SOUTH, Direction.WEST };
            case 'J':
                return new Direction[] { Direction.NORTH, Direction.WEST };
            default:
                return new Direction[] { };
        }
    }
    private static Direction GetReverse(Direction direction)
    {
        switch (direction)
        {
            case Direction.NORTH:
                return Direction.SOUTH;
            case Direction.EAST:
                return Direction.WEST;
            case Direction.SOUTH:
                return Direction.NORTH;
            case Direction.WEST:
                return Direction.EAST;
            default:
                throw new ArgumentException("Invalid direction");
        }
    }

    // handle the esge cases
    private static Direction[] GetValidFirstDirections(int position, int boardWidth)
    {
        if (position % boardWidth == 0)
        {
            return new Direction[] { Direction.EAST, Direction.SOUTH, Direction.NORTH };
        }
        if ((position + 1) % boardWidth == 0)
        {
            return new Direction[] { Direction.WEST, Direction.SOUTH, Direction.NORTH };
        }
        return new Direction[] { Direction.NORTH, Direction.SOUTH, Direction.EAST, Direction.WEST };
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
        // Day10.PartOneTest();
        // Day10.PartOne();
        Day10.PartTwoTest();
        // Day10.PartTwo();
    }
}
