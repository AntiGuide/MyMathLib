using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathLib {
    class Matrix4X4 {
        private double[,] matrix;

        public Matrix4X4(double[,] matrix) {
            if (matrix.GetLength(0) != 4 || matrix.GetLength(1) != 4) {
                throw new ArgumentOutOfRangeException(nameof(matrix), "Array height and width has to be 4");
            }

            this.matrix = matrix;
        }

        public Matrix4X4(Quaternion q) {
            var matrix = new double[4, 4];
            q = q.Normalize();

            matrix[0, 0] = 1 - 2 * Math.Pow(q.z, 2) - 2 * Math.Pow(q.w, 2);
            matrix[0, 1] = 2 * q.y * q.z - 2 * q.w * q.x;
            matrix[0, 2] = 2 * q.y * q.w + 2 * q.z * q.x;
            matrix[0, 3] = 0;

            matrix[1, 0] = 2 * q.y * q.z + 2 * q.w * q.x;
            matrix[1, 1] = 1 - 2 * Math.Pow(q.y, 2) - 2 * Math.Pow(q.w, 2);
            matrix[1, 2] = 2 * q.z * q.w - 2 * q.y * q.x;
            matrix[1, 3] = 0;

            matrix[2, 0] = 2 * q.y * q.w - 2 * q.z * q.x;
            matrix[2, 1] = 2 * q.z * q.w + 2 * q.y * q.x;
            matrix[2, 2] = 1 - 2 * Math.Pow(q.y, 2) - 2 * q.z;
            matrix[2, 3] = 0;

            matrix[3, 0] = 0;
            matrix[3, 1] = 0;
            matrix[3, 2] = 0;
            matrix[3, 3] = 1;

            this.matrix = matrix;
        }

        public Matrix4X4(double[] matrix) {
            var matrix2 = new double[4, 4];
            for (int i = 0; i < 4; i++) {
                for (int i2 = 0; i2 < 4; i2++) {
                    matrix2[i, i2] = matrix[i * 4 + i2];
                }
            }

            this.matrix = matrix2;
        }

        public Matrix4X4(IEnumerable<double> matrix) {
            var matrix2 = new double[4, 4];
            for (int i = 0; i < 4; i++) {
                for (int i2 = 0; i2 < 4; i2++) {
                    matrix2[i, i2] = matrix.ElementAt(i * 4 + i2);
                }
            }

            this.matrix = matrix2;
        }

        public Matrix4X4(double m1, double m2, double m3, double m4, double m5, double m6, double m7, double m8, double m9, double m10, double m11, double m12, double m13, double m14, double m15, double m16) {
            var matrix = new double[,] { { m1, m2, m3, m4 }, { m5, m6, m7, m8 }, { m9, m10, m11, m12 }, { m13, m14, m15, m16 } };
            this.matrix = matrix;
        }

        public Matrix4X4() {
            this.matrix = new double[4, 4];
        }

        public double this[int i, int i2] {
            get { return matrix[i, i2]; }
            set { matrix[i, i2] = value; }
        }

        public static Matrix4X4 operator +(Matrix4X4 a, Matrix4X4 b) {
            var ret = new Matrix4X4();
            for (int i = 0; i < 4; i++) {
                for (int i2 = 0; i2 < 4; i2++) {
                    ret[i, i2] = a[i, i2] + b[i, i2];
                }
            }

            return ret;
        }

        public static Matrix4X4 operator -(Matrix4X4 a, Matrix4X4 b) {
            return a + !b;
        }

        public static Matrix4X4 operator !(Matrix4X4 a) {
            var ret = new Matrix4X4();
            for (int i = 0; i < 4; i++) {
                for (int i2 = 0; i2 < 4; i2++) {
                    ret[i, i2] = -a[i, i2];
                }
            }

            return ret;
        }

        public static Matrix4X4 operator *(double a, Matrix4X4 b) {
            for (int i = 0; i < 4; i++) {
                for (int i2 = 0; i2 < 4; i2++) {
                    b[i, i2] *= a;
                }
            }

            return b;
        }

        public static Matrix4X4 operator *(Matrix4X4 a, double b) {
            return b * a;
        }

        public static Matrix4X4 operator *(Matrix4X4 a, Matrix4X4 b) {
            var ret = new Matrix4X4();
            for (int i = 0; i < 4; i++) {
                for (int i2 = 0; i2 < 4; i2++) {
                    var erg = 0d;
                    for (int i3 = 0; i3 < 4; i3++) {
                        erg += a[i, i3] * b[i3, i2];
                    }

                    ret[i, i2] = erg;
                }


            }

            return ret;
        }

        public static Vector3 operator *(Matrix4X4 a, Vector3 b) {
            var bArr = new double[4] { b.x, b.y, b.z, 1 };
            var ret = new double[3];
            for (int i = 0; i < 3; i++) {
                var erg = 0d;
                for (int i2 = 0; i2 < 4; i2++) {
                    erg += a[i, i2] * bArr[i2];
                }

                ret[i] = erg;
            }

            return new Vector3(ret[0], ret[1], ret[2]);
        }

        override public string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("|\t" + Math.Round(matrix[0, 0], 2) + "\t" + Math.Round(matrix[0, 1], 2) + "\t" + Math.Round(matrix[0, 2], 2) + "\t" + Math.Round(matrix[0, 3], 2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(matrix[1, 0], 2) + "\t" + Math.Round(matrix[1, 1], 2) + "\t" + Math.Round(matrix[1, 2], 2) + "\t" + Math.Round(matrix[1, 3], 2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(matrix[2, 0], 2) + "\t" + Math.Round(matrix[2, 1], 2) + "\t" + Math.Round(matrix[2, 2], 2) + "\t" + Math.Round(matrix[2, 3], 2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(matrix[3, 0], 2) + "\t" + Math.Round(matrix[3, 1], 2) + "\t" + Math.Round(matrix[3, 2], 2) + "\t" + Math.Round(matrix[3, 3], 2) + "\t|");
            return stringBuilder.ToString();
        }
    }
}
