using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace klasyfikacja
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new DecisionTree();
            x.Train();



            foreach (var classify in Data.ToClassify)
            {
                var result = x.Decide(classify, x.Nodes);
                Console.WriteLine($"Decision for { string.Join(Environment.NewLine, classify) } is { result.ToString() }  ");

            }

            Console.ReadLine();
        }
    }
}
