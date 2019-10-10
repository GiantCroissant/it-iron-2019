namespace ItIron2019.XNodeSkillTree.EditorExtension
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    
    using UnityEditor;
    using UnityEngine;

    using XNodeEditor;

    [NodeGraphEditor.CustomNodeGraphEditorAttribute(typeof(Runtime.SkillNodeGraph))]
    public class SkillNodeGraphEditor : NodeGraphEditor
    {
        private readonly Dictionary<string, List<string>> _nodeRelationship = new Dictionary<string, List<string>>();
        
        public override void OnGUI()
        {
            base.OnGUI();

            if (GUILayout.Button("Process Graph"))
            {
                Debug.Log($"Process Graph");
                _nodeRelationship.Clear();

                HandleProcessGraph();
            }
        }

        private void HandleProcessGraph()
        {
            var sng = this.target as Runtime.SkillNodeGraph;
            if (sng == null)
            {
                return;
            }

            var nodes = sng.nodes;
            
            nodes.ForEach(node =>
            {
                var sn = node as Runtime.SkillNode;
                if (sn != null)
                {
                    List<string> ids = null;
                    var cached = _nodeRelationship.TryGetValue(sn.skillId, out ids);
                    if (!cached)
                    {
                        ids = new List<string>();
                        _nodeRelationship.Add(sn.skillId, ids);
                    }
                    
                    node.Outputs.ToList().ForEach(outP =>
                    {
                        if (outP.Connection != null && outP.Connection.node != null)
                        {
                            var outsn = outP.Connection.node as Runtime.SkillNode;
                            if (outsn != null)
                            {
                                List<string> linkedids = null;
                                var linkedCache = _nodeRelationship.TryGetValue(outsn.skillId, out linkedids);
                                if (!linkedCache)
                                {
                                    linkedids = new List<string>();
                                    _nodeRelationship.Add(outsn.skillId, linkedids);
                                }
                                linkedids.Add(sn.skillId);
                            }
                        }
                    });
                }
            });

            // Uncomment lines below to see the immediate output
//            var desc =
//                _nodeRelationship.Aggregate("", (acc, next) =>
//                {
//                    var key = next.Key;
//                    var ids = next.Value.Aggregate("", (idacc, idnext) => $"{idacc} {idnext}");
//
//                    return $"{acc}\n[{key}]: {ids}";
//                });
//            
//            Debug.Log(desc);

            // Here, the relationship of nodes are known
            StoreRelationship();
        }

        private void StoreRelationship()
        {
            var skillData = ScriptableObject.CreateInstance<GameUse.SkillData>();
            skillData.skillItems = new List<GameUse.SkillItem>();
            foreach (var nr in _nodeRelationship)
            {
                var id = nr.Key;
                var dependOnIds = nr.Value;

                skillData.skillItems.Add(new GameUse.SkillItem
                {
                    id = id,
                    dependOnIds = dependOnIds
                });
            }

            var assetPath = Path.Combine("Assets", "Skill Data.asset");
            AssetDatabase.CreateAsset(skillData, assetPath);
            AssetDatabase.Refresh();
        }
    }
}
