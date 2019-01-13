using System.Collections.Generic;

namespace klasyfikacja
{
    internal class DecisionTreeNode
    {
        public List<List<string>> Data { get; internal set; }
        public int AttributeIndex { get; internal set; }
        public bool Pure { get; internal set; }
        public List<DecisionTreeNode> ChildNodes;
    }
}