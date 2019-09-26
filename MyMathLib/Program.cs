using System;

namespace MyMathLib {
    public static class Program {
        private static void Main() {
//            var mat = new Matrix4X4();
//            mat.SetRotation(-122f, 99f, 66f);//Unity Left Handed We are right handed
//            point = new Vector3(0,10,0);
//            point = mat.Transform(point);
//            Console.WriteLine(point);

            var mat = new Matrix4X4();
            //mat.SetTRS(new Vector3(20,10,5), new Quaternion (60,30,90), new Vector3(3,3,3));
            mat.SetTRS(new Vector3(20,10,5), new Quaternion (0.6830127f, 0.5f, -0.1830127f, 0.4999999f), new Vector3(3,3,3));
            var point = new Vector3(0, 10, 0);
            point = mat.Transform(point);
            //var expectedResultPoint = new Vector3(-10, 0, 0);

            Console.WriteLine(point);
        }
    }
}