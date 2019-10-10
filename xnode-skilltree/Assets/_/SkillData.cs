namespace ItIron2019.XNodeSkillTree.GameUse
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class SkillItem
    {
        public string id;
        public List<string> dependOnIds;
    }
    
    public class SkillData : ScriptableObject
    {
        public List<SkillItem> skillItems;
    }
}
