using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using XNode;

namespace PCGTool.Scripts.Nodes {
    public class BasicPCGModelNode : PCGModelNode {
        [Input(typeConstraint = TypeConstraint.Strict)]
        public Tile[] allTileset;

        public override IEnumerable<TileCaseStruct> Generate(int seed) {
            List<Tile> finalSet = new List<Tile>();
            Tile[][] tmpSets = GetInputValues<Tile[]>("allTileset", null);
            if (tmpSets != null) {
                foreach (Tile[] set in tmpSets) {
                    foreach (Tile t in set) {
                        finalSet.Add(t);
                    }
                }
            } else {
                Debug.LogWarning("Missing Entry node Tileset");
                yield break;
            }
            
            Random.InitState(seed);
            int size = finalSet.Count;
            TileCaseStruct tcs;
            for (int i = 0; i < height; ++i) {
                for (int j = 0; j < width; ++j) {
                    tcs = new TileCaseStruct();
                    tcs.position = new Vector3Int(j, i, 0);
                    tcs.tile = finalSet[Random.Range(0, size)];
                    yield return tcs;
                }
            }
        }
        
    }
}