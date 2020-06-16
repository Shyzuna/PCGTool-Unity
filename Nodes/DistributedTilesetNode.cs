using UnityEngine.Tilemaps;
using XNode;

namespace PCGTool.Scripts.Nodes {
    public class DistributedTilesetNode : Node{
        [Input(typeConstraint = TypeConstraint.Strict, backingValue = ShowBackingValue.Unconnected)]
        public Tile tileset;
        [Output]
        public Tile[] outTileset;
        public override object GetValue(NodePort port) {
            if (port.fieldName.Equals("outTileset")) {
                return GetInputValues("tileset", tileset);
            }
            return null;
        }
    }
}