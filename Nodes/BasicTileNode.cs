using UnityEngine.Tilemaps;
using XNode;

namespace PCGTool.Scripts.Nodes {
    public class BasicTileNode : TileNode {
        [Output]
        public Tile tile;

        public override object GetValue(NodePort port) {
            if (port.fieldName.Equals("tile")) {
                return tile;
            }
            return null;
        }
    }
}