using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathLib {
    public class Matrix4X4 {
        private readonly float[,] matrix;

        private Matrix4X4 scaleMatrix;
        private Matrix4X4 rotationMatrix;
        private Matrix4X4 translationMatrix;

        public Matrix4X4(float[,] matrix) {
            if (matrix.GetLength(0) != 4 || matrix.GetLength(1) != 4) {
                throw new ArgumentOutOfRangeException(nameof(matrix), "Array height and width has to be 4");
            }

            this.matrix = matrix;
        }

        public Matrix4X4(IReadOnlyList<float> tmpMatrix) {
            var matrix2 = new float[4, 4];
            for (int i = 0; i < 4; i++) {
                for (int i2 = 0; i2 < 4; i2++) {
                    matrix2[i, i2] = tmpMatrix[i * 4 + i2];
                }
            }

            this.matrix = matrix2;
        }

        public Matrix4X4(IEnumerable<float> tmpMatrix) {
            var matrix2 = new float[4, 4];
            var enumerable = tmpMatrix as float[] ?? tmpMatrix.ToArray();
            for (var i = 0; i < 4; i++) {
                for (int i2 = 0; i2 < 4; i2++) {
                    matrix2[i, i2] = enumerable[i * 4 + i2];
                }
            }

            this.matrix = matrix2;
        }

        public Matrix4X4(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9, float m10, float m11, float m12, float m13, float m14, float m15, float m16) {
            var tmpMatrix = new[,] {{m1, m2, m3, m4}, {m5, m6, m7, m8}, {m9, m10, m11, m12}, {m13, m14, m15, m16}};
            this.matrix = tmpMatrix;
        }

        public Matrix4X4() {
            this.matrix = new float[4, 4];
            matrix[0, 0] = 1f;
            matrix[1, 1] = 1f;
            matrix[2, 2] = 1f;
            matrix[3, 3] = 1f;
        }

        public void SetTranslation(Vector3 vec) {
            EnsureMatricesSetUp();

            translationMatrix[0, 3] = vec.x;
            translationMatrix[1, 3] = vec.y;
            translationMatrix[2, 3] = vec.z;

            ComputeMatrix();
        }

        public void SetScale(Vector3 vec) {
            EnsureMatricesSetUp();

            scaleMatrix[0, 0] = vec.x;
            scaleMatrix[1, 1] = vec.y;
            scaleMatrix[2, 2] = vec.z;

            ComputeMatrix();
        }

        public void SetRotation(Quaternion q) {
            EnsureMatricesSetUp();

            q = q.Normalize();

            rotationMatrix[0, 0] = 1 - 2 * (q.y * q.y + q.z * q.z);
            rotationMatrix[0, 1] =     2 * (q.x * q.y - q.z * q.w);
            rotationMatrix[0, 2] =     2 * (q.x * q.z + q.y * q.w);
            
            rotationMatrix[1, 0] =     2 * (q.x * q.y + q.z * q.w);
            rotationMatrix[1, 1] = 1 - 2 * (q.x * q.x + q.z * q.z);
            rotationMatrix[1, 2] =     2 * (q.y * q.z - q.x * q.w);
            
            rotationMatrix[2, 0] =     2 * (q.x * q.z - q.y * q.w);
            rotationMatrix[2, 1] =     2 * (q.y * q.z + q.x * q.w);
            rotationMatrix[2, 2] = 1 - 2 * (q.x * q.x + q.y * q.y);
            
            ComputeMatrix();
        }

        public void SetRotation(float angle, Vector3 axis) {
            SetRotation(new Quaternion(angle, axis));
        }

        public void SetRotation(float x, float y, float z) {
            SetRotation(new Quaternion(x, y, z));
        }

        public void SetTRS(Vector3 translation, Quaternion rotation, Vector3 scale) {
            SetTranslation(translation);
            SetRotation(rotation);
            SetScale(scale);
        }

        private void ComputeMatrix() {
            // Scale, Rotate, Translate
            //var result = scaleMatrix * rotationMatrix * translationMatrix;
            var result = translationMatrix * rotationMatrix * scaleMatrix;
            //result.matrix.CopyTo(matrix, 0);
            for (var x = 0; x < matrix.GetLength(0); x++) {
                for (var y = 0; y < matrix.GetLength(1); y++) {
                    matrix[x, y] = result.matrix[x, y];
                }
            }
        }

        private void EnsureMatricesSetUp() {
            if (scaleMatrix is null) scaleMatrix = new Matrix4X4();
            if (rotationMatrix is null) rotationMatrix = new Matrix4X4();
            if (translationMatrix is null) translationMatrix = new Matrix4X4();
        }

        public float this[int i, int i2] {
            get => matrix[i, i2];
            set => matrix[i, i2] = value;
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

        public static Matrix4X4 operator *(float a, Matrix4X4 b) {
            for (var i = 0; i < 4; i++) {
                for (var i2 = 0; i2 < 4; i2++) {
                    b[i, i2] *= a;
                }
            }

            return b;
        }

        public static Matrix4X4 operator *(Matrix4X4 a, float b) {
            return b * a;
        }

        public static Matrix4X4 operator *(Matrix4X4 a, Matrix4X4 b) {
            var ret = new Matrix4X4();
            for (var i = 0; i < 4; i++) {
                for (var i2 = 0; i2 < 4; i2++) {
                    var erg = 0f;
                    for (var i3 = 0; i3 < 4; i3++) {
                        erg += a[i, i3] * b[i3, i2];
                    }

                    ret[i, i2] = erg;
                }
            }

            return ret;
        }

        public static Vector3 operator *(Matrix4X4 a, Vector3 b) {
            var bArr = new Vector4(b.x, b.y, b.z, 1f);
            var ret = new Vector4();
            for (var i = 0; i < 4; i++) {
                var erg = 0f;
                for (var i2 = 0; i2 < 4; i2++) {
                    erg += a[i, i2] * bArr[i2];
                }

                ret[i] = erg;
            }

            return new Vector3(ret.x, ret.y, ret.z);
        }

        public static Vector3 operator *(Vector3 a, Matrix4X4 b) {
            var aArr = new Vector4(a.x, a.y, a.z, 1f);
            var ret = new Vector3();
            for (var i = 0; i < 3; i++) {
                var erg = 0f;
                for (var i2 = 0; i2 < 4; i2++) {
                    erg += aArr[i2] * b[i2, i];
                }

                ret[i] = erg;
            }

            return ret;
        }

        public Vector3 Transform(Vector3 point) {
            return this * point;
        }

        public override string ToString() {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("|\t" + Math.Round(matrix[0, 0], 2) + "\t" + Math.Round(matrix[0, 1], 2) + "\t" + Math.Round(matrix[0, 2], 2) + "\t" + Math.Round(matrix[0, 3], 2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(matrix[1, 0], 2) + "\t" + Math.Round(matrix[1, 1], 2) + "\t" + Math.Round(matrix[1, 2], 2) + "\t" + Math.Round(matrix[1, 3], 2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(matrix[2, 0], 2) + "\t" + Math.Round(matrix[2, 1], 2) + "\t" + Math.Round(matrix[2, 2], 2) + "\t" + Math.Round(matrix[2, 3], 2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(matrix[3, 0], 2) + "\t" + Math.Round(matrix[3, 1], 2) + "\t" + Math.Round(matrix[3, 2], 2) + "\t" + Math.Round(matrix[3, 3], 2) + "\t|");
            return stringBuilder.ToString();
        }
    }
}