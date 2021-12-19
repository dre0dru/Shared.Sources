using System;
using System.Collections.Generic;
using Shared.Sources.CustomDrawers;
using UnityEngine;

namespace Simplification
{
    [Serializable]
    public struct TestStruct
    {
        [ConstantString(typeof(C))]
        [SerializeField]
        private List<string> _list;
    }
    
    public class SimpleTest : MonoBehaviour
    {
        [ConstantString(typeof(C))]
        [SerializeField]
        private List<string> _list;

        [SerializeField]
        private TestStruct _testStruct;

        [SerializeField]
        private List<KvpNew<string, int>> _list2;
        
        [SerializeField]
        private Dict<string, int> _dict;
    }
}
