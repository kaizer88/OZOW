using System;
using System.Collections.Generic;
using System.Linq;
using Ozow.GameOfLife.Domain.DomainModel;

namespace Ozow.GameOfLife.Domain.DomainServices
{
    public interface IGenerationService
    {        
        IList<IList<ICell>> NextGeneration(IList<IList<ICell>> grid);
    }

    /// <summary>
    /// This class takes as input the current game state, and returns a new game state
    /// </summary>
    public class GenerationService : IGenerationService
    {
        #region Dependencies
        private readonly INeighborCounterService _neighbourCounterSvc;
        #endregion

        #region Constructor
        public GenerationService(INeighborCounterService neighbourCounterSvc)
        {
            _neighbourCounterSvc = neighbourCounterSvc;
        }
        #endregion

        public IList<IList<ICell>> NextGeneration(IList<IList<ICell>> oldGrid)
        {
            var newGrid = new List<IList<ICell>>();

            // Creat a new grid, taking the old grid as input
            var rowNo = 0;
            var colNo = 0;
            oldGrid.ToList().ForEach(r => {
                var row = new List<ICell>();
                rowNo++;
                colNo=1;

                r.ToList().ForEach(c => {
                    var shouldLive = _shouldLive(oldGrid, rowNo, colNo);
                    var cell = new Cell(shouldLive);
                    row.Add(cell);
                    colNo++;
                });

                newGrid.Add(row);
            });

            return newGrid;
        }

        #region Private Methods
        private bool _shouldLive(IList<IList<ICell>> grid, int rowNo, int colNo)
        {            
            var neighbourCount = _neighbourCounterSvc.Count(grid,rowNo,colNo);
            var oldCellAlive = grid[rowNo-1][colNo-1].IsAlive;

            // Old cell alive?
            if(oldCellAlive)
            {
                if(neighbourCount < 2)
                    return false; // <-- Any live cell with fewer than two live neighbours dies, as if by underpopulation.
                else if (neighbourCount > 3)  
                    return false; // <-- Any live cell with more than three live neighbours dies, as if by overpopulation.
                else
                    return true; // <-- Any live cell with two or three live neighbours lives on to the next generation.
            }
            else
            {
                //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                return neighbourCount == 3;
            }
        }
        #endregion
    }
}