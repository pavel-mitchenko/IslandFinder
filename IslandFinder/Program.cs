using System;
using System.Threading;

namespace IslandFinder
{
  class Program
  {

    static readonly char[,] sea = new char[,]
    {
      {'x','x','-','-','-','-','-','-' },
      {'-','x','-','-','-','-','-','-' },
      {'x','x','-','-','-','x','-','-' },
      {'-','-','-','-','x','-','-','-' },
      {'-','-','-','-','-','-','-','x' },
      {'-','-','-','-','-','-','-','x' },
      {'-','-','-','x','x','-','-','-' },
      {'-','-','-','x','x','-','-','-' },
      {'-','-','-','-','-','-','-','-' },
      {'-','-','-','-','-','-','-','x' },
    };

    static readonly int Height = sea.GetLength(0);
    static readonly int Width = sea.GetLength(1);

    static readonly bool[,] checkedCells = new bool[Height, Width];

    static void Main(string[] args)
    {
      int islandQuantity = 0;

      for (var y = 0; y < Height; y++)
      {
        for (int x = 0; x < Width; x++)
        {
          if (IsChecked(y, x))
          {
            continue;
          }

          MarkAsChecked(y, x);

          if (IsFull(y, x))
          {
            CheckNeighbors(y, x);
            islandQuantity++;
            PrintCurrentQuantity(islandQuantity);
          }
        }
      }

      Console.WriteLine();
      Console.ReadLine();
    }

    private static void CheckNeighbors(int yCenter, int xCenter)
    {
      for (int y = yCenter - 1; y <= yCenter + 1; y++)
      {
        for (int x = xCenter - 1; x <= xCenter + 1; x++)
        {
          if (IsOutOfSea(y, x) || IsChecked(y, x))
          {
            continue;
          }

          MarkAsChecked(y, x);

          if (IsFull(y, x))
          {
            CheckNeighbors(y, x);
          }
        }
      }
    }

    private static bool IsOutOfSea(int x, int y) => x < 0 || x >= Height || y < 0 || y >= Width;

    private static bool IsFull(int y, int x) => sea[y, x] == 'x';

    private static void MarkAsChecked(int y, int x)
    {
      checkedCells[y, x] = true;
      Print(y, x);
    }

    private static bool IsChecked(int y, int x) => checkedCells[y, x];

    #region for debug purposes
    private static void Print(int y, int x)
    {
      const int indent = 3;
      Console.SetCursorPosition(x + indent, y + indent);
      Console.ForegroundColor = IsFull(y, x) ? ConsoleColor.Red : ConsoleColor.Blue;
      Console.Write(sea[y, x]);
      Thread.Sleep(300);
    }

    private static void PrintCurrentQuantity(int islandQuantity)
    {
      const int indent = 1;
      Console.SetCursorPosition(indent, indent);
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write(islandQuantity);
    }
    #endregion
  }
}
