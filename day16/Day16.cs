using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Day16
{
    public static long PartOneSolution(StreamReader sr)
    {
        Grid grid = new Grid();
        string s;
        int rowidx = 0;
        while ((s = sr.ReadLine()) != null)
        {
            grid._cols = s.Length;
            for (int colidx = 0; colidx < s.Length; colidx++)
            {
                Cell cell = new Cell(CellTypeFromChar(s[colidx]));
                grid._cells.Add((rowidx, colidx), cell);
            }
            rowidx++;
        }
        grid._rows = rowidx;

        grid._beams.Add(new Beam((0, 0), Direction.Right));

        for (int i = 0; i < 810; i++)
        {
            if (i % (grid._rows) == 0)
            {
                grid.PurgeBeams();
            }
            grid.NextTick();
            Console.ReadLine();
            Console.Clear();
            grid.PrintGrid(true);
            Console.WriteLine($"Iteration: {i}");
            Console.WriteLine($"BeamCount: {grid._beams.Count}");
        }

        return grid.GetCountEnergized();
    }

    public static long PartTwoSolution(StreamReader sr)
    {
        List<int> counts = new List<int> { -1 };
        Grid grid = new Grid();
        string s;
        int rowidx = 0;
        while ((s = sr.ReadLine()) != null)
        {
            grid._cols = s.Length;
            for (int colidx = 0; colidx < s.Length; colidx++)
            {
                Cell cell = new Cell(CellTypeFromChar(s[colidx]));
                grid._cells.Add((rowidx, colidx), cell);
            }
            rowidx++;
        }
        grid._rows = rowidx;

        for (int _row = 0; _row < grid._rows; _row++)
        {
            for (int _col = 0; _col < grid._cols; _col++)
            {
                if (_row == 0 || _col == 0 || _row == grid._rows - 1 || _col == grid._cols - 1)
                {
                    foreach (Direction dir in Enum.GetValues(typeof(Direction)))
                    {
                        grid._beams = new List<Beam>();
                        Beam beam = new Beam((_row, _col), dir);
                        grid._beams.Add(beam);
                        for (int i = 0; i < 800; i++)
                        {
                            grid.NextTick();
                            // grid.PrintGrid(true);
                            // Console.ReadLine();
                            // Console.Clear();
                        }
                        counts.Add(grid.GetCountEnergized());
                        grid.ResetCells();
                    }
                }
            }
        }
        grid._rows = rowidx;
        return counts.Max();
    }

    // we should move all the beam logic inside this class
    private class Grid
    {
        public int _rows;
        public int _cols;
        public Dictionary<(int, int), Cell> _cells = new Dictionary<(int, int), Cell>();
        public List<Beam> _beams = new List<Beam>();

        public void PrintGrid(bool showEnergized = false)
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int k = 0; k < _cols; k++)
                {
                    if (_cells[(i, k)].energized)
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write(CellTypeToChar(_cells[(i, k)]._type));
                    }
                }
                Console.WriteLine();
            }
        }

        public void PurgeBeams()
        {
            Beam[] beams = _beams.ToArray();
            _beams.Clear();
            foreach (Beam _beam in beams)
            {
                if (IsOutOfBounds(_beam) || _beam.GetAge() > _rows * _cols)
                {
                    continue;
                }
                _beams.Add(_beam);
            }
        }

        public void ResetCells()
        {
            foreach (KeyValuePair<(int, int), Cell> kvp in _cells)
            {
                kvp.Value.energized = false;
            }
        }

        public void NextTick()
        {
            List<Beam> newBeams = new List<Beam>();
            foreach (Beam beam in _beams)
            {
                if (IsOutOfBounds(beam))
                {
                    continue;
                }
                Cell cell = _cells[beam.GetPosition()];
                cell.Energize();
                switch (cell._type)
                {
                    case CellType.EMPTY:
                        break;

                    case CellType.MIRROR_TR:
                        switch (beam.GetDirection())
                        {
                            case Direction.Right:
                                beam.SetDirection(Direction.Up);
                                break;

                            case Direction.Left:
                                beam.SetDirection(Direction.Down);
                                break;

                            case Direction.Up:
                                beam.SetDirection(Direction.Right);
                                break;
                            case Direction.Down:
                                beam.SetDirection(Direction.Left);
                                break;
                        }
                        break;

                    case CellType.MIRROR_BR:
                        switch (beam.GetDirection())
                        {
                            case Direction.Right:
                                beam.SetDirection(Direction.Down);
                                break;

                            case Direction.Left:
                                beam.SetDirection(Direction.Up);
                                break;

                            case Direction.Up:
                                beam.SetDirection(Direction.Left);
                                break;
                            case Direction.Down:
                                beam.SetDirection(Direction.Right);
                                break;
                        }
                        break;

                    case CellType.SPLITTER_H:
                        switch (beam.GetDirection())
                        {
                            case Direction.Right:
                                break;

                            case Direction.Left:
                                break;

                            default:
                                beam.SetDirection(Direction.Left);
                                Beam newBeam = new Beam(beam.GetPosition(), Direction.Right);
                                newBeam.Move();
                                newBeams.Add(newBeam);
                                break;
                        }
                        break;

                    case CellType.SPLITTER_V:
                        switch (beam.GetDirection())
                        {
                            case Direction.Up:
                                break;

                            case Direction.Down:
                                break;

                            default:
                                beam.SetDirection(Direction.Up);
                                Beam newBeam = new Beam(beam.GetPosition(), Direction.Down);
                                newBeam.Move();
                                newBeams.Add(newBeam);
                                break;
                        }
                        break;
                }
                beam.Move();
            }
            foreach (Beam _newBeam in newBeams)
            {
                _beams.Add(_newBeam);
            }
        }

        public bool IsOutOfBounds(Beam beam)
        {
            var pos = beam.GetPosition();
            if (pos.Item1 >= _rows || pos.Item1 < 0)
            {
                return true;
            }
            if (pos.Item2 >= _cols || pos.Item2 < 0)
            {
                return true;
            }
            return false;
        }

        public int GetCountEnergized()
        {
            int result = 0;
            foreach (KeyValuePair<(int, int), Cell> kvp in _cells)
            {
                if (kvp.Value.energized)
                {
                    result++;
                }
            }
            return result;
        }
    }

    private class Beam
    {
        private Direction _direction;
        private (int, int) _position;
        private int _age;

        public Beam((int, int) position, Direction direction)
        {
            _direction = direction;
            _position = position;
            _age = 0;
        }

        public void SetDirection(Direction direction)
        {
            _direction = direction;
        }

        public (int, int) GetPosition()
        {
            return _position;
        }

        public Direction GetDirection()
        {
            return _direction;
        }

        public void Move()
        {
            switch (_direction)
            {
                case Direction.Right:
                    _position.Item2++;
                    break;
                case Direction.Left:
                    _position.Item2--;
                    break;
                case Direction.Up:
                    _position.Item1--;
                    break;
                case Direction.Down:
                    _position.Item1++;
                    break;
            }
            _age++;
        }

        public int GetAge()
        {
            return _age;
        }

        public override string ToString()
        {
            return $"({_position.Item1}, {_position.Item2}) {_direction}";
        }
    }

    private class Cell
    {
        public CellType _type;
        public bool energized = false;

        public Cell(CellType type)
        {
            _type = type;
        }

        public void Energize()
        {
            energized = true;
        }
    }

    enum CellType
    {
        MIRROR_TR,   // '/'
        MIRROR_BR,   // '\'
        SPLITTER_H,  // '-'
        SPLITTER_V,  // '|'
        EMPTY,       // '.'
    }

    static char CellTypeToChar(CellType cell)
    {
        switch (cell)
        {
            case CellType.MIRROR_TR:
                return '/';
            case CellType.MIRROR_BR:
                return '\\';
            case CellType.SPLITTER_H:
                return '-';
            case CellType.SPLITTER_V:
                return '|';
            case CellType.EMPTY:
                return '.';
            default:
                throw new Exception($"Invalid cell type: {cell}");
        }

    }

    static CellType CellTypeFromChar(char c)
    {
        switch (c)
        {
            case '/':
                return CellType.MIRROR_TR;
            case '\\':
                return CellType.MIRROR_BR;
            case '-':
                return CellType.SPLITTER_H;
            case '|':
                return CellType.SPLITTER_V;
            case '.':
                return CellType.EMPTY;
            default:
                throw new Exception($"Invalid cell: {c}");
        }
    }

    enum Direction
    {
        Up, Down, Left, Right
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
        // Day16.PartOneTest();
        // Day16.PartOne();
        // Day16.PartTwoTest();
        Day16.PartTwo();
    }
}
