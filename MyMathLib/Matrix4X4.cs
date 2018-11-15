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

        public Matrix4X4(double[] matrix) {
            var matrix2 = new double[4,4];
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

        override public string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("|\t" + matrix[0, 0] + "\t" + matrix[0, 1] + "\t" + matrix[0, 2] + "\t" + matrix[0, 3] + "\t|");
            stringBuilder.AppendLine("|\t" + matrix[1, 0] + "\t" + matrix[1, 1] + "\t" + matrix[1, 2] + "\t" + matrix[1, 3] + "\t|");
            stringBuilder.AppendLine("|\t" + matrix[2, 0] + "\t" + matrix[2, 1] + "\t" + matrix[2, 2] + "\t" + matrix[2, 3] + "\t|");
            stringBuilder.AppendLine("|\t" + matrix[3, 0] + "\t" + matrix[3, 1] + "\t" + matrix[3, 2] + "\t" + matrix[3, 3] + "\t|");
            return stringBuilder.ToString();
        }
    }
}
