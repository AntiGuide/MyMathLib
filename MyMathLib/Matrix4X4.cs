using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathLib {
    /// <summary>Representation of a mathematical 4x4 matrix which can be used to transform points</summary>
    public class Matrix4X4 {
        /// <summary>The 2D array to represent the combined matrix including translation, rotation and scale</summary>
        private readonly float[,] matrix;

        /// <summary>Matrix containing only the scaling component of the final matrix</summary>
        private Matrix4X4 scaleMatrix;

        /// <summary>Matrix containing only the rotation component of the final matrix</summary>
        private Matrix4X4 rotationMatrix;

        /// <summary>Matrix containing only the translation component of the final matrix</summary>
        private Matrix4X4 translationMatrix;

        /// <summary>
        /// Create a final matrix from a pre created 2D float array
        /// </summary>
        /// <param name="matrix">A 2D float array with 4X4 dimensions to directly set</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the dimensions don't match the matrix dimensions'</exception>
        public Matrix4X4(float[,] matrix) {
            if (matrix.GetLength(0) != 4 || matrix.GetLength(1) != 4) {
                throw new ArgumentOutOfRangeException(nameof(matrix), "Array height and width has to be 4");
            }

            this.matrix = matrix;
        }
        
        /// <summary>
        /// Create a final matrix from a pre created one dimensional array or IReadOnlyList
        /// </summary>
        /// <param name="tmpMatrix">Array containing 16 elements</param>
        public Matrix4X4(IReadOnlyList<float> tmpMatrix) {
            var matrix2 = new float[4, 4];
            for (int i = 0; i < 4; i++) {
                for (int i2 = 0; i2 < 4; i2++) {
                    matrix2[i, i2] = tmpMatrix[i * 4 + i2];
                }
            }

            this.matrix = matrix2;
        }
        
        /// <summary>
        /// Create a final matrix from a pre created one dimensional list or IEnumerable
        /// </summary>
        /// <param name="tmpMatrix">List containing 16 elements</param>
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

        /// <summary>Create a matrix from 16 individual floats. m1 - m4 => row 1 and so on</summary>
        public Matrix4X4(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9, float m10, float m11, float m12, float m13, float m14, float m15, float m16) {
            var tmpMatrix = new[,] {{m1, m2, m3, m4}, {m5, m6, m7, m8}, {m9, m10, m11, m12}, {m13, m14, m15, m16}};
            this.matrix = tmpMatrix;
        }

        /// <summary>
        /// Create an identity matrix
        /// </summary>
        public Matrix4X4() {
            this.matrix = new float[4, 4];
            matrix[0, 0] = 1f;
            matrix[1, 1] = 1f;
            matrix[2, 2] = 1f;
            matrix[3, 3] = 1f;
        }

        /// <summary>
        /// Set the translation component of the final matrix. Used in Matrix4X4.Transform().
        /// </summary>
        /// <param name="vec">The translation vector. Points will get transformed by those values.</param>
        public void SetTranslation(Vector3 vec) {
            EnsureMatricesSetUp();

            translationMatrix[0, 3] = vec.x;
            translationMatrix[1, 3] = vec.y;
            translationMatrix[2, 3] = vec.z;

            ComputeMatrix();
        }

        /// <summary>
        /// Set the scale component of the final matrix. Used in Matrix4X4.Transform().
        /// </summary>
        /// <param name="vec">The scale vector. Points will get scaled by those values.</param>
        public void SetScale(Vector3 vec) {
            EnsureMatricesSetUp();

            scaleMatrix[0, 0] = vec.x;
            scaleMatrix[1, 1] = vec.y;
            scaleMatrix[2, 2] = vec.z;

            ComputeMatrix();
        }

        /// <summary>
        /// Set the rotation component of the final matrix using a quaternion. Used in Matrix4X4.Transform().
        /// </summary>
        /// <param name="q">The quaternion representation of a rotation</param>
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

        /// <summary>
        /// Set the rotation component of the final matrix using an angle axis representation. Used in Matrix4X4.Transform().
        /// </summary>
        /// <param name="angle">The angle to rotate around the axis.</param>
        /// <param name="axis">The axis to rotate around</param>
        public void SetRotation(float angle, Vector3 axis) {
            SetRotation(new Quaternion(angle, axis));
        }

        /// <summary>
        /// Set the rotation component of the final matrix using euler angles. Used in Matrix4X4.Transform().
        /// </summary>
        public void SetRotation(float x, float y, float z) {
            SetRotation(new Quaternion(x, y, z));
        }

        /// <summary>
        /// Sets translation, rotation and scale.
        /// </summary>
        /// <param name="translation">Move objects by those values</param>
        /// <param name="rotation">Rotate objects with this quaternion</param>
        /// <param name="scale">Scale objects by this values</param>
        public void SetTRS(Vector3 translation, Quaternion rotation, Vector3 scale) {
            SetTranslation(translation);
            SetRotation(rotation);
            SetScale(scale);
        }

        /// <summary>
        /// Internal method to compute the final matrix out of the translation, rotation and scale components set by SetTRS, SetRotation, SetScale and SetTranslation
        /// </summary>
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

        /// <summary>
        /// Checks if the components arrays are initialized and initializes them if they are not
        /// </summary>
        private void EnsureMatricesSetUp() {
            if (scaleMatrix is null) scaleMatrix = new Matrix4X4();
            if (rotationMatrix is null) rotationMatrix = new Matrix4X4();
            if (translationMatrix is null) translationMatrix = new Matrix4X4();
        }

        /// <summary>
        /// Access the final matrix with an indexer.
        /// </summary>
        /// <param name="i">The final matrixes row</param>
        /// <param name="i2">The final matrixes column</param>
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

        /// <summary>
        /// Transform a point by the matrix
        /// </summary>
        /// <param name="point">The point to transform</param>
        /// <returns>Returns a copy of the transformed point</returns>
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