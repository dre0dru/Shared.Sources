using System.Collections.Generic;

namespace Shared.Sources.ScriptableDatabase
{
    public interface IScriptableDatabase<TKey, TValue>
    {
        IEnumerable<TKey> Keys { get; }
        TValue Get(TKey key);
        bool TryGet(TKey key, out TValue value);
        bool ContainsKey(TKey key);
    }
}
