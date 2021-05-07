using System;
using System.IO;
using System.Linq;

namespace TestConsoleApp
{
  class Program
  {
    // Путь к файлу исходной матрицы.
    private const string inputFilePath = "input.txt";
    // (Числа в каждой строке файла должны быть разделены запятыми)
    // Пример:
    // -6,3,-8,-4
    // -8,1,-9,5
    // -1,-2,-3,-5

    // Путь к файлу отсортированной матрицы.
    private const string outputFilePath = "output.txt";

    static void Main(string[] args)
    {
      Console.WriteLine("Исходная матрица:");

      // Считать исходную матрицу из файла.
      double[,] matrix = ReadMatrixFromFile(inputFilePath);

      // Вывести исходную матрицу в консоль.
      PrintMatrix(matrix);

      // Сортирует в порядке возрастания столбцы матрицы, 
      //  в которых содержится хотя бы один положительный элемент.
      SortMatrixColumnsWithPositiveElements(matrix);

      // Вывести отсортированную матрицу в консоль.
      Console.WriteLine("Отсортированная матрица:");
      PrintMatrix(matrix);

      // Записать отсортированную матрицу в файл.
      WriteMatrixToFile(outputFilePath,
                        matrix);

      Console.ReadKey();
    }

    // Считывает данные из файла по переданному пути к файлу и возвращает многомерный массив (матрицу) чисел типа double.
    public static double[,] ReadMatrixFromFile(string inputFilePath)
    {
      // Считываем из файла элементы (числа типа double), разделённые символом 
      //  запятой (','), и преобразуем в массив массивов типа double.
      var matrix = File.ReadAllLines(inputFilePath)
                       .Select(x => (x.Split(',').Select(double.Parse).ToArray()))
                       .ToArray();
      return To2D(matrix);
    }

    // Записывает матрицу в файл по переданному пути.
    public static void WriteMatrixToFile(string outputFilePath,
                                         double[,] matrix)
    {
      string matrixString = "";
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          // Не писать ',' перед первой цифрой в ряду.
          if (j == 0)
            matrixString += Convert.ToString(matrix[i, j]);
          else
            matrixString += "," + Convert.ToString(matrix[i, j]);
        }
        matrixString += "\n";
      }

      File.WriteAllText(outputFilePath,
                        matrixString);
    }

    // Сортировка в порядке возрастания столбцов матрицы (многомерного массива), 
    //  в которых содержится хотя бы один положительный элемент.
    public static void SortMatrixColumnsWithPositiveElements(double[,] matrix)
    {
      int lastSortedColumn = -1;

      for (int i = 0; i < matrix.GetLength(1); i++)
      {
        for (int j = 0; j < matrix.GetLength(0); j++)
        {
          if (matrix[j, i] > 0 && i != lastSortedColumn)
          {
            SortColumn(matrix, i);
            lastSortedColumn = i;
            break;
          }
        }
      }
    }

    // Вывод матрицы (многомерного массива) в консоль.
    public static void PrintMatrix(double[,] matrix)
    {
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          Console.Write($"{matrix[i, j]}\t");
        }
        Console.WriteLine();
      }
    }

    // Сортировка по возрастанию элементов колонки 
    //  многомерного массива по заданному индексу.
    private static void SortColumn(double[,] matrix, int columnIndex)
    {
      double[] tempArray = new double[matrix.GetLength(0)];

      // Копируем колонку в одномерный массив.
      for (int i = 0; i < tempArray.Length; i++)
        tempArray[i] = matrix[i, columnIndex];

      // Сортируем элементы одномерного массива.
      Array.Sort(tempArray, (x, y) => x.CompareTo(y));

      // Записываем в исходный многомерный массив.
      for (int i = 0; i < matrix.GetLength(0); i++)
        matrix[i, columnIndex] = tempArray[i];
    }

    // Конвертация массива массивов в многомерный массив.
    private static T[,] To2D<T>(T[][] source)
    {
      try
      {
        int FirstDim = source.Length;

        // Выкидывает InvalidOperationException если source не прямоугольный массив массивов.
        int SecondDim = source.GroupBy(row => row.Length).Single().Key;

        var result = new T[FirstDim, SecondDim];
        for (int i = 0; i < FirstDim; ++i)
          for (int j = 0; j < SecondDim; ++j)
            result[i, j] = source[i][j];

        return result;
      }
      catch (InvalidOperationException)
      {
        throw new InvalidOperationException("Данный массив массивов не прямоугольный.");
      }
    }
  }
}
