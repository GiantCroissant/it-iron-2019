namespace ItIron2019.CustomAssetGraph.InEditor.NormalCode
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using UnityEditor;
    using UnityEditorInternal;

    using UnityEngine;

    public class MonsterAssetCretion
    {
        [MenuItem("Assets/It Iron 2019/Create Monster Asset")]
        public static void Create()
        {
            //
            var sourceDirectory = Path.Combine(Application.dataPath, "_", "Design Assets", "Monsters");
            var jsonFiles = Directory.EnumerateFiles(sourceDirectory, "*.json");

            var monsters = jsonFiles
                .Select(jsonFile =>
                {
                    var jsonText = File.ReadAllText(jsonFile);

                    var monster = CodeGen.Monster.FromJson(jsonText);

                    return monster;
                });

            //
            var monsterDatas =
                monsters.Select(monster =>
                {
                    var so = ScriptableObject.CreateInstance<Runtime.MonsterData>();
                    so.id = monster.Id;
                    so.title = monster.Title;
                    so.hp = (int) monster.Hp;
                    so.mp = (int) monster.Mp;
                    so.attack = (int) monster.Attack;

                    return so;
                });

            var exportedMonsterPath = Path.Combine(Application.dataPath, "_", "Exported", "Monsters");
            var exportedMonsterPathExisted = Directory.Exists(exportedMonsterPath);
            if (!exportedMonsterPathExisted)
            {
                Directory.CreateDirectory(exportedMonsterPath);
            }
            
            monsterDatas.ToList().ForEach(asset =>
            {
                // Absolute path
                var individualMonsterPath = Path.Combine(exportedMonsterPath, $"{asset.id}");
                var individualMonsterPathExisted = Directory.Exists(individualMonsterPath);
                if (!individualMonsterPathExisted)
                {
                    Directory.CreateDirectory(individualMonsterPath);
                }
                
                // Relative path
                var assetOutPath =
                    Path.Combine("Assets", "_", "Exported", "Monsters", $"{asset.id}", $"{asset.id}.asset");
                AssetDatabase.CreateAsset(asset, assetOutPath);
                AssetDatabase.SaveAssets();
            });
        }
    }
}
