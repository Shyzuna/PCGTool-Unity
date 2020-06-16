using System.Collections.Generic;
using XNode;

// Just a test node who helped me to solve a problem ^^'

namespace PCGTool.Scripts.Nodes {
    public class TestNode : Node {
        [Output]
        public int value;

        public string test;
        public List<NodePort> nodeList = new List<NodePort>();
        public int index = 0;

        public override object GetValue(NodePort port) {
            return null;
        }
    }
}