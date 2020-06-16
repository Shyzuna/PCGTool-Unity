using System;
using System.Collections;
using System.Collections.Generic;
using PCGTool.Scripts;
using PCGTool.Scripts.Nodes;
using UnityEngine;
using UnityEngine.Tilemaps;
using XNode;

// Display the graphs
namespace PCGTool.Scripts {
    [RequireComponent(typeof(Grid))]
    public class PCGDisplay : MonoBehaviour {

        // Specify tilemap : Be sure to setup the grid property → Read only property (cannot be modified by code)
        public enum TilemapType {
            RECTANGLE, ISOMETRIC, ISOMETRIC_ZY, HEXAGON_POINT_TOP, HEXAGON_FLAT_TOP
        }
        
        [SerializeField]
        private List<PCGGraph> _PCGGraphs = new List<PCGGraph>();

        [SerializeField]
        private int _seed;
        [SerializeField]
        private bool _generateSeed = true;
        [SerializeField]
        private bool _sameSeedAllGraph = true;
        
        [SerializeField]
        private TilemapType _tilemapType = TilemapType.RECTANGLE;

        private Dictionary<TilemapType, GameObject> _tilemapTemplates;
        private GridLayout _grid = null;
        private List<Tilemap> _tilemaps = new List<Tilemap>();

        // Start is called before the first frame update
        void Start() {
            RunPCGDisplay();
        }

        public void Reset() {
            foreach (Tilemap tm in _tilemaps) {
                if (tm != null) {
                    DestroyImmediate(tm.gameObject);
                }
            }
            _tilemaps.Clear();
            _seed = 0;
        }

        private void LoadTemplate() {
            // Load tilemap templates
            _tilemapTemplates = new Dictionary<TilemapType, GameObject>();
            _tilemapTemplates[TilemapType.RECTANGLE] = Resources.Load<GameObject>("TilemapsTemplate/BaseTM");
            _tilemapTemplates[TilemapType.ISOMETRIC] = Resources.Load<GameObject>("TilemapsTemplate/IsoTM");
            _tilemapTemplates[TilemapType.ISOMETRIC_ZY] = Resources.Load<GameObject>("TilemapsTemplate/IsoTMZY");
            _tilemapTemplates[TilemapType.HEXAGON_FLAT_TOP] = Resources.Load<GameObject>("TilemapsTemplate/HexaFTTM");
            _tilemapTemplates[TilemapType.HEXAGON_POINT_TOP] = Resources.Load<GameObject>("TilemapsTemplate/HexaPTTM");
        }
        
        public void RunPCGDisplay() {
            if (_generateSeed && _sameSeedAllGraph) {
                _seed = (int) DateTime.Now.Ticks;
            }

            if (_tilemapTemplates == null) {
                LoadTemplate();
            }

            _grid = GetComponent<Grid>();
            
            // Generate every graph using entry point
            foreach (PCGGraph graph in _PCGGraphs) {
                if (graph.mainModel != null) {
                    Generate(graph.mainModel, graph.name);
                } else {
                    Debug.LogWarning($"Missing Main model flag in {graph.name}");
                }
            }
        }
        
        private void Generate(PCGModelNode model, string graphName) {
            if (_generateSeed && !_sameSeedAllGraph) {
                _seed = (int) DateTime.Now.Ticks;
            }
            Vector3Int offset = _grid.WorldToCell(transform.position);

            GameObject go = Instantiate(_tilemapTemplates[_tilemapType], transform);
            go.name = graphName;
            Tilemap tilemap = go.GetComponent<Tilemap>();
            TilemapRenderer tilemapRenderer = go.GetComponent<TilemapRenderer>();
            _tilemaps.Add(tilemap);
            
            foreach (TileCaseStruct tcs in model.Generate(_seed)) {
                tilemap.SetTile(tcs.position + offset, tcs.tile);
            }
        }
    }
}