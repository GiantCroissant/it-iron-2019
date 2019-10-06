namespace ItIron2019.OdinInsepctorNormal
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    using Sirenix.OdinInspector.Editor.Drawers;

    using Sirenix.OdinInspector;

    public interface INpcEventSender
    {
        event System.EventHandler<(string, int)> NpcHpAdded;
        event System.EventHandler<int> AllNpcHpAdded;
        event System.EventHandler ClearAllNpc;
    }

    [System.Serializable]
    public class Npc
    {
        public string id;
        public string title;
        [BoxGroup("Dynamic Range")]
        [ProgressBar("Min", "Max", 0.3f, 0.9f, 0.3f)]
        public int hp;

        //
        [HideInInspector]
        [BoxGroup("Dynamic Range")]
        public int Min;

        [HideInInspector]
        [BoxGroup("Dynamic Range")]
        public int Max = 100;
    }

    
    public class EventSender :
        MonoBehaviour,
        INpcEventSender
    {
        [ProgressBar(0, 10, ColorMember = "GetCountColor", BackgroundColorMember = "GetCountBackgroundColor", Segmented = true)]
        public int SegmentedColoredBar = 5;
        private Color GetCountColor()
        {
            return
                this.SegmentedColoredBar > 3 ?
                    Color.white : Color.red;
        }
        
        private Color GetCountBackgroundColor()
        {
            return
                this.SegmentedColoredBar > 3 ? 
                    Color.blue :
                    new Color(0.16f, 0.16f, 0.16f, 1f);
        }
        
        public List<Npc> npcs;

        public event System.EventHandler<(string, int)> NpcHpAdded;
        public event System.EventHandler<int> AllNpcHpAdded;
        public event System.EventHandler ClearAllNpc;
        
        [Button]
        public void AddHpToSpecificNpc(string id, int hp)
        {
            NpcHpAdded.Invoke(this, (id, hp));
        }

        [Button(ButtonSizes.Small, ButtonStyle.FoldoutButton)]
        public void AddHpToAllNpc(int hp)
        {
            AllNpcHpAdded.Invoke(this, hp);
        }

        [InfoBox(
            "This button will clear all npc, use with caution", InfoMessageType.Warning)]
        [GUIColor(1, 0.4f, 0.4f)]
        [Button(ButtonSizes.Large, ButtonStyle.Box, Expanded = true)]
        public void DeleteAllNpc()
        {
            ClearAllNpc.Invoke(this, System.EventArgs.Empty);
        }
    }
}
