using System.Collections.Generic;
using PCGTool.Scripts.Nodes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using XNode;
using XNodeEditor;

namespace PCGTool.Scripts.Editor.NodesEditor {
    [CustomNodeEditor(typeof(DistributedTilesetNode))]
    public class DistributedTilesetNodeEditor : TilesetNodeEditor {
        private DistributedTilesetNode _distributedTilesetNode;

        private Dictionary<NodePort, int> _toRemove;
        private List<string> _toAdd;

        public override void OnCreate() {
            if (_distributedTilesetNode == null) {
                _distributedTilesetNode = (DistributedTilesetNode) target;
            }
            
            _toRemove = new Dictionary<NodePort, int>();
            _toAdd = new List<string>();
        }

        public override void OnBodyGUI() {
            if (_distributedTilesetNode == null) {
                _distributedTilesetNode = (DistributedTilesetNode) target;
            }

            bool dirty = false;
            // Remove PortNode To remove
            foreach (KeyValuePair<NodePort, int> del in _toRemove) {
                _distributedTilesetNode.RemoveDynamicPort(del.Key);
                _distributedTilesetNode.distribution.RemoveAt(del.Value);
                dirty = true;
            }

            foreach (string inputName in _toAdd) {
                AddTileInput(inputName);
                dirty = true;
            }

            if (dirty) {
                EditorUtility.SetDirty(_distributedTilesetNode);
                _toRemove.Clear();
                _toAdd.Clear();
            }
            
            // Specifying the update method
            onUpdateNode = NodeUpdate;
            
            serializedObject.Update();

            // Display PortNode Block
            int i = 0;
            foreach (NodePort np in _distributedTilesetNode.DynamicInputs) {
                NodeEditorGUILayout.PortField(np);
                if (GUILayout.Button("-", GUILayout.Width(50))) {
                    PlanRemoveTileInput(i, np);
                }
                EditorGUILayout.BeginVertical();
                _distributedTilesetNode.distribution[i] = EditorGUILayout.TextField("Distribution", _distributedTilesetNode.distribution[i]);
                EditorGUILayout.EndVertical();    
                i++;
            }
            
            // Display Add Block button
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (GUILayout.Button("+", GUILayout.Width(50))) {
                PlanAddTileInput();
            }
            EditorGUILayout.Space();
            EditorGUILayout.EndHorizontal();
            

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("outTileset"));

            serializedObject.ApplyModifiedProperties();
        }

        public void AddTileInput(string name) {
            if (_distributedTilesetNode == null) {
                _distributedTilesetNode = (DistributedTilesetNode) target;
            }
            _distributedTilesetNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Strict, name);
            _distributedTilesetNode.distribution.Add("0");
        }

        public void PlanRemoveTileInput(int id, NodePort np) {
            _toRemove[np] = id;
        }

        public void PlanAddTileInput() {
            string name = $"tile{_distributedTilesetNode.lastTileIndex}";
            _toAdd.Add(name);
            _distributedTilesetNode.lastTileIndex++;
        }

        public void NodeUpdate(Node n) {
            // Called when modification occurs
        }
    }
}