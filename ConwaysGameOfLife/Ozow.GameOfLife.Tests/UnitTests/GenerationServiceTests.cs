using System;
using System.Linq;
using NUnit.Framework;
using Ozow.GameOfLife.Domain.DomainServices;
using Ozow.GameOfLife.Tests.Builders;

namespace Ozow.GameOfLife.Tests.UnitTests
{
    public class GenerationServiceTests
    {
        [Test]
        public void NextGeneration_ReturnsNewGrid()
        {
            // Arrange
            var oldGenGrid = new GridBuilder()
                            .WithRows(1)
                            .WithCols(1)
                            .WithActiveCell(1,1)
                            .Build();

            // Act
            var sut = _createTestSubject();
            var newGenGrid = sut.NextGeneration(oldGenGrid);

            // Assert
            Assert.AreNotSame(oldGenGrid, newGenGrid);            
        }

        [Test]
        public void NextGeneration_NewGridSameSizeAsOld()
        {
            // Arrange
            var rows = Faker.RandomNumber.Next(2,5);
            var cols = Faker.RandomNumber.Next(2,5);
            var oldGenGrid = new GridBuilder()
                            .WithRows(rows)
                            .WithCols(cols)                            
                            .Build();

            // Act
            var sut = _createTestSubject();
            var newGenGrid = sut.NextGeneration(oldGenGrid);

            // Assert
            Assert.AreEqual(rows, newGenGrid.Count);
            Assert.AreEqual(cols, newGenGrid.First().Count);
        }

        /// <summary>
        /// Any live cell with fewer than two live neighbours dies, as if by underpopulation.
        /// </summary>
        [Test]
        public void NextGeneration_Underpopulation_CellDies()
        {
            // Arrange
            var neighbourCounterSvc = new NeighborCounterServiceBuilder()
                                        .CountReturns(Faker.RandomNumber.Next(0,1))
                                        .Build();                                        
            var oldGenGrid = new GridBuilder()
                            .WithRows(1)
                            .WithCols(1)
                            .WithActiveCell(1,1)
                            .Build();

            // Act
            var sut = _createTestSubject(neighbourCounterSvc);
            var newGenGrid = sut.NextGeneration(oldGenGrid);

            // Assert: Died due to underpopulation
            Assert.IsFalse(newGenGrid[0][0].IsAlive); 
        }

       [Test]
        public void NextGeneration_With2Neighbours_CellLives()
        {
            // Arrange
            var neighbourCounterSvc = new NeighborCounterServiceBuilder()
                                        .CountReturns(2)
                                        .Build();                                        
            var oldGenGrid = new GridBuilder()
                            .WithRows(1)
                            .WithCols(1)
                            .WithActiveCell(1,1)
                            .Build();

            // Act
            var sut = _createTestSubject(neighbourCounterSvc);
            var newGenGrid = sut.NextGeneration(oldGenGrid);

            // Assert: 
            Assert.IsTrue(newGenGrid[0][0].IsAlive);
        }

       [Test]
        public void NextGeneration_With3Neighbours_CellLives()
        {
            // Arrange
            var neighbourCounterSvc = new NeighborCounterServiceBuilder()
                                        .CountReturns(3)
                                        .Build();                                        
            var oldGenGrid = new GridBuilder()
                            .WithRows(1)
                            .WithCols(1)
                            .WithActiveCell(1,1)
                            .Build();

            // Act
            var sut = _createTestSubject(neighbourCounterSvc);
            var newGenGrid = sut.NextGeneration(oldGenGrid);

            // Assert
            Assert.IsTrue(newGenGrid[0][0].IsAlive);
        }

        [Test]
        public void NextGeneration_Overpopulation_CellDies()
        {
            // Arrange
            var neighbourCounterSvc = new NeighborCounterServiceBuilder()
                                        .CountReturns(Faker.RandomNumber.Next(4,8))
                                        .Build();                                        
            var oldGenGrid = new GridBuilder()
                            .WithRows(1)
                            .WithCols(1)
                            .WithActiveCell(1,1)
                            .Build();

            // Act
            var sut = _createTestSubject(neighbourCounterSvc);
            var newGenGrid = sut.NextGeneration(oldGenGrid);

            // Assert: Died due to overpopulation
            Assert.IsFalse(newGenGrid[0][0].IsAlive); 
        }

        [Test]
        public void NextGeneration_Reproduction_CellRevived()
        {
            // Arrange
            var neighbourCounterSvc = new NeighborCounterServiceBuilder()
                                        .CountReturns(3) // exactly three live neighbours 
                                        .Build();                                        
            var oldGenGrid = new GridBuilder()
                            .WithRows(1)
                            .WithCols(1)                            
                            .Build();

            // Act
            var sut = _createTestSubject(neighbourCounterSvc);
            var newGenGrid = sut.NextGeneration(oldGenGrid);

            // Assert: Cell revived due to reproduction
            Assert.IsTrue(newGenGrid[0][0].IsAlive); 
        }

        #region Private Methods
        private IGenerationService _createTestSubject(INeighborCounterService neighbourCounterSvc = null)
        {
            neighbourCounterSvc = neighbourCounterSvc ?? new NeighborCounterServiceBuilder().Build();

            return new GenerationService(neighbourCounterSvc);
        }
        #endregion
    }
}