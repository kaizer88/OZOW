using NUnit.Framework;
using Ozow.Sorting.Domain.DomainServices;
using Ozow.Sorting.Domain.DomainServices.SortAlgo;

namespace Ozow.Sorting.Tests.IntegrationTests
{
    public class AcceptanceIntegrationTests
    {
        [Test]
        public void WithGenericBuiltInSort_SortsResult()
        {
            // Arrange
            var inputString = "Contrary to popular belief, the pink unicorn flies east.";

            // Act
            var sut = _createTestSubject(sortAlgo: new GenericBuiltInSort());
            var output = sut.Sort(inputString);

            // Assert
            var expected = "aaabcceeeeeffhiiiiklllnnnnooooppprrrrssttttuuy";
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void WithQuickSort_SortsResult()
        {
            // Arrange
            var inputString = "Contrary to popular belief, the pink unicorn flies east.";

            // Act
            var sut = _createTestSubject(sortAlgo: new QuickSort());
            var output = sut.Sort(inputString);

            // Assert
            var expected = "aaabcceeeeeffhiiiiklllnnnnooooppprrrrssttttuuy";
            Assert.AreEqual(expected, output);
        }

        #region Private Methods
        private ISortDomainService _createTestSubject(ISortAlgo sortAlgo)
        {
            return new SortDomainService(sortAlgo);
        }
        #endregion
    }
}