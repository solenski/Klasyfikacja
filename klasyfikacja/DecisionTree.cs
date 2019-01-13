using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace klasyfikacja
{
    class DecisionTree
    {
        public int ClassifcationResultIndex { get; private set; }

        public List<DecisionTreeNode> Nodes;
        private int MaxAttributeIndex = 4;
        public void Train(int attributeIndex = 0, int classificationResultIndex = 5)
        {
            this.ClassifcationResultIndex = classificationResultIndex;
            this.Nodes = new List<DecisionTreeNode>();
            var groupedby = Data.ToTrain.GroupBy(x => x[attributeIndex]);



            foreach (var grouping in groupedby)
            {

                this.Nodes.Add(new DecisionTreeNode
                {
                    AttributeIndex = attributeIndex,
                    Data = grouping.ToList(),
                    Pure = IsPurePredicate(grouping)
                });

            }

            CreateNodes(attributeIndex + 1, Nodes);


        }

        private bool IsPurePredicate(IGrouping<string, List<string>> grouping)
        {
            return grouping.All(x => x[ClassifcationResultIndex] == "true") || grouping.All(x => x[ClassifcationResultIndex] == "false");
        }

        public bool Decide(List<string> ToClassify, List<DecisionTreeNode> nodes)
        {
            var node = nodes.FirstOrDefault(x => ToClassify[x.AttributeIndex] == x.Data.SelectMany(y => y).ToList()[x.AttributeIndex]);
            if (!node.Pure && node.ChildNodes != null)
            {
                return this.Decide(ToClassify, node.ChildNodes);
            }
            else if (node.Pure)
            {
                return node.Data.First()[ClassifcationResultIndex] == "true" ? true : false;
            }
            else
            {
                return Convert.ToDouble(node.Data.Count(x => x[ClassifcationResultIndex] == "true")) / node.Data.Count >
                       0.5
                    ? true
                    : false;
            }
        }

        private void CreateNodes(int attributeIndex, List<DecisionTreeNode> nodes)
        {
            foreach (var decisionTreeNode in nodes)
            {
                if (!decisionTreeNode.Pure)
                {
                    decisionTreeNode.ChildNodes = new List<DecisionTreeNode>();
                    foreach (var grouped in decisionTreeNode.Data.GroupBy(x => x[attributeIndex]))
                    {
                        decisionTreeNode.ChildNodes.Add(new DecisionTreeNode
                        {
                            AttributeIndex = attributeIndex,
                            Data = grouped.ToList(),
                            Pure = IsPurePredicate(grouped)
                        });
                    }
                }
            }
            if (attributeIndex != MaxAttributeIndex)
                CreateNodes(attributeIndex + 1, nodes.SelectMany(x => x.ChildNodes).ToList());

        }
    }
}
