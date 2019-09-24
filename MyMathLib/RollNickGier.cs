using System;

namespace MyMathLib {
    public static class RollNickGier {
        public static Matrix4X4 GetRotationMatrix(double x, double y, double z) {
            z *= (Math.PI / 180);
            y *= (Math.PI / 180);
            x *= (Math.PI / 180);

            var sinA = (float)Math.Sin(z);
            var cosA = (float)Math.Cos(z);
            var sinB = (float)Math.Sin(y);
            var cosB = (float)Math.Cos(y);
            var sinC = (float)Math.Sin(x);
            var cosC = (float)Math.Cos(x);
            var m = new float[4, 4];

            m[0, 0] = cosA * cosB;
            m[0, 1] = cosA * sinB * sinC - sinA * cosC;
            m[0, 2] = cosA * sinB * cosC + sinA * sinC;
            m[0, 3] = 0;

            m[1, 0] = sinA * cosB;
            m[1, 1] = sinA * sinB * sinC + cosA * cosC;
            m[1, 2] = sinA * sinB * cosC - cosA * sinC;
            m[1, 3] = 0;

            m[2, 0] = -sinB;
            m[2, 1] = cosB * sinC;
            m[2, 2] = cosB * cosC;
            m[2, 3] = 0;

            m[3, 0] = 0;
            m[3, 1] = 0;
            m[3, 2] = 0;
            m[3, 3] = 1;

            return new Matrix4X4(m);
        }
    }
}
