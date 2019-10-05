using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LDGJ45.Core.TileMaps
{
    public sealed class Grid<T>
    {
        private T[,] _gridElements;

        public Grid()
        {
        }

        public Grid(int width, int height, int cellWidth, int cellHeight)
        {
            _gridElements = PlotGrid(width, height, cellWidth, cellHeight);
        }

        public int Columns => _gridElements.GetLength(1);
        public int Rows => _gridElements.GetLength(0);

        public T this[int row, int col]
        {
            get => _gridElements[row, col];
            set => _gridElements[row, col] = value;
        }

        public bool IsInRange(int row, int col)
        {
            return col >= 0
                   && row >= 0
                   && col < Columns
                   && row < Rows;
        }

        public void Resize(int rows, int cols)
        {
            var newGrid = new T[rows, cols];

            var minRows = Math.Min(rows, Rows);
            var minCols = Math.Min(cols, Columns);

            for (var row = 0; row < minRows; row++)
            for (var col = 0; col < minCols; col++)
                newGrid[row, col] = _gridElements[row, col];

            _gridElements = newGrid;
        }

        private static T[,] PlotGrid(int width, int height, int cellWidth, int cellHeight)
        {
            return new T[height / cellHeight, width / cellWidth];
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var element in _gridElements)
                if (element != null)
                    yield return element;
        }
    }
}