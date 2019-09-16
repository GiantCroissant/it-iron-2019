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
    [CustomAssetGenerator("Create Monster Data", "v0.1", 1)]
    public class CreateMonsterData : UnityEngine.AssetGraph.IAssetGenerator
    {
        public void OnValidate()
        {
        }

        public string GetAssetExtension(AssetReference asset)
        {
            return ".asset";
        }

        public Type GetAssetType(AssetReference asset)
        {
            return typeof(Runtime.MonsterData);
        }

        public bool CanGenerateAsset(AssetReference asset)
        {
            return true;
        }

        public bool GenerateAsset(AssetReference asset, string generateAssetPath)
        {
            // Pass in generateAssetPath as to be as-is

            var generated = false;

            // Treat it as individual rather than collective
            var jsonFile = asset.absolutePath;
            var jsonText = File.ReadAllText(jsonFile);

            Debug.Log($"GenerateAsset - jsonFilePath: {jsonFile}");
            var monster = CodeGen.Monster.FromJson(jsonText);

            //
            var monsterData = ConvertToMonsterData(monster);
            
            var directory = Path.GetDirectoryName(generateAssetPath);
            
            var assetName = $"{monsterData.id}";
            // For now just remove monster- prefix to suit the required out generateAssetPath
            assetName = assetName.Remove(0, 8);
            var fullAssetPath = Path.Combine(directory, $"{assetName}.asset");
            
            AssetDatabase.CreateAsset(monsterData, fullAssetPath);
            
            Debug.Log($"GenerateAsset - generateAssetPath: {generateAssetPath}");
            
            generated = true;
            
            return generated;
        }

        private Runtime.MonsterData ConvertToMonsterData(CodeGen.Monster monster)
        {
            var so = ScriptableObject.CreateInstance<Runtime.MonsterData>();
            so.id = monster.Id;
            so.title = monster.Title;
            so.hp = (int) monster.Hp;
            so.mp = (int) monster.Mp;
            so.attack = (int) monster.Attack;

            return so;
        }

        public void OnInspectorGUI(Action onValueChanged)
        {
        }
    }
}
