using Ozow.GameOfLife.Domain.DomainModel;
using Ozow.GameOfLife.Domain.DomainServices;

namespace Ozow.GameOfLife.Domain.Factories
{
    public interface IGameFactory
    {
        IGameOfLife CreateGame(IGameEventEmitter eventEmitter, int gridRowCount, int gridColCount);
    }

    public class GameFactory : IGameFactory
    {
        #region Dependencies
        private IGenerationService _genService;
        #endregion

        #region Constructor
        public GameFactory(IGenerationService genService)
        {
            _genService = genService;            
        }
        #endregion

        public IGameOfLife CreateGame(IGameEventEmitter eventEmitter, int gridRowCount, int gridColCount)
        {
            return new DomainModel.GameOfLife(eventEmitter, _genService, gridRowCount, gridColCount);
        }
    }
}