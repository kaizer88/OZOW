using System.Collections.Generic;
using Ozow.GameOfLife.Domain.DomainModel;

namespace Ozow.GameOfLife.Domain.DomainServices
{
    public interface IGameEventEmitter
    {
        void NewGameStateEvent(IList<IList<ICell>> state);
        void InitGameStateEvent(IList<IList<ICell>> state);
    }
}