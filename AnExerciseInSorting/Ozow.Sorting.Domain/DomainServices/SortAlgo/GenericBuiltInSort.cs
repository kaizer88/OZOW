using System.Linq;
using System.Text;

namespace Ozow.Sorting.Domain.DomainServices.SortAlgo
{
    public class GenericBuiltInSort : ISortAlgo
    {
        public string Sort(string input)
        {
            return input
                .OrderBy(c => c)
                .Aggregate<char, StringBuilder>(new StringBuilder(64), (sb,c) => sb.Append(c))
                .ToString();
        }
    }
}