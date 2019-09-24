using System;

namespace MyMathLib {
    public static class AxisAngle {
        public static Matrix4X4 GetRotationMatrix(double a, Vector3 v) {
            var mat = new float[4, 4];
            a *= (Math.PI / 180);
            var cosA = (float)Math.Cos(a);
            var sinA = (float)Math.Sin(a);
            var invCosA = 1 - cosA;

            mat[0, 0] = invCosA * (float)Math.Pow(v.x, 2) + cosA;
            mat[0, 1] = invCosA * v.x * v.y + v.z * sinA;
            mat[0, 2] = invCosA * v.x * v.z + v.y * sinA;
            mat[0, 3] = 0;

            mat[1, 0] = mat[0, 1];
            mat[1, 1] = invCosA * (float)Math.Pow(v.y, 2) + cosA;
            mat[1, 2] = invCosA * v.y * v.z - v.x * sinA;
            mat[1, 3] = 0;

            mat[2, 0] = invCosA * v.x * v.z - v.y * sinA;
            mat[2, 1] = invCosA * v.y * v.z + v.x * sinA;
            mat[2, 2] = invCosA * (float)Math.Pow(v.z, 2) + cosA;
            mat[2, 3] = 0;

            mat[3, 0] = 0;
            mat[3, 1] = 0;
            mat[3, 2] = 0;
            mat[3, 3] = 1;

            return new Matrix4X4(mat);

        }
    }
}
