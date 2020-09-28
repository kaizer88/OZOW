using System;
using System.Collections.Generic;
using System.Linq;
using Ozow.GameOfLife.Domain.DomainModel;

namespace Ozow.GameOfLife.Tests.Builders
{
    public class GridBuilder
    {
        private int _rowCount = Faker.RandomNumber.Next(2,6);
        private int _colCount = Faker.RandomNumber.Next(2,6);
        private List<Tuple<int,int>> _activeCells = new List<Tuple<int,int>>();

        internal IList<IList<ICell>> Build()
        {
            var grid = new List<IList<ICell>>();

            for(var r=0; r<_rowCount; r++){
                var rowList = new List<ICell>();
                for(var c=0; c<_colCount; c++)
                    rowList.Add( _createCell(r+1,c+1));
                grid.Add(rowList);
            }

            return grid;
        }


        internal GridBuilder WithRows(int rows)
        {
            _rowCount = rows;
            return this;
        }

        internal GridBuilder WithCols(int cols)
        {
            _colCount= cols;
            return this;
        }

        internal GridBuilder WithActiveCell(int row, int col)
        {
            _activeCells.Add(new Tuple<int,int>(row, col));
            return this;
        }

        private ICell _createCell(int row, int col)
        {            
            return _activeCells.Any(x=> x.Item1 == row && x.Item2 == col) 
                ? new Cell(true)
                : new Cell(false);
        }

    }
}