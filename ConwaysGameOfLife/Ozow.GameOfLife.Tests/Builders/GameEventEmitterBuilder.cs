using System;
using NSubstitute;
using Ozow.GameOfLife.Domain.DomainServices;

namespace Ozow.GameOfLife.Tests.Builders
{
    public class GameEventEmitterBuilder
    {
        internal IGameEventEmitter Build()
        {
            var emitter = Substitute.For<IGameEventEmitter>();
            
            return emitter;
        }
    }
}