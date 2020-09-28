using System;

namespace Ozow.GameOfLife.Domain.DomainModel
{
    public interface ICell
    {
        bool IsAlive { get; }
    }

    public class Cell : ICell
    {
        public bool IsAlive { get; private set; }

        public Cell()
        {
            // Random initial cell state
            this.IsAlive = new Random().Next(1, int.MaxValue) % 2 == 0;
        }        
        public Cell(bool isAlive)
        {            
            this.IsAlive = isAlive;
        }        

    }
}