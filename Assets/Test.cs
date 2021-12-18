using System;
using Shared.Sources.Collections;
using Shared.Sources.CustomDrawers;
using UnityEngine;

public static class C
{
    public const string S = "S";
    
    public static class Inner
    {
        public const string I = "I";
        public const string U = "U";

        public static class Inside
        {
            public const string T = "T";

        }
    }
}

[Serializable]
public struct SelectableKvp : IKvp<string, int>
{
    [ConstantString(typeof(C))]
    [SerializeField]
    private string _key;

    [SerializeField]
    private int _value;

    public string Key
    {
        get => _key;
        set => _key = value;
    }

    public int Value
    {
        get => _value;
        set => _value = value;
    }
}

public class Test : MonoBehaviour
{
    [SerializeField]
    private UDictionary<int, GameObject> _simpleDict;

    [ConstantString(typeof(C.Inner.Inside))]
    [SerializeField]
    private string _plainString;
    
    [SerializeField]
    private SelectableKvp _selectable;
    
    [SerializeField]
    private UDictionary<string, int, SelectableKvp> _someDictionary;
    
    [SerializeField]
    private UDictionary<string, int> _testDictionary;

    [SerializeField]
    private UDictionarySo<string, int> _ref;
    
    [SerializeField]
    private UDictionarySo<string, int> _refKeys;

    private void Awake()
    {
        Debug.Log($"{_someDictionary["asd"]}");
    }

    [ContextMenu("CreateTestDictionary")]
    public void CreateTestDictionary()
    {
        _testDictionary = new UDictionary<string, int>()
        {
            { "asd", 2 },
            { "dasg", 2151 }
        };
    }

    [ContextMenu("Add")]
    public void Add()
    {
        _someDictionary.Add("hfg", 5783);
        _someDictionary["asd"]= 97807;
    }
    
    [ContextMenu("Add Duplicate")]
    public void AddDuplicate()
    {
        _someDictionary.Add("asd", 67);
    }

    [ContextMenu("Remove")]
    public void Remove()
    {
        _someDictionary.Remove("asd");
        _someDictionary.Remove("hfg");
    }
}
