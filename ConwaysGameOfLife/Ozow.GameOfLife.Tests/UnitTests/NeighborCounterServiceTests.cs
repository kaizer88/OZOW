using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ozow.GameOfLife.Domain.DomainServices;
using Ozow.GameOfLife.Tests.Builders;

namespace Ozow.GameOfLife.Tests.UnitTests
{
    public class NeighborCounterServiceTests
    {
        List<dynamic> _neighbours = new List<dynamic>(){
                new {row = 1, col = 1},
                new {row = 1, col = 2},
                new {row = 1, col = 3},
                new {row = 2, col = 1},
                new {row = 2, col = 3},
                new {row = 3, col = 1},
                new {row = 3, col = 2},
                new {row = 3, col = 3},
            };

        [Test]        
        public void Count_HasNoNeighbors_zero()
        {
            // Arrange: Create 3x3 grid with no cells alive
            var grid = new GridBuilder()
                            .WithRows(3)
                            .WithCols(3)                            
                            .Build();

            // Act
            var sut = _createSubjectUnderTest();
            var count = sut.Count(grid, 2, 2); // Evaluate the center cell for neighbours

            // Assert
            Assert.AreEqual(0, count);
            
        }

        [Test]        
        public void Count_OneNeighbourActive_Returns1()
        {
            // Arrange: Check all neighbours
            _neighbours.ForEach(n => {                
                var grid = new GridBuilder()
                                .WithRows(3)
                                .WithCols(3)
                                .WithActiveCell(row: n.row, col: n.col)
                                .Build();
                                
                // Act
                var sut = _createSubjectUnderTest();
                var count = sut.Count(grid, 2, 2); // Evaluate the center cell for neighbours

                // Assert
                Assert.AreEqual(1, count);
            });
        }

        [Test]        
        public void Count_MultiCellActive_Returns2()
        {
            // Arrange:
            var neighbour1 = _neighbours[Faker.RandomNumber.Next(0,_neighbours.Count -1)];
            var neighbour2 = _neighbours.Where(x => x != neighbour1).ToList()[Faker.RandomNumber.Next(0,_neighbours.Count -2)];
            var grid = new GridBuilder()
                            .WithRows(3)
                            .WithCols(3)
                            .WithActiveCell(row: neighbour1.row, col: neighbour1.col)
                            .WithActiveCell(row: neighbour2.row, col: neighbour2.col)
                            .Build();
                            
            // Act
            var sut = _createSubjectUnderTest();
            var count = sut.Count(grid, 2, 2); // Evaluate the center cell for neighbours

            // Assert
            Assert.AreEqual(2, count);
        }

        [Test]        
        public void Count_MultiCellActive_Returns3()
        {
            // Arrange:
            var neighbour1 = _neighbours[Faker.RandomNumber.Next(0,_neighbours.Count -1)];
            var neighbour2 = _neighbours.Where(x => x != neighbour1).ToList()[Faker.RandomNumber.Next(0,_neighbours.Count -2)];
            var neighbour3 = _neighbours
                .Where(x => x != neighbour1)
                .Where(x => x != neighbour2)
                .ToList()[Faker.RandomNumber.Next(0,_neighbours.Count -3)];

            var grid = new GridBuilder()
                            .WithRows(3)
                            .WithCols(3)
                            .WithActiveCell(row: neighbour1.row, col: neighbour1.col)
                            .WithActiveCell(row: neighbour2.row, col: neighbour2.col)
                            .WithActiveCell(row: neighbour3.row, col: neighbour3.col)
                            .Build();
                            
            // Act
            var sut = _createSubjectUnderTest();
            var count = sut.Count(grid, 2, 2); // Evaluate the center cell for neighbours

            // Assert
            Assert.AreEqual(3, count);
        }

        [Test]        
        public void Count_NoErrorWhenOnLeftBoundry()
        {
                var grid = new GridBuilder()
                                .WithRows(3)
                                .WithCols(3)                                
                                .Build();
                                
                // Act
                var sut = _createSubjectUnderTest();
                var count = sut.Count(grid, 2, 1); // Left boundry

                // Assert
                Assert.AreEqual(0, count);
        }

        [Test]        
        public void Count_NoErrorWhenAtTopBoundry()
        {
                var grid = new GridBuilder()
                                .WithRows(3)
                                .WithCols(3)                                
                                .Build();
                                
                // Act
                var sut = _createSubjectUnderTest();
                var count = sut.Count(grid, 1, 2); // Top boundry

                // Assert
                Assert.AreEqual(0, count);
        }

        [Test]        
        public void Count_NoErrorWhenOnRightBoundry()
        {
                var grid = new GridBuilder()
                                .WithRows(3)
                                .WithCols(3)                                
                                .Build();
                                
                // Act
                var sut = _createSubjectUnderTest();
                var count = sut.Count(grid, 2, 3); // Right boundry

                // Assert
                Assert.AreEqual(0, count);
        }

        [Test]        
        public void Count_NoErrorWhenAtBottomBoundry()
        {
                var grid = new GridBuilder()
                                .WithRows(3)
                                .WithCols(3)                                
                                .Build();
                                
                // Act
                var sut = _createSubjectUnderTest();
                var count = sut.Count(grid, 3, 2); // Bottom boundry

                // Assert
                Assert.AreEqual(0, count);
        }

        #region Private Methods
        private INeighborCounterService _createSubjectUnderTest()
        {
            return new NeighborCounterService();
        }
        #endregion
    }
}