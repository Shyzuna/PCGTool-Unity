using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PCGTool.Scripts.Nodes {
    public class DistributedPCGModelNode : PCGModelNode {
        [Input(typeConstraint = TypeConstraint.Strict)]
        public TileDistribStruct[] allTileset;


        public override IEnumerable<TileCaseStruct> Generate(int seed) {
            //Setting seed
            Random.InitState(seed);
            
            // Building Final tileset
            float totalDistribution = 0;
            List<TileDistribStruct> finalSet = new List<TileDistribStruct>();
            TileDistribStruct[][] tmpSets = GetInputValues<TileDistribStruct[]>("allTileset", null);
            if (tmpSets != null) {
                // Does it keep same order ?
                foreach (TileDistribStruct[] set in tmpSets) {
                    foreach (TileDistribStruct t in set) {
                        finalSet.Add(t);
                        totalDistribution += t.distrib;
                    }
                }
            } else {
                Debug.LogWarning("Missing Entry node DistributedTileset");
                yield break;
            }

            int size = finalSet.Count;
            TileCaseStruct tcs;
            for (int i = 0; i < height; ++i) {
                for (int j = 0; j < width; ++j) {
                    tcs = new TileCaseStruct();
                    tcs.position = new Vector3Int(j, i, 0);
                    tcs.tile = GetDistributedTile(Random.Range(0f, totalDistribution), finalSet);
                    yield return tcs;
                }
            }
        }

        private Tile GetDistributedTile(float val, List<TileDistribStruct> finalSet) {
            float currentDistribution = 0;
            foreach (TileDistribStruct tds in finalSet) {
                currentDistribution += tds.distrib;
                if (val <= currentDistribution) {
                    return tds.tile;
                }
            }

            return null;
        }
    }
}