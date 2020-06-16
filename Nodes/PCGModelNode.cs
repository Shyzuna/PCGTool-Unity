using System.Collections.Generic;
using XNode;

namespace PCGTool.Scripts.Nodes {
    public abstract class PCGModelNode : Node {
        public int width;
        public int height;

        public bool isMain;
        
        public abstract IEnumerable<TileCaseStruct> Generate(int seed);
    }
}