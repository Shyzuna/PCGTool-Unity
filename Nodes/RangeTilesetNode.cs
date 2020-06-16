using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using XNode;

namespace PCGTool.Scripts.Nodes {
    public class RangeTilesetNode : TilesetNode{
        [Output]
        public TileRangeStruct[] outTileset;

        public List<string> minValues = new List<string>();
        public List<string> maxValues = new List<string>();
        public List<NodePort> tileFields = new List<NodePort>();

        public int lastTileIndex = 0;

        public override object GetValue(NodePort port) {
            if (port.fieldName.Equals("outTileset")) {
                List<TileRangeStruct> output = new List<TileRangeStruct>();
                int i = 0;
                int minVal;
                int maxVal;
                Tile tile;
                foreach (NodePort np in tileFields) {
                    if (!Int32.TryParse(maxValues[i], out maxVal)) {
                        Debug.LogWarning($"Invalid value (int : 0-100) for max range tileset : {np.fieldName}");
                        continue;
                    }
                    if (!Int32.TryParse(minValues[i], out minVal)) {
                        Debug.LogWarning($"Invalid value (int : 0-100) for max range tileset : {np.fieldName}");
                        continue;
                    }
                    tile = (Tile) np.GetInputValue();
                    if (tile == null) {
                        Debug.LogWarning($"No tile node specified for : {np.fieldName}");
                        continue;
                    }
                    output.Add(new TileRangeStruct {
                        minVal = minVal,
                        maxVal = maxVal,
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