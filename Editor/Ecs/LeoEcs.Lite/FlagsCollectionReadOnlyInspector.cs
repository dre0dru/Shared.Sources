#if LEOECS_LITE

using System;
using Leopotam.EcsLite.UnityEditor;
using Shared.Sources.Stats;
using UnityEditor;

namespace Shared.Sources.Editor.Ecs.LeoEcs.Lite
{
    public class FlagsCollectionReadOnlyInspector<TKey, TFlagsCollection> : IEcsComponentInspector
        where TFlagsCollection : FlagsCollection<TKey>
    {
        public Type GetFieldType()
        {
            return typeof(TFlagsCollection);
        }

        public int GetPriority() => 0;

        public (bool, object) OnGui(string label, object value, EcsEntityDebugView entityView)
        {
            var indentLevel = EditorGUI.indentLevel;

            var flagsCollection = (TFlagsCollection)value;

            foreach (var flag in flagsCollection)
            {
                EditorGUILayout.LabelField(flag.Key.ToString());
                
                EditorGUI.indentLevel++;

                EditorGUILayout.LabelField(flag.Key.ToString(), flag.Value.Count.ToString());

                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel = indentLevel;

            return (false, value);
        }
    }
}

#endif
