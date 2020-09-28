using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Ozow.GameOfLife.Domain.DomainModel;
using Ozow.GameOfLife.Domain.DomainServices;
using Ozow.GameOfLife.Tests.Builders;

namespace Tests.UnitTests
{
    public class GameOfLifeTests
    {
        #region Constructor Tests
        [Test]
        public void Ctor_RowColCountCorrect()
        {
            // Arrange
            var rows = Faker.RandomNumber.Next(2, 10);
            var cols = Faker.RandomNumber.Next(2, 10);

            // Act
            var sut = _createSubjectUnderTest(rows, cols);

            // Assert
            Assert.AreEqual(rows, sut.RowCount);
            Assert.AreEqual(cols, sut.ColCount);
        }
        #endregion

        #region Method: StartGame()  Tests
        [Test]
        public void StartGame_CorrectNumberOfRows()
        {
            // Arrange
            var rows = Faker.RandomNumber.Next(2, 10);
            var cols = Faker.RandomNumber.Next(2, 10);

            // Act
            var sut = _createSubjectUnderTest(rows, cols);
            sut.StartGame();

            // Assert
            Assert.AreEqual(rows, sut.GameState.Count); 
        }

        [Test]
        public void StartGame_CorrectNumberOfCellsPerRow()
        {
            // Arrange
            var rows = Faker.RandomNumber.Next(2, 10);
            var cols = Faker.RandomNumber.Next(2, 10);

            // Act
            var sut = _createSubjectUnderTest(rows, cols);
            sut.StartGame();

            // Assert
            sut.GameState
                .ToList()
                .ForEach(r => Assert.AreEqual(cols, r.Count) );            
        }

        [Test]
        public void StartGame_EmitsInitialState()
        {
            // Arrange
            var rows = Faker.RandomNumber.Next(2, 10);
            var cols = Faker.RandomNumber.Next(2, 10);
            var eventEmitter = new GameEventEmitterBuilder().Build();

            // Act
            var sut = _createSubjectUnderTest(rows, cols, eventEmitter);
            sut.StartGame();

            // Assert
            eventEmitter
                .Received(1)
                .InitGameStateEvent(Arg.Any<IList<IList<ICell>>>());
        }
        #endregion

        #region  Method: NextGen() Test
        /// <summary>
        /// The grid returned from the GenerationService becomes the new Game state
        /// </summary>
        [Test]
        public void  NextGen_GridReturnedFromGenServiceBecomesNewState()
        {
            // Arrange
            var rows = Faker.RandomNumber.Next(2, 10);
            var cols = Faker.RandomNumber.Next(2, 10);    
            // Create a dummy grid to be returned by the Generation service
            var gridToReturn = new GridBuilder()
                .WithCols(cols)
                .WithRows(rows)
                .Build();

            // Creat a stub gen service, give it the grid to return
            var genService = new GenerationServiceBuilder()
                .NextGeneration_Returns(gridToReturn)
                .Build();        

            // Act
            var sut = _createSubjectUnderTest(rows, cols, genService: genService);
            sut.StartGame();
            sut.NextGen();

            // Assert
            Assert.AreSame(gridToReturn, sut.GameState);
        }

        /// <summary>
        /// The grid returned from the GenerationService becomes the new Game state
        /// </summary>
        [Test]
        public void  NextGen_EmitsEventWithNewState()
        {
            // Arrange
            var rows = Faker.RandomNumber.Next(2, 10);
            var cols = Faker.RandomNumber.Next(2, 10);    
            // Create a dummy grid to be returned by the Generation service
            var newGameState = new GridBuilder()
                .WithCols(cols)
                .WithRows(rows)
                .Build();

            // Creat a stub gen service, give it the grid to return
            var genService = new GenerationServiceBuilder()
                .NextGeneration_Returns(newGameState)
                .Build();        
            
            // Create Event Emitter
            var eventEmitter = new GameEventEmitterBuilder().Build();

            // Act
            var sut = _createSubjectUnderTest(rows, cols, genService: genService, eventEmitter: eventEmitter);
            sut.StartGame();
            sut.NextGen();

            // Assert
            eventEmitter
                .Received(1)
                .NewGameStateEvent(newGameState);
        }
        #endregion

        #region Private Methods
        private IGameOfLife _createSubjectUnderTest(int rows, int cols, IGameEventEmitter eventEmitter = null,
            IGenerationService genService = null)
        {
            eventEmitter = eventEmitter ?? new GameEventEmitterBuilder().Build();
            genService  = genService ?? new GenerationServiceBuilder().Build();
            return new GameOfLife(eventEmitter, genService, rowCount: rows, columnCount: cols);
        }
        #endregion

    }

}