#if LEOECS_LITE

using System;
using Leopotam.EcsLite.UnityEditor;
using Shared.Sources.Stats;
using UnityEditor;

namespace Shared.Sources.Editor.Ecs.LeoEcs.Lite
{
    public abstract class StatsCollectionReadOnlyInspector<TKey, TValue, TStatsCollection> : IEcsComponentInspector
        where TStatsCollection : StatsCollection<TKey, TValue>
    {
        public Type GetFieldType()
        {
            return typeof(TStatsCollection);
        }

        public int GetPriority() => 0;

        public (bool, object) OnGui(string label, object value, EcsEntityDebugView entityView)
        {
            var indentLevel = EditorGUI.indentLevel;

            var statsCollection = (TStatsCollection)value;

            foreach (var stat in statsCollection)
            {
                EditorGUILayout.LabelField(stat.Key.ToString());
                
                EditorGUI.indentLevel++;

                foreach (var values in stat.Value)
                {
                    EditorGUILayout.LabelField(values.Key.ToString(), values.Value.ToString());
                }

                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel = indentLevel;

            return (false, value);
        }
    }
}

#endif
