using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ozow.Sorting.Domain.DomainServices.SortAlgo
{
    public class QuickSort : ISortAlgo
    {
        public string Sort(string input)
        {             
            return _quicksort(input.ToArray().ToList())
                .Aggregate<char, StringBuilder>(new StringBuilder(64), (sb,c) => sb.Append(c))
                .ToString();
        }

        /// <summary>
        /// Quick sort implementation taken from: https://gist.github.com/lbsong/6833729
        /// </summary>
        public static List<T> _quicksort<T>(List<T> elements) where T: IComparable {
            if (elements.Count() < 2) 
                return elements;
                
            var pivot = new Random().Next(elements.Count());
            var val = elements[pivot];
            var lesser = new List<T>();
            var greater = new List<T>();
            for (int i = 0; i < elements.Count(); i++) {
                if (i != pivot) {
                    if (elements[i].CompareTo(val) < 0) {
                        lesser.Add(elements[i]);
                    }
                    else {
                        greater.Add(elements[i]);
                    }
                }
            }

            var merged = _quicksort(lesser);
            merged.Add(val);
            merged.AddRange(_quicksort(greater));
            return merged;
        }
    }
}