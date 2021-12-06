using System.Collections.Generic;
using UnityEngine;

namespace Shared.Sources.Collections
{
    public static class CollectionsExtensions
    {
        public static IList<T> Shuffle<T>(this IList<T> collection)
        {  
            int n = collection.Count;  
            while (n > 1) {  
                n--;  
                int k = Random.Range(0, n + 1);  
                (collection[k], collection[n]) = (collection[n], collection[k]);
            }

            return collection;
        }
    }
}