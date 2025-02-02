﻿using MathMTS.algebra.exceptions;
using MathMTS.algebra.Interfaces;

namespace MathMTS.algebra;

/// <summary>
/// Vector class to provide functionality for vector operations.
/// </summary>
public abstract class Vector : IVector
{
    private double[] matrix;
    private int dimension;

    public int Dimension
    {
        get { return dimension; }
        set { dimension = value; }
    }

    public double[] Values => matrix;

    public Vector(double[] matrix)
    {
        this.matrix = matrix;
        this.Dimension = matrix.Length;
    }
    /// <summary>
    /// Creates a vector from 2 points where the first point 
    /// is the start and the second point the end.
    /// </summary>
    public Vector(IPoint start, IPoint end)
    {
        if (start.Coordinates.Length == end.Coordinates.Length)
        {
            throw new InvalidVectorOperationException(
                "You can't create a vector of different dimensions");
        }

        for (int i = 0; i < start.Coordinates.Length; i++)
        {
            end.Coordinates[i] -= start.Coordinates[i];
        }
        this.matrix = end.Coordinates;
        this.dimension = end.Coordinates.Length;
    }

    public double GetLength()
    {
        double val = 0;
        foreach (var value in Values)
        {
            val += value * value;
        }
        return Math.Sqrt(val);
    }

    public IVector Normalize()
    {
        IVector vec = this;
        var alpha = 1 / GetLength();
        return alpha * vec;
    }

    public virtual IVector CorssProduct(ref IVector buffer, IVector other)
    {
        var size = this.Values.Length;
        for (int i = 0; i < this.Values.Length; i++)
        {

            var a = this.Values[(i + 1) % size] * other.Values[(i + 2) % size];
            var b = this.Values[(i + 2) % size] * other.Values[(i + 1) % size];
            buffer.Values[i] = a - b;
        }

        return buffer;
    }

    public double Length()
    {
        double sum = 0;
        foreach (var val in matrix)
        {
            sum += val * val;
        }
        return Math.Sqrt(sum);
    }
}
