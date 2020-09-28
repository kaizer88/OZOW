using System.Collections.Generic;
using System.Linq;
using Ozow.GameOfLife.Domain.DomainModel;

namespace Ozow.GameOfLife.Domain.DomainServices
{
    public interface INeighborCounterService    
    {
        int Count(IList<IList<ICell>> grid, int rowNo,int colNo);
    }

    /// <summary>
    /// Handles logic for couting how many neibours a given cell has
    /// </summary>
    public class NeighborCounterService : INeighborCounterService
    {
        public int Count(IList<IList<ICell>> grid, int rowNo, int colNo)
        {
            var count = 0;
            var rowCount = grid.Count();
            var colCount = grid.First().Count();

            for(var r=rowNo-1; r < rowNo+2; r++)
                for(var c=colNo-1; c < colNo+2; c++)
                {
                    // Ignore center cell as it isn't a neighbor
                    if(r==rowNo && c == colNo)
                        continue;

                    // Don't evaluate cells left of the boundry
                    if(c-1 < 0)
                        continue;

                    // Don't evaluate cells right of the boundry
                    if(c > colCount)
                        continue;

                    // Don't evaluate cells above the top boundry
                    if(r-1 < 0)
                        continue;

                    // Don't evaluate cells below the bottom boundry
                    if(r > rowCount)
                        continue;
                    
                    var cell = grid.ToList()[r-1].ToList()[c-1];
                    count += (cell.IsAlive ? 1 : 0);
                }

            return count;
        }
    }
}