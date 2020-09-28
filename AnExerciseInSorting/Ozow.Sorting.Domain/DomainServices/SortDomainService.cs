using System.Linq;
using System.Text;
using Ozow.Sorting.Domain.DomainServices.SortAlgo;

namespace Ozow.Sorting.Domain.DomainServices
{
    public interface ISortDomainService
    {
        string Sort(string inputString);
    }

    public class SortDomainService : ISortDomainService
    {
        #region Dependencies
        private readonly ISortAlgo _sortAlgo;
        #endregion

        #region Constructor
        public SortDomainService(ISortAlgo sortAlgo)
        {
            _sortAlgo = sortAlgo;            
        }
        #endregion

        public string Sort(string inputString)
        {
            var unsorted =  inputString                                
                .Where(c => !char.IsPunctuation(c)) // <-- Filter out punctuation
                .Where(c => c!= ' ') // <-- Filter out spaces
                .Select(char.ToLower) // <-- Map to lower case
                .Aggregate<char, StringBuilder>(new StringBuilder(64), (sb,c) => sb.Append(c)) // <-- Reduce char[] to String Builder
                .ToString(); // <-- Convert string builder to string
            
            // Use abstract sort algo
            return _sortAlgo.Sort(unsorted);
        }
    }
}