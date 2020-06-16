using UnityEditor;
using UnityEngine;

namespace PCGTool.Scripts.Editor {
    [CustomEditor(typeof(PCGDisplay)), CanEditMultipleObjects]
    public class PCGDisplayEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            PCGDisplay pcgDisplay = (PCGDisplay) target;
            
            if (GUILayout.Button("Generate")) {
                pcgDisplay.RunPCGDisplay();
            }
            if (GUILayout.Button("Clear & Generate")) {
                pcgDisplay.Reset();
                pcgDisplay.RunPCGDisplay();
            }
            if (GUILayout.Button("Reset")) {
                pcgDisplay.Reset();
            }
        }
    }
}