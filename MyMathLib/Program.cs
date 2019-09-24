using System;

namespace MyMathLib {
    public static class Program {
        private static void Main() {
            var a = new Quaternion(1, 2, 3, 4);
            var b = new Quaternion(8, 7, 6, 5);
            var c = new Quaternion(9, 11, 13, 10);

            Console.WriteLine();
            Console.WriteLine("Quaternion Tests");
            Console.WriteLine("----------------");
            Console.WriteLine();

            Console.Write("a) \t" + (a + b + c == c + b + a) + " \t");
            Console.WriteLine(a + b + c + "\t\t\t= \t" + (c + b + a));

            Console.Write("b) \t" + (a * b == b * a) + " \t");
            Console.WriteLine(a * b + "\t\t\t= \t" + b * a);

            Console.Write("c) \t" + ((a + b) * c == a * c + b * c) + " \t");
            Console.WriteLine((a + b) * c + "\t\t= \t" + (a * c + b * c));

            Console.Write("d) \t" + (a * b * c == a * (b * c)) + " \t");
            Console.WriteLine(a * b * c + "\t\t= \t" + a * (b * c));

            Console.WriteLine();

            Console.Write("Division:");
            Console.WriteLine("\t" + a * b / a);

            Console.Write("Inverse:");
            Console.WriteLine("\t" + a.Inverse() + "\t= \t" + Quaternion.Conjugate(a) / (float)Math.Pow(a.Abs(), 2));

            var d = new Matrix4X4();
            var e = new Matrix4X4(new float[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
            var f = new Matrix4X4(new float[,] { { 0, 1, 2, 3 }, { 4, 5, 6, 7 }, { 8, 9, 10, 11 }, { 12, 13, 14, 15 } });

            Console.WriteLine();
            Console.WriteLine("Matrix Tests");
            Console.WriteLine("------------");
            Console.WriteLine();

            Console.WriteLine("Initialization:");
            Console.WriteLine(d.ToString());

            Console.WriteLine("d + e:");
            Console.WriteLine((d + e).ToString());

            Console.WriteLine("d - e:");
            Console.WriteLine((d - e).ToString());

            Console.WriteLine("e * f:");
            Console.WriteLine((e * f).ToString());

            var g = new Vector3();
            var h = new Vector3(1, -5, 2 );
            var i = new Vector3(2, 0, 3);

            Console.WriteLine();
            Console.WriteLine("Vector Tests");
            Console.WriteLine("------------");
            Console.WriteLine();

            Console.WriteLine("Initialization:");
            Console.WriteLine(g.ToString());

            Console.WriteLine("g + h:");
            Console.WriteLine((g + h).ToString());

            Console.WriteLine("g - h:");
            Console.WriteLine((g - h).ToString());

            Console.WriteLine("h * i:");
            Console.WriteLine(Vector3.Dot(h, i));
            
            Console.WriteLine("h x i:");
            Console.WriteLine(Vector3.Cross(h, i).ToString());

            Console.WriteLine();
            Console.WriteLine("Rotation Matrix Tests");
            Console.WriteLine("------------");
            Console.WriteLine();

            Console.WriteLine(RollNickGier.GetRotationMatrix(90, 90, 90).ToString());
            Console.WriteLine();
            Console.WriteLine(AxisAngle.GetRotationMatrix(90, new Vector3(0, 1, 0)).ToString());
            Console.WriteLine();
            Console.WriteLine((AxisAngle.GetRotationMatrix(90, new Vector3(0, 1, 0)) * new Vector3(5,0,0)).ToString());

            var q = new Quaternion(90, new Vector3(0, 1, 0));
            var v = new Quaternion(0, 5, 0, 0);
            var q2 = Quaternion.Conjugate(q);
            var res = q * v * q2;

            Console.WriteLine(new Vector3(res.y, res.z, res.w).ToString());

            Console.WriteLine((new Matrix4X4(q) * new Vector3(5, 0, 0)).ToString());

            Console.ReadLine();
        }
    }
}
