namespace ItIron2019.CustomAssetGraph.InEditor.CustomAssetGraph
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using UnityEditor;
    using UnityEngine;
    using UnityEngine.AssetGraph;

    [System.Serializable]
    [CustomPrefabBuilder("Create Monster Prefab", "v0.1", 1)]
    public class CreateMonsterPrefab : UnityEngine.AssetGraph.IPrefabBuilder
    {
        public void OnValidate()
        {
        }

        public bool CanCreatePrefab(string groupKey, List<UnityEngine.Object> objects, ref PrefabCreateDescription description)
        {
            var tex = objects.Find(o => o.GetType() == typeof(UnityEngine.Sprite));

            if (tex != null)
            {
                description.prefabName = $"{groupKey}";
            }
            
            return tex != null;
        }

        public GameObject CreatePrefab(string groupKey, List<UnityEngine.Object> objects, GameObject previous)
        {
//            Debug.Log($"groupKey: {groupKey}");
            
//            var monsterGO = new GameObject($"monster-{groupKey}");
            var monsterGO = new GameObject($"{groupKey}");

            var sr = monsterGO.AddComponent<SpriteRenderer>();

            var tex = (Sprite)objects.Find(o => o.GetType() == typeof(Sprite));

            sr.sprite = tex;

            return monsterGO;
        }

        public void OnInspectorGUI(Action onValueChanged)
        {
        }
    }
}
