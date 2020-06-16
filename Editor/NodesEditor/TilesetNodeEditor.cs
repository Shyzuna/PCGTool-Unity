using PCGTool.Scripts.Nodes;
using UnityEngine;
using XNodeEditor;

namespace PCGTool.Scripts.Editor.NodesEditor {
    [CustomNodeEditor(typeof(TilesetNode))]
    public class TilesetNodeEditor : NodeEditor {
        // Modify node color
        public override Color GetTint() {
            return Color.green;
        }
    }
}