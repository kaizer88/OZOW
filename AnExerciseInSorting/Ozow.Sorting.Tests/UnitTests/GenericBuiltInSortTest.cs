using NUnit.Framework;
using Ozow.Sorting.Domain.DomainServices.SortAlgo;

namespace Ozow.Sorting.Tests.UnitTests
{
    public class GenericBuiltInSortTest
    {
        [Test]
        public void Sort_InputUnsorted_OutputSorted()
        {
            // Arrange
            var input = "dcab";            

            // Act
            var sut = new GenericBuiltInSort();
            var output = sut.Sort(input);

            // Assert
            Assert.AreEqual("abcd", output);
        }
    }
}