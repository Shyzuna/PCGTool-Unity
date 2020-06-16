using System.Collections.Generic;
using UnityEngine.Tilemaps;
using XNode;


namespace PCGTool.Scripts.Nodes {
    public class BasicTilesetNode : TilesetNode {
        [Input(typeConstraint = TypeConstraint.Strict)]
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