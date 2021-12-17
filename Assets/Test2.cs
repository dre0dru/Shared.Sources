using System;
using Shared.Sources.Collections;
using UnityEngine;

[ConstantStringKvp(typeof(C.Inner))]
[Serializable]
public class CCSKvp : StringKvp<GameObject>
{
    
}

public class Test2 : MonoBehaviour
{
    // [SerializeField]
    // private CCSKvp _asd;
    //
    [ConstantStringKvp(typeof(C))]
    [SerializeField]
    private StringKvp<int> _someStringKvp;
        
        
    [SerializeField]
    private UDictionary<string, GameObject, CCSKvp> _anotherDict;
        
    [SerializeField]
    private UDictionary<string, GameObject, StringKvp<GameObject>> _yetAnother;
}