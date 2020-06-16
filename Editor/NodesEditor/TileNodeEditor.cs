using PCGTool.Scripts.Nodes;
using XNodeEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using XNode;

// Override Basic xnode node display

namespace PCGTool.Scripts.Editor.NodesEditor {
    [CustomNodeEditor(typeof(TileNode))]
    public class TileNodeEditor : NodeEditor {
        
        // /!\ Care here : Should use parent type if any other class inherit from TileNode
        private BasicTileNode _basicTileNode;
        
        private Texture2D _tileImg;

        private Tile _oldState;

        public override void OnCreate() {
            if (_basicTileNode == null) {
                _basicTileNode = (BasicTileNode) target;
            }
            if (_basicTileNode.tile != null) {
                LoadTexture(_basicTileNode.tile.sprite);
            }
        }

        // Modify display method
        public override void OnBodyGUI() {
            if (_basicTileNode == null) {
                _basicTileNode = (BasicTileNode) target;
            }
            
            // Specifying the update method
            onUpdateNode = NodeUpdate;
                
            serializedObject.Update();
            
            // Not needed can use TileNode field
            //SerializedProperty sp = serializedObject.FindProperty("tile");
            //Tile tile = (Tile) sp.objectReferenceValue;
            //sp.objectReferenceValue = EditorGUILayout.ObjectField("Tile", tile, typeof(Tile), false);
            
            // Specific unity object picker for tile
            _basicTileNode.tile = (Tile)EditorGUILayout.ObjectField("Tile",  _basicTileNode.tile, typeof(Tile), false, GUILayout.ExpandWidth(true));

            // Refresh image preview
            if (_basicTileNode.tile != null) {
                LoadTexture(_basicTileNode.tile.sprite);
            }

            if (_tileImg != null) {
                EditorGUILayout.LabelField(new GUIContent(_tileImg));
            }
            
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("tile"));
            
            serializedObject.ApplyModifiedProperties();

        }

        // Modify node color
        public override Color GetTint() {
            return Color.yellow;
        }

        public void NodeUpdate(Node n) {
            // Called when modification occurs
        }

        // Create preview texture of selected tile
        private void LoadTexture(Sprite sprite) {
            Texture2D t = sprite.texture;
            Rect r = sprite.rect;
            Color[] subPixels = t.GetPixels((int) r.x, (int) r.y, (int) r.width, (int) r.height);
            _tileImg = new Texture2D((int) r.width, (int) r.height, t.format, false);
            _tileImg.SetPixels(subPixels);
            _tileImg.Apply();
        }
    }
}