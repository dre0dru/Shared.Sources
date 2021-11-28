using System.Collections.Generic;

namespace Shared.Sources.ScriptableDatabase
{
    public class KeyToDatabaseMap<TKey, TValue> : Dictionary<TKey, IScriptableDatabase<TKey, TValue>>
    {
        public void FillFromDatabases(IEnumerable<IScriptableDatabase<TKey, TValue>> scriptableDatabases)
        {
            foreach (var scriptableDatabase in scriptableDatabases)
            {
                FillFromDatabase(scriptableDatabase);
            }
        }

        public void FillFromDatabase(IScriptableDatabase<TKey, TValue> scriptableDatabase)
        {
            foreach (var key in scriptableDatabase.Keys)
            {
                Add(key, scriptableDatabase);
            }
        }
    }
}
