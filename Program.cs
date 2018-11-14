using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quaternion {
    class Program {
        static void Main(string[] args) {
            var a = new Quaternion(1, 2, 3, 4);
            var b = new Quaternion(8, 7, 6, 5);
            var c = new Quaternion(9, 11, 13, 10);

            Console.Write("a) \t" + ((a + b + c) == (c + b + a)).ToString() + " \t");
            Console.WriteLine((a+b+c).ToString() + "\t\t = \t" + (c+b+a).ToString());

            Console.Write("b) \t" + ((a * b) == (b * a)).ToString() + " \t");
            Console.WriteLine((a * b).ToString() + "\t\t = \t" + (b * a).ToString());

            Console.Write("c) \t" + (((a + b) * c) == (a * c + b * c)).ToString() + " \t");
            Console.WriteLine(((a + b) * c).ToString() + "\t = \t" + (a*c + b*c).ToString());

            Console.Write("d) \t" + (((a * b) * c) == (a * (b * c))).ToString() + " \t");
            Console.WriteLine(((a * b) * c).ToString() + "\t = \t" + (a * (b * c)).ToString());

            Console.ReadLine();
        }
    }
}
