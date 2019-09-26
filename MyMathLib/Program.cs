using System;

namespace MyMathLib {
    public static class Program {
        private static void Main() {
            var mat = new Matrix4X4();
            mat.SetTRS(new Vector3(20, 0, 0), new Quaternion(0, 0, 90), new Vector3(3, 3, 3));
            var point = new Vector3(0, 10, 0);
            point = mat.Transform(point);
            var expectedResultPoint = new Vector3(-10, 0, 0);

            Console.WriteLine($"GotPoint: ({Round(point.x)}, {Round(point.y)}, {Round(point.z)})");
            Console.WriteLine($"Expected: ({Round(expectedResultPoint.x)}, {Round(expectedResultPoint.y)}, {Round(expectedResultPoint.z)})");
        }

        private static float Round(float number, int decimals = 5) {
            return (float) System.Math.Round(number, decimals, MidpointRounding.AwayFromZero);
        }
    }
}