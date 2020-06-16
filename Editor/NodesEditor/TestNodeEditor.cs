using System.Collections.Generic;
using PCGTool.Scripts.Nodes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using XNode;
using XNodeEditor;

// And its editor who helped me a lot to find out the problem with link display ^^'

namespace PCGTool.Scripts.Editor.NodesEditor {
    [CustomNodeEditor(typeof(TestNode))]
    public class TestNodeEditor : NodeEditor {

        private TestNode _testNode;
        private string[] nameTmp = new[] {"a", "b", "c", "d", "e", "f"};
        public override void OnCreate() {
            _testNode = (TestNode) target;
            //_testNode.nodeList = new List<NodePort>();
            /*_testNode.nodeList.Add(_testNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Inherited, "test1"));
            _testNode.nodeList.Add(_testNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Inherited, "test2"));
            _testNode.nodeList.Add(_testNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Inherited, "test3"));*
            _testNode.nodeList.Add(_testNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Inherited, "tutu"));
            _testNode.index++;
            _testNode.nodeList.Add(_testNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Inherited, "tutu" + (char)_testNode.index));
            _testNode.index++;
            _testNode.nodeList.Add(_testNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Inherited, "tutu" + (char)_testNode.index));
            _testNode.index++;*/
            //_testNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Inherited, nameTmp[_testNode.index]);
            //_testNode.index++;
            //_testNode.nodeList.Add(_testNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Inherited, "TUTU"));
            //_testNode.nodeList.Add(_testNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Inherited));
            Debug.Log(_testNode.nodeList.Count);
        }

        public override void OnBodyGUI() {
            if (GUILayout.Button("+")) {
                AddPortNode();
            }

            serializedObject.Update();
            
            foreach (NodePort np in _testNode.DynamicPorts) {
                NodeEditorGUILayout.PortField(np);
            }
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("value"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("test"));
            serializedObject.ApplyModifiedProperties();
        }

        public void AddPortNode() {
            _testNode.index++;
            _testNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Inherited, "test"+_testNode.index);
            EditorUtility.SetDirty(_testNode);
        }
    }
}