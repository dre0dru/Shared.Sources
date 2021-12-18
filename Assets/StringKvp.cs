using System;
using Shared.Sources.Collections;
using UnityEngine;

//TODO try default drawer, but also add another attribute that can be placed on a field that contains kvp
//Search for source type there
[Serializable]
public class StringKvp<TValue> : IKvp<string, TValue>
{
    [SerializeField]
    private string _key;

    [SerializeField]
    private TValue _value;

    public string Key
    {
        get => _key;
        set => _key = value;
    }

    public TValue Value
    {
        get => _value;
        set => _value = value;
    }
}