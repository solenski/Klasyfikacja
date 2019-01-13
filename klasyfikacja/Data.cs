using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace klasyfikacja
{
    public static class Data
    {
        public static List<List<string>> ToClassify => new List<List<string>>
        {
            new List<string>
            {
                "MEZCZYZNA", "NISKIE", "PODSTAWOWE", "BEZROBOTNY", "powyzej_60"
            },
            new List<string>
            {
                "KOBIETA", "WYSOKIE", "WYZSZE", "UMOWA_NA_CZAS_NIEOKRESLONY", "od_30_do_40"
            }
        };

        public static List<List<string>> ToTrain { get; } =
            File.ReadAllLines("klasyfikacja.txt").Select(x => x.Split(' ').Skip(2).ToList()).ToList();


    }
}
