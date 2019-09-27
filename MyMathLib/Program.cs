using System;

namespace MyMathLib {
    public static class Program {
        private static void Main() {
            var mat = new Matrix4X4();

            // Set translation, rotation and scale for matrix
            mat.SetTRS(new Vector3(20, 0, 0), new Quaternion(0, 0, 90), new Vector3(3, 3, 3));

            var point = new Vector3(0, 10, 0);

            // Transform point with created matrix
            point = mat.Transform(point);

            var expectedResultPoint = new Vector3(-10, 0, 0);

            // Print transformed and expected point to compare
            Console.WriteLine($"GotPoint: ({Round(point.x)}, {Round(point.y)}, {Round(point.z)})");
            Console.WriteLine($"Expected: ({Round(expectedResultPoint.x)}, {Round(expectedResultPoint.y)}, {Round(expectedResultPoint.z)})");
        }

        /// <summary>Rounds a float away from zero with 5 decimals as a standard</summary>
        private static float Round(float number, int decimals = 5) {
            return (float) System.Math.Round(number, decimals, MidpointRounding.AwayFromZero);
        }
    }
}
