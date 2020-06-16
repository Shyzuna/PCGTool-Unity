using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using XNode;

namespace PCGTool.Scripts.Nodes {
    public class DistributedTilesetNode : TilesetNode {
        [Output]
        public TileDistribStruct[] outTileset;
        
        public List<string> distribution = new List<string>();

        public int lastTileIndex = 0;

        // Output value formation
        public override object GetValue(NodePort port) {
            if (port.fieldName.Equals("outTileset")) {
                List<TileDistribStruct> output = new List<TileDistribStruct>();
                int i = 0;
                int distrib;
                Tile tile;
                foreach (NodePort np in DynamicInputs) {
                    if (!Int32.TryParse(distribution[i], out distrib)) {
                        Debug.LogWarning($"Invalid value (int) for distribution value : {np.fieldName}");
                        continue;
                    }
                    tile = (Tile) np.GetInputValue();
                    if (tile == null) {
                        Debug.LogWarning($"No tile node specified for : {np.fieldName}");
                        continue;
                    }
                    output.Add(new TileDistribStruct {
                        distrib = distrib,
                        tile = tile
                    });
                    i++;
                }
                return output.ToArray();
            }
            return null;
        }
    }
}