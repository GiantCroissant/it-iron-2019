namespace ItIron2019.MultipleAssetGraph.InEditor
{
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using UnityEditor;
    using UnityEditorInternal;

    using UnityEngine;
    using UnityEngine.AssetGraph;

    public class BuildTask
    {
        [MenuItem("Assets/It Iron 2019/Execute Single Graph")]
        public static void ExecuteSingle()
        {
            var graphName = $"AssetGraph - 1.asset";
            var graphPath = Path.Combine("Assets", "_", "Graphs", graphName);
            AssetGraphUtility.ExecuteGraph(graphPath);
        }

        [MenuItem("Assets/It Iron 2019/Execute Multiple Graph")]
        public static void ExecuteMultiple()
        {
//            var graphName1 = $"AssetGraph - 1.asset";
//            var graphPath1 = Path.Combine("Assets", "_", "Graphs", graphName1);
//            var graphName2 = $"AssetGraph - 2.asset";
//            var graphPath2 = Path.Combine("Assets", "_", "Graphs", graphName2);
//
//            var graphGuids = new List<string>
//            {
//                AssetDatabase.AssetPathToGUID(graphPath1),
//                AssetDatabase.AssetPathToGUID(graphPath2)
//            };
//            
            var graphDirectory = Path.Combine(Application.dataPath, "_", "Graphs");
            var filePaths = Directory.GetFiles(graphDirectory);

            var fileNames =
                filePaths.Select(fp =>
                {
                    var fileInfo = new FileInfo(fp);

                    var count = fileInfo.DirectoryName.Length;

                    // Add one more for slash char
                    return fp.Remove(0, count + 1);
                });

            var graphGuids =
                fileNames
                    .Select(fn =>
                    {
                        var graphPath = Path.Combine("Assets", "_", "Graphs", fn);
                        var guid = AssetDatabase.AssetPathToGUID(graphPath);
                        return guid;
                    })
                    .ToList();

            // Graph that has smaller sort order will run first 
            AssetGraphUtility.ExecuteAllGraphs(graphGuids, true);
        }

        [MenuItem("Assets/It Iron 2019/Create Graph Config")]
        public static void Create()
        {
            var bbc = BatchBuildConfig.GetConfig();
            var fileName = "Batch Build Config.asset";
            var filePath = Path.Combine("Assets", "_", "Graph Configs", fileName);

            var graphCollection = BatchBuildConfig.GraphCollection.CreateNewGraphCollection("2 jobs graph");
            bbc.GraphCollections.Add(graphCollection);
            
            var graphName1 = $"AssetGraph - 1.asset";
            var graphPath1 = Path.Combine("Assets", "_", "Graphs", graphName1);
            var graphName2 = $"AssetGraph - 2.asset";
            var graphPath2 = Path.Combine("Assets", "_", "Graphs", graphName2);
            
            graphCollection.AddGraph(AssetDatabase.AssetPathToGUID(graphPath1));
            graphCollection.AddGraph(AssetDatabase.AssetPathToGUID(graphPath2));
            
            // It already creates at Assets/AssetGraph/AssetBundles/SavedSettings/BatchBuildConfig/BatchBuildConfig.asset
//            AssetDatabase.CreateAsset(bbc, filePath);
//            AssetDatabase.SaveAssets();
        }
        
        [MenuItem("Assets/It Iron 2019/Execute Graph Collection")]
        public static void ExecuteGraphCollection()
        {
            // After named collection in build config, just use the name directly to save hassle
            var collectionName = "2 jobs graph";
            AssetGraphUtility.ExecuteGraphCollection(collectionName);
        }
    }
}
