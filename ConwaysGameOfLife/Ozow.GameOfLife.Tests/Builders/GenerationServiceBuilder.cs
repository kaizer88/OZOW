using System;
using System.Collections.Generic;
using NSubstitute;
using Ozow.GameOfLife.Domain.DomainModel;
using Ozow.GameOfLife.Domain.DomainServices;

namespace Ozow.GameOfLife.Tests.Builders
{
    public class GenerationServiceBuilder
    {
        private IList<IList<ICell>> _nextGenerationMethodReturns = null;

        public IGenerationService Build()
        {
            var genService = Substitute.For<IGenerationService>();

            _nextGenerationMethodReturns  = _nextGenerationMethodReturns ?? new GridBuilder().Build();
            genService.NextGeneration(Arg.Any<IList<IList<ICell>>>())
                .Returns(_nextGenerationMethodReturns);

            return genService;
        }

        internal GenerationServiceBuilder NextGeneration_Returns(IList<IList<ICell>> grid)
        {
            _nextGenerationMethodReturns = grid;
            return this;
        }
    }
}