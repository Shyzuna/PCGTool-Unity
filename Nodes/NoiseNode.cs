using ProceduralNoiseProject;
using UnityEngine;
using XNode;

// Check about value between frequency/amplitude on fractal/noise

namespace PCGTool.Scripts.Nodes {
    public class NoiseNode : Node {
        
        public enum NOISE_TYPE {  PERLIN, VALUE, SIMPLEX, VORONOI, WORLEY }
        
        [Output]
        public NoiseNode noiseNode;

        public NOISE_TYPE noiseType;
        public float scale = 1f; // Frequency / scale ?
        public float amplitude = 1f; // Only for worley → maybe hide otherwise
        public int octaves = 4;
        public float minOffset = 0f;
        public float maxOffset = 0f;

        protected float _offsetX;
        protected float _offsetY;
        protected Noise _noise;
        protected FractalNoise _fractalNoise;
        
        public override object GetValue(NodePort port) {
            if (port.fieldName.Equals("noiseNode")) {
                return this;
            }
            return null;
        }

        // Select the right noise 
        public virtual void SetUpNoise(int seed) {
            _offsetX = Random.Range(minOffset, maxOffset);
            _offsetY = Random.Range(minOffset, maxOffset);
            switch (noiseType) {
                case NOISE_TYPE.PERLIN:
                    _noise = new PerlinNoise(seed, scale);
                    break;
                case NOISE_TYPE.VALUE:
                    _noise = new ValueNoise(seed, scale);
                    break;
                case NOISE_TYPE.SIMPLEX:
                    _noise = new SimplexNoise(seed, scale);
                    break;
                case NOISE_TYPE.VORONOI:
                    _noise = new VoronoiNoise(seed, scale);
                    break;
                case NOISE_TYPE.WORLEY:
                    _noise = new WorleyNoise(seed, scale, amplitude);
                    break;
                default:
                    _noise = new PerlinNoise(seed, scale);
                    break;
            }
            _fractalNoise = new FractalNoise(_noise, octaves, 1f); // Should use frequency here ?
        }

        // Get sampled value of 2D noise
        public virtual float GetNoiseAt(float x, float y, float width, float height) {
            float res = _noise.Sample2D(x / (width - 1), y / (height - 1));
            return res;
        }
    }
}