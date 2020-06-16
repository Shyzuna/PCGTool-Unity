
using PCGTool.Scripts.Nodes;
using XNodeEditor;

namespace PCGTool.Scripts.Editor.NodesEditor {
    [CustomNodeEditor(typeof(NoisePCGModelNode))]
    public class NoisePCGModelNodeEditor : PCGModelNodeEditor {
        public override void OnBodyGUI() {
            if (_pcgModelNode == null) {
                _pcgModelNode = (PCGModelNode) target;
            }
            
            // Specifying the update method
            onUpdateNode = NodeUpdate;
                
            serializedObject.Update();

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("allTileset"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("width"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("height"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("noiseNode"));
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}