using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PCGTool.Scripts.Nodes {
    public class NoisePCGModelNode : PCGModelNode {
        [Input(typeConstraint = TypeConstraint.Strict)]
        public TileRangeStruct[] allTileset;

        [Input(typeConstraint = TypeConstraint.Inherited, connectionType = ConnectionType.Override)]
        public NoiseNode noiseNode;

        public override IEnumerable<TileCaseStruct> Generate(int seed) {
            //Setting seed
            Random.InitState(seed);

            // Checking and init noise
            NoiseNode noise = GetInputValue("noiseNode", noiseNode);
            if (noise != null) {
                noise.SetUpNoise(seed);
            } else {
                Debug.LogWarning("Missing Entry node Noise");
                yield break;
            }

            // Building Final tileset
            List<TileRangeStruct> finalSet = new List<TileRangeStruct>();
            TileRangeStruct[][] tmpSets = GetInputValues<TileRangeStruct[]>("allTileset", null);
            if (tmpSets != null) {
                foreach (TileRangeStruct[] set in tmpSets) {
                    foreach (TileRangeStruct t in set) {
                        finalSet.Add(t);
                    }
                }
            } else {
                Debug.LogWarning("Missing Entry node RangeTileset");
                yield break;
            }
            
            float [,] noiseSample = new float[width, height];
            for (int i = 0; i < height; ++i) {
                for (int j = 0; j < width; ++j) {
                    noiseSample[j, i] = noise.GetNoiseAt(j, i, width, height);
                }
            }

            NormalizeArray(noiseSample);
            
            int size = finalSet.Count;
            TileCaseStruct tcs;
            for (int i = 0; i < height; ++i) {
                for (int j = 0; j < width; ++j) {
                    tcs = new TileCaseStruct();
                    tcs.position = new Vector3Int(j, i, 0);
                    tcs.tile = CheckInsideRange(noiseSample[j, i], finalSet);
                    yield return tcs;
                }
            }
        }

        public Tile CheckInsideRange(float val, List<TileRangeStruct> set) {
            foreach (TileRangeStruct trs in set) {
                // Maybe do the division before
                if (val >= trs.minVal / 100.0 && val <= trs.maxVal / 100.0) {
                    return trs.tile;
                }
            }

            Debug.LogWarning($"No tile for Range value {val * 100}");
            return null;
        }

        // Utilitary method to normalize noise array
        private void NormalizeArray(float[,] arr) {
            float min = float.PositiveInfinity;
            float max = float.NegativeInfinity;

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    float v = arr[x, y];
                    if (v < min) min = v;
                    if (v > max) max = v;
                }
            }

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    float v = arr[x, y];
                    arr[x, y] = (v - min) / (max - min);
                }
            }
        }
        
    }
}