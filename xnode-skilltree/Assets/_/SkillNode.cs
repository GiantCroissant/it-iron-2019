﻿namespace ItIron2019.XNodeSkillTree.Runtime
{
    using System.Collections;

    using System.Collections.Generic;
    using UnityEngine;
    using XNode;

    public class SkillNode : Node
    {
        //
        public string skillId;
        public string skillName;
        
        [Input] public float value;
        [Output] public float result1;
        [Output] public float result2;
    
        // Use this for initialization
        protected override void Init()
        {
            base.Init();
        }

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
    }
}

