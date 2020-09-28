using System;
using System.Collections.Generic;
using Ozow.GameOfLife.Domain.DomainServices;

namespace Ozow.GameOfLife.Domain.DomainModel
{
    public interface IGameOfLife
    {
        int RowCount { get; }
        int ColCount { get; }        
        
        IList<IList<ICell>> GameState { get; }

        void StartGame();
        void NextGen();
    }

    public class GameOfLife : IGameOfLife
    {
        #region Dependencies
        private readonly IGameEventEmitter _eventEmitter;
        private readonly IGenerationService _genService;
        #endregion

        #region public Properties
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }                
        public IList<IList<ICell>> GameState { get; private set; }        
        #endregion

        #region Constructor
        public GameOfLife(IGameEventEmitter eventEmitter, IGenerationService genService, int rowCount, int columnCount)
        {
            _eventEmitter = eventEmitter;
            _genService = genService;

            this.RowCount = rowCount;
            this.ColCount = columnCount;
        }
        #endregion

        #region Public Methods
        public void StartGame()
        {
            this.GameState = _crateInitGameState();  
            _eventEmitter.InitGameStateEvent(this.GameState);
        }
        public void NextGen()
        {
            // Use current game state to get the next game state
            this.GameState = _genService.NextGeneration(this.GameState);
            _eventEmitter.NewGameStateEvent(this.GameState);
        }
        #endregion

        #region Private Methods
        private IList<IList<ICell>> _crateInitGameState()
        {
            var mailList  = new List<IList<ICell>>();

            // Crate Init grid with random cell alive/dead states
            for(var r = 0; r < this.RowCount; r++){
                var rowList = new List<ICell>();
                for(var c = 0; c < this.ColCount; c++)
                    rowList.Add(new Cell());
                mailList.Add(rowList);
            }

            return mailList;
        }
        #endregion
    }
}