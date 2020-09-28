using System;
using NSubstitute;
using Ozow.Sorting.Domain.DomainServices.SortAlgo;


namespace Ozow.Sorting.Tests.Builders
{
    public class SortAlgoBuilder
    {
        internal ISortAlgo Build()
        {
            var dummyAlgo =  Substitute.For<ISortAlgo>();

            // Just return the string passed as input
            dummyAlgo.Sort(Arg.Any<string>()).Returns( x => x.ArgAt<string>(0) );

            return dummyAlgo;
        }
    }
}