namespace ItIron2019.CustomAssetGraph.InEditor.NormalCode
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using UnityEditor;
    using UnityEditorInternal;

    using UnityEngine;

    public class MonsterPrefabCretion
    {
        [MenuItem("Assets/It Iron 2019/Create Monster Prefab")]
        public static void Create()
        {
            var absolutePathToMonster = Path.Combine(Application.dataPath, "_", "Art Assets", "Monsters");
            var subMonsterDirectory = Directory.GetDirectories(absolutePathToMonster).ToList();

            var subMonsterDirectoryNames =
                subMonsterDirectory.Select(smd =>
                {
                    var strippedSubDirectory = smd.Remove(0, absolutePathToMonster.Length + 1);

                    Debug.Log(strippedSubDirectory);
                    return strippedSubDirectory;
                }).ToList();
                
//            Debug.Log($"{subMonsterDirectory.First()}");

            subMonsterDirectoryNames
                .ForEach(smdn =>
                {
                    var pathToMonster = Path.Combine("Assets", "_", "Art Assets", "Monsters", smdn);
                    var monsterFile = "stand.png";
                    var monsterPath = Path.Combine(pathToMonster, monsterFile);
                    
                    Debug.Log(monsterPath);
                    
                    var textureImporter = TextureImporter.GetAtPath(monsterPath) as TextureImporter;
                    if (textureImporter != null)
                    {
                        textureImporter.textureType = TextureImporterType.Sprite;
                        textureImporter.spriteImportMode = SpriteImportMode.Single;
                        textureImporter.mipmapEnabled = false;
                        textureImporter.filterMode = FilterMode.Point;
                        textureImporter.maxTextureSize = 64;
                    }

                    var monsterTexture = AssetDatabase.LoadAssetAtPath<Sprite>(monsterPath);
                    if (monsterTexture != null)
                    {
                        var monsterGO = new GameObject($"monster-{smdn}");
                        var spriteRenderer = monsterGO.AddComponent<SpriteRenderer>();
                        spriteRenderer.sprite = monsterTexture;
        
                        var monsterPrefabPath = Path.Combine("Assets", "_", "Exported", "Monsters", $"monster-{smdn}", $"monster-{smdn}.prefab");
        
                        monsterPrefabPath = AssetDatabase.GenerateUniqueAssetPath(monsterPrefabPath);
        
                        PrefabUtility.SaveAsPrefabAssetAndConnect(monsterGO, monsterPrefabPath, InteractionMode.UserAction);
//                        PrefabUtility.SaveAsPrefabAsset(monsterGO, monsterPrefabPath);
//                        AssetDatabase.Refresh();
                        GameObject.DestroyImmediate(monsterGO);
                    }
                });
        }
    }

//    public class CharacterCreation : AssetPostprocessor
//    {
//        [MenuItem("Assets/It Iron 2019/Create Character")]
//        public static void Create()
//        {
//            var pathToCharacter = Path.Combine("Assets", "_", "Art Assets", "Character");
//            var characterFile = "p1_spritesheet.png";
//            var characterPath = Path.Combine(pathToCharacter, characterFile);
//
//            //
//            var characterTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(characterPath);
//            var textureImporter = TextureImporter.GetAtPath(characterPath) as TextureImporter;
//            if (characterTexture == null)
//            {
//                Debug.Log("Can not load character texure2d");
//            }
//            if (textureImporter != null)
//            {
//                textureImporter.textureType = TextureImporterType.Sprite;
//                textureImporter.spriteImportMode = SpriteImportMode.Multiple;
//                textureImporter.mipmapEnabled = false;
//                textureImporter.filterMode = FilterMode.Point;
//                textureImporter.maxTextureSize = 512;
//
////                FurtherProcess(characterTexture, textureImporter);
//            }
//
////            var rect = new Rect(texture.width / 4, texture.height / 4, texture.width / 2, texture.height / 2);
//        }
//
//        private void OnPreprocessTexture()
//        {
//            Debug.Log("OnPreprocessTexture overwriting defaults");
//
//            var textureImporter = assetImporter as TextureImporter;
//            textureImporter.textureType = TextureImporterType.Sprite;
//            textureImporter.spriteImportMode = SpriteImportMode.Multiple;
//            textureImporter.mipmapEnabled = false;
//            textureImporter.filterMode = FilterMode.Point;
//            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
//            textureImporter.maxTextureSize = 512;
//        }
//
////        private static void FurtherProcess(Texture2D texture, AssetImporter assetImporter)
//        public void OnPostprocessTexture(Texture2D texture)
//        {
//            var minimumSpriteSize = 16;
//            var extrudeSize = 0;
//            var rects = InternalSpriteUtility.GenerateAutomaticSpriteRectangles(texture, minimumSpriteSize, extrudeSize);
//            if (rects.Length == 0)
//            {
//                Debug.Log("No rect is generated");
//            }
//            else
//            {
//                Debug.Log($"Rect {rects.Length} is generated");
//            }
//
//            var A_Sprite = rects.OrderBy(r =>
//            {
//                Debug.Log($"r.width {r.width} r.height {r.height}");
//
//                return r.width * r.height;
//            }).First().center;
//            int colCount = rects.Where(r => r.Contains(new Vector2(r.center.x, A_Sprite.y))).Count();
//            int rowCount = rects.Where(r => r.Contains(new Vector2(A_Sprite.x, r.center.y))).Count();
//            Vector2Int spriteSize = new Vector2Int(texture.width / colCount, texture.height / rowCount);
//
//            Debug.Log($"colCount {colCount}");
//            Debug.Log($"rowCount {rowCount}");
//
//            List<SpriteMetaData> metas = new List<SpriteMetaData>();
//
//            for (int r = 0; r < rowCount; ++r)
//            {
//                for (int c = 0; c < colCount; ++c)
//                {
//                    SpriteMetaData meta = new SpriteMetaData();
//                    meta.rect = new Rect(c * spriteSize.x, r * spriteSize.y, spriteSize.x, spriteSize.y);
//                    meta.name = string.Format("#{3} {0} ({1},{2})", Path.GetFileNameWithoutExtension(assetImporter.assetPath), c, r,r*colCount+c);
//                    metas.Add(meta);
//                }
//            }
//
//            TextureImporter textureImporter = (TextureImporter)assetImporter;
//            textureImporter.spritesheet = metas.ToArray();
//            AssetDatabase.Refresh();
//        }
//    }
}
