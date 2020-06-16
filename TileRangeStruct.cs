using System;
using UnityEngine.Tilemaps;

namespace PCGTool.Scripts {
    [Serializable]
    public struct TileRangeStruct {
        public Tile tile;
        public int minVal;
        public int maxVal;
    }
}