using System;
using Shared.Sources.Collections;
using Shared.Sources.UDatabase;
using UnityEngine;

public class UDatabaseComposite<TKey, TValue> : DictionaryUDatabaseSo<TKey, UDatabaseSo<TKey, TValue>>
{
    [SerializeField]
    private UDictionary<TKey, UDatabaseComposite<TKey, TValue>> _nestedComposites;


    public UDatabaseSo<TKey, TValue> GetDatabase(TKey key, params TKey[] path)
    {
        throw new NotImplementedException();
    }
}

[CreateAssetMenu(fileName = "TEST", menuName = "MENUNAME/TEST")]
public class UDatabaseComposite : UDatabaseComposite<string, int>
{
}

