using System;
using NSubstitute;
using NUnit.Framework;
using Ozow.Sorting.Domain.DomainServices;
using Ozow.Sorting.Domain.DomainServices.SortAlgo;
using Ozow.Sorting.Tests.Builders;

namespace Tests.UnitTests
{
    public class SortDomainServiceTests
    {
        [Test]
        public void Sort_InputWithPunctuation_PunctuationFilteredOut()
        {
            // Arrange
            var input = "a!b.c,d?";
            var sortAlgo = new SortAlgoBuilder().Build();

            // Act
            var sut = _createTestSubject(sortAlgo);
            var output = sut.Sort(input);

            // Assert
            Assert.AreEqual("abcd", output);
        }

        [Test]
        public void Sort_InputWithUpperCaseLetters_UpperCaseMappedToLower()
        {
            // Arrange
            var input = "ABCD";
            var sortAlgo = new SortAlgoBuilder().Build();

            // Act
            var sut = _createTestSubject(sortAlgo);
            var output = sut.Sort(input);

            // Assert
            Assert.AreEqual("abcd", output);            
        }

        [Test]
        public void Sort_CalledSortAlgo()
        {
            // Arrange
            var input = "test123";
            var sortAlgo = new SortAlgoBuilder().Build();

            // Act
            var sut = _createTestSubject(sortAlgo);
            sut.Sort(input);

            // Assert
            sortAlgo
                .Received(1)
                .Sort(input);
        }

        #region Private Methods
        private ISortDomainService _createTestSubject(ISortAlgo sortAlgo)
        {
            return new SortDomainService(sortAlgo);            
        }
        #endregion
    }
}