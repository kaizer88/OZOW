using System;
using System.Collections.Generic;
using NSubstitute;
using Ozow.GameOfLife.Domain.DomainModel;
using Ozow.GameOfLife.Domain.DomainServices;

namespace Ozow.GameOfLife.Tests.Builders
{
    public class NeighborCounterServiceBuilder
    {
        private int _countMethodReturns = Faker.RandomNumber.Next(0,8);

        internal INeighborCounterService Build()
        {
            var stub = Substitute.For<INeighborCounterService>();

            stub.Count(Arg.Any<IList<IList<ICell>>>(), Arg.Any<int>(), Arg.Any<int>())
                .Returns(_countMethodReturns);

            return stub;
        }

        internal NeighborCounterServiceBuilder CountReturns(int returnVal)
        {
            _countMethodReturns = returnVal;
            return this;
        }
    }
}