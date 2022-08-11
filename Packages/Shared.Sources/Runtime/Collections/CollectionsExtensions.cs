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

        //TODO code duplication: https://stackoverflow.com/questions/12838122/ilistt-and-ireadonlylistt
        public static bool TryMoveNextCircular<T>(this IList<T> list, ref int index, out T next)
        {
            if (list.Count == 0)
            {
                next = default;
                return false;
            }
            
            index = (index + 1) % list.Count;

            next = list[index];
            return true;
        }

        public static bool TryMovePrevCircular<T>(this IList<T> list, ref int index, out T next)
        {
            if (list.Count == 0)
            {
                next = default;
                return false;
            }

            index -= 1;
            if (index < 0)
            {
                index = list.Count - 1;
            }
            
            index %= list.Count;
            
            next = list[index];
            return true;
        }

        public static bool TryMoveNextCircular<T>(this IReadOnlyList<T> list, ref int index, out T next)
        {
            if (list.Count == 0)
            {
                next = default;
                return false;
            }
            
            index = (index + 1) % list.Count;

            next = list[index];
            return true;
        }

        public static bool TryMovePrevCircular<T>(this IReadOnlyList<T> list, ref int index, out T next)
        {
            if (list.Count == 0)
            {
                next = default;
                return false;
            }

            index -= 1;
            if (index < 0)
            {
                index = list.Count - 1;
            }
            
            index %= list.Count;
            
            next = list[index];
            return true;
        }
    }
}