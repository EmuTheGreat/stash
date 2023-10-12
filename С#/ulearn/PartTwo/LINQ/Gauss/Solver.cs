using System;
using System.Collections.Generic;
using System.Linq;

namespace GaussAlgorithm;

public class Solver
{
    public double[] Solve(double[][] matrix, double[] freeMembers) => 
        GaussMethod(ConnectMatrix(matrix, freeMembers));

    public static double[] GaussMethod(double[][] matrix)
    {
        var rows = matrix.Length;
        var used = new List<int>();
        var colums = matrix[0].Length;
        var result = new double[colums - 1];

        for (int j = 0; j < colums - 1; j++)
            for (int i = 0; i < rows; i++)
            {
                if (matrix[i][j] == 0 || used.Contains(i)) continue;
                used.Add(i);
                for (int k = 0; k < rows; k++)
                {
                    if (k == i) continue;
                    var c = matrix[k][j] / matrix[i][j];
                    for (int m = 0; m < colums; m++)
                        matrix[k][m] -= matrix[i][m] * matrix[k][j] / matrix[i][j];
                }
                break;
            }

        foreach (var row in matrix)
        {
            if (row.All(x => x == 0)) continue;
            if (row.Last() != 0
                && row.Take(colums - 1).All(x => x == 0)
                || row.Any(x => x is double.NaN)) throw new NoSolutionException("");

            var index = 0;
            for (int j = 0; j < colums - 1; j++)
                if (Math.Round(row[j], 1) != 0) { index = j; break; }
            result[index] = row.Last() / row[index];
        }
        return result;
    }

    public static double[][] ConnectMatrix(double[][] matrix, double[] freeMembers)
    {
        var cols = matrix[0].Length + 1;
        var rows = matrix.Length;
        var expandedMatrix = new double[rows][];

        for (int i = 0; i < rows; ++i) expandedMatrix[i] = new double[cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols - 1; j++)
                expandedMatrix[i][j] = matrix[i][j];
            expandedMatrix[i][cols - 1] = freeMembers[i];
        }
        return expandedMatrix;
    }
}