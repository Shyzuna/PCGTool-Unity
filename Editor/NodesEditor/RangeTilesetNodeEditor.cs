using System.Collections.Generic;
using PCGTool.Scripts.Nodes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using XNode;
using XNodeEditor;

namespace PCGTool.Scripts.Editor.NodesEditor {
    [CustomNodeEditor(typeof(RangeTilesetNode))]
    public class RangeTilesetNodeEditor : TilesetNodeEditor {
        private RangeTilesetNode _rangeTilesetNode;

        private List<int> _toRemove;
        private List<string> _toAdd;

        public override void OnCreate() {
            if (_rangeTilesetNode == null) {
                _rangeTilesetNode = (RangeTilesetNode) target;
            }
            
            _toRemove = new List<int>();
            _toAdd = new List<string>();
        }

        public override void OnBodyGUI() {
            if (_rangeTilesetNode == null) {
                _rangeTilesetNode = (RangeTilesetNode) target;
            }

            bool dirty = false;
            // Remove PortNode To remove
            foreach (int del in _toRemove) {
                _rangeTilesetNode.RemoveDynamicPort(_rangeTilesetNode.tileFields[del]);
                _rangeTilesetNode.tileFields.RemoveAt(del);
                _rangeTilesetNode.minValues.RemoveAt(del);
                _rangeTilesetNode.maxValues.RemoveAt(del);
                dirty = true;
            }

            foreach (string inputName in _toAdd) {
                AddTileInput(inputName);
                dirty = true;
            }

            if (dirty) {
                EditorUtility.SetDirty(_rangeTilesetNode);
                _toRemove.Clear();
                _toAdd.Clear();
            }
            
            // Specifying the update method
            onUpdateNode = NodeUpdate;
            
            serializedObject.Update();

            // Display PortNode Block
            int i = 0;
            foreach (NodePort np in _rangeTilesetNode.tileFields) {
                NodeEditorGUILayout.PortField(np);
                if (GUILayout.Button("-", GUILayout.Width(50))) {
                    PlanRemoveTileInput(i);
                }
                EditorGUILayout.BeginVertical();
                _rangeTilesetNode.minValues[i] = EditorGUILayout.TextField("Min", _rangeTilesetNode.minValues[i]);
                _rangeTilesetNode.maxValues[i] = EditorGUILayout.TextField("Max", _rangeTilesetNode.maxValues[i]);
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
            if (_rangeTilesetNode == null) {
                _rangeTilesetNode = (RangeTilesetNode) target;
            }
            _rangeTilesetNode.tileFields.Add(_rangeTilesetNode.AddDynamicInput(typeof(Tile), Node.ConnectionType.Override, Node.TypeConstraint.Strict,
                name));
            _rangeTilesetNode.minValues.Add("0");
            _rangeTilesetNode.maxValues.Add("0");
        }

        public void PlanRemoveTileInput(int id) {
            _toRemove.Add(id);
        }

        public void PlanAddTileInput() {
            string name = $"tile{_rangeTilesetNode.lastTileIndex}";
            _toAdd.Add(name);
            _rangeTilesetNode.lastTileIndex++;
        }

        public void NodeUpdate(Node n) {
            // Called when modification occurs
        }
    }
}