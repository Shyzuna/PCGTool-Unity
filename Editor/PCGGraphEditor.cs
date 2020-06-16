using System;
using System.Collections.Generic;
using PCGTool.Scripts.Nodes;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace PCGTool.Scripts.Editor {
    [CustomNodeGraphEditor(typeof(PCGGraph))]
    public class PCGGraphEditor : NodeGraphEditor {

        private Dictionary<string, Type[]> _availableNodes;
        private PCGGraph _currentGraph;

        // Override open function
        public override void OnOpen() {
            // Init available nodes
            _availableNodes = new Dictionary<string, Type[]>();

            _availableNodes["Model"] = NodeEditorWindow.GetDerivedTypes(typeof(PCGModelNode));
            _availableNodes["Tileset"] = NodeEditorWindow.GetDerivedTypes(typeof(TilesetNode));
            _availableNodes["Tile"] = NodeEditorWindow.GetDerivedTypes(typeof(TileNode));
            _availableNodes["Noise"] = NodeEditorWindow.GetDerivedTypes(typeof(NoiseNode));

            if (target != null) {
                _currentGraph = (PCGGraph) target;
            }

            window.titleContent = new GUIContent("PCGTool");
        }

        // Override ContextMenu
        public override void AddContextMenuItems(GenericMenu menu) {
            Vector2 pos = NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition);
            
            // Create menu on previously defined available nodes
            foreach (KeyValuePair<string, Type[]> item in _availableNodes) {
                foreach (Type t in item.Value) {
                    menu.AddItem(new GUIContent($"{item.Key}/{t.Name}"), false, () => {
                        CreateNode(t, pos);
                    });
                }
            }
            menu.AddSeparator("");
            menu.AddItem(new GUIContent("Preferences"), false, () => NodeEditorWindow.OpenPreferences());
            NodeEditorWindow.AddCustomContextMenuItems(menu, target);
            
        }

        public void MarkModelAsMain(PCGModelNode n) {
            if (_currentGraph != null) {
                if (_currentGraph.mainModel != null) {
                    _currentGraph.mainModel.isMain = false;
                }
                _currentGraph.mainModel = n;
                _currentGraph.mainModel.isMain = true;
            }
        }
    }
}