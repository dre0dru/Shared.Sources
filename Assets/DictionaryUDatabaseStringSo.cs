using System;
using Shared.Sources.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DictionaryUDatabaseStringSo", menuName = "Test/DictionaryUDatabaseStringSo")]
public class DictionaryUDatabaseStringSo : UDictionarySo<string, int>
{
    private void Awake()
    {
        var res = this.CreateClone<string, int, DictionaryUDatabaseStringSo>();
    }
}