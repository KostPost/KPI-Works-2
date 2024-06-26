﻿namespace Laba4;

public class Figure
{
    protected (double X, double Y)[] vertices;

    public Figure(params (double, double)[] vertices)
    {
        this.vertices = vertices;
    }

    protected double SideLength(int index1, int index2)
    {
        if (index1 < 0 || index2 < 0 || index1 >= vertices.Length || index2 >= vertices.Length)
        {
            throw new ArgumentOutOfRangeException("Index out of range");
        }

        var (x1, y1) = vertices[index1];
        var (x2, y2) = vertices[index2];
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }
}