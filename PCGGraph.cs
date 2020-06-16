using System;
using System.Collections;
using System.Collections.Generic;
using PCGTool.Scripts.Nodes;
using UnityEngine;
using XNode;


namespace PCGTool.Scripts {
    [CreateAssetMenu(menuName = "PCGTool/PCGGraph")]
    public class PCGGraph : NodeGraph {
        // Main model node
        public PCGModelNode mainModel;
    }
}