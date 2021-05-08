# Sort matrix columns with positive elements (C#).

This source code is an example of how to sort matrix columns using the C# programming language.  
This project is a homework from my college programming teacher (2021).

## Problem statement:

> Create a C# program that checks whether there is at least one column in the matrix containing a positive element(-s).  
> Arrange elements in this column(-s) in ascending order.  
> Take the matrix from the file, write the result to the file.

## Implementation:

Constants (file paths):

```csharp
private const string inputFilePath = "input.txt";
private const string outputFilePath = "output.txt";
```

Functions:

```csharp
// Reads data from the file by the passed path
//  and returns a multidimensional array (matrix) of double numbers.
public static double[,] ReadMatrixFromFile(string inputFilePath)

// Writes the matrix to the file by the passed path.
public static void WriteMatrixToFile(string outputFilePath,
                                     double[,] matrix)

// Sort in ascending order the columns of a matrix (multidimensional array)
//  that contains at least one positive element.
public static void SortMatrixColumnsWithPositiveElements(double[,] matrix)

// Prints the matrix (multidimensional array) to the console window.
public static void PrintMatrix(double[,] matrix)

// Sort in ascending order of the column elements
//  of a multidimensional array by the specified index.
private static void SortColumn(double[,] matrix, int columnIndex)

// Converts the jagged array to a multidimensional array.
// T[][] -> T[,]
private static T[,] To2D<T>(T[][] source)
```

## Usage example:

Input (`input.txt`):

```
-6,3,-8,-4
-8,1,-9,5
-1,-2,-3,-5
```

Output (`output.txt`):

```
-6,-2,-8,-5
-8,1,-9,-4
-1,3,-3,5
```
