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

        public static TDictionarySo CreateCloneForEditor<TKey, TValue, TDictionarySo>(this TDictionarySo dictionarySo)
            where TDictionarySo : DictionarySo<TKey, TValue>
        {
            #if UNITY_EDITOR
            return dictionarySo.CreateClone<TKey, TValue, TDictionarySo>();
            #else
            return dictionarySo;
            #endif
        }
        
        public static TDictionarySo CreateClone<TKey, TValue, TDictionarySo>(this TDictionarySo dictionarySo)
            where TDictionarySo : DictionarySo<TKey, TValue>
        {
            return Object.Instantiate(dictionarySo);
        }
    }
}