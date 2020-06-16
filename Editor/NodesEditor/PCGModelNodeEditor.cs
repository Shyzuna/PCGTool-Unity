using PCGTool.Scripts.Nodes;
using UnityEditor;
using XNode;
using XNodeEditor;
using UnityEngine;

namespace PCGTool.Scripts.Editor.NodesEditor {
    [CustomNodeEditor(typeof(PCGModelNode))]
    public class PCGModelNodeEditor : NodeEditor {
        protected PCGModelNode _pcgModelNode;

        private GUIStyle _mainModelStyle;

        public override void OnCreate() {
            // Create custom style for main model
            _mainModelStyle = new GUIStyle();
            _mainModelStyle.alignment = TextAnchor.MiddleCenter;
            _mainModelStyle.fontStyle = FontStyle.Bold;
            _mainModelStyle.normal.textColor = Color.blue;
        }

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
            
            serializedObject.ApplyModifiedProperties();

        }
        
        public override void OnHeaderGUI() {
            // Change header title color if main model
            if (_pcgModelNode == null) {
                _pcgModelNode = (PCGModelNode) target;
            }
            GUIStyle current = _pcgModelNode.isMain ? _mainModelStyle : NodeEditorResources.styles.nodeHeader;
            GUILayout.Label(target.name, current, GUILayout.Height(30));
        }

        // Modify node color
        public override Color GetTint() {
            return Color.cyan;
        }

        public void NodeUpdate(Node n) {
            // Called when modification occurs
        }
        
        public override void AddContextMenuItems(GenericMenu menu) {
            // Actions if only one node is selected
            if (Selection.objects.Length == 1 && Selection.activeObject is XNode.Node) {
                XNode.Node node = Selection.activeObject as XNode.Node;
                menu.AddItem(new GUIContent("Move To Top"), false, () => NodeEditorWindow.current.MoveNodeToTop(node));
                menu.AddItem(new GUIContent("Rename"), false, NodeEditorWindow.current.RenameSelectedNode);
                // Add Mark option for main model
                menu.AddItem(new GUIContent("Mark as main model"), false, () => ((PCGGraphEditor)NodeEditorWindow.current.graphEditor).MarkModelAsMain(_pcgModelNode));
            }

            // Remove Duplicate
            menu.AddItem(new GUIContent("Remove"), false, NodeEditorWindow.current.RemoveSelectedNodes);

            // Custom sctions if only one node is selected
            if (Selection.objects.Length == 1 && Selection.activeObject is XNode.Node) {
                XNode.Node node = Selection.activeObject as XNode.Node;
                NodeEditorWindow.AddCustomContextMenuItems(menu, node);
            }
        }
    }
}