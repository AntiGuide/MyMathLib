using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMathLib {
    /// <summary>Representation of a mathematical 3x3 matrix which can be used to transform points</summary>
    public class Matrix3X3 {
        /// <summary>The 2D array to represent the combined matrix including translation, rotation and scale</summary>
        private readonly float[,] matrix;

        public Matrix3X3(float[,] matrix) {
            if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3) {
                throw new ArgumentOutOfRangeException(nameof(matrix), "Array height and width has to be 3");
            }

            this.matrix = matrix;
        }

        /// <summary>
        /// Create a final matrix from a pre created one dimensional array or IReadOnlyList
        /// </summary>
        /// <param name="tmpMatrix">Array containing 9 elements</param>
        public Matrix3X3(IReadOnlyList<float> tmpMatrix) {
            var matrix2 = new float[3, 3];
            for (var i = 0; i < 3; i++) {
                for (var i2 = 0; i2 < 3; i2++) {
                    matrix2[i, i2] = tmpMatrix[i * 3 + i2];
                }
            }

            this.matrix = matrix2;
        }

        /// <summary>
        /// Create a final matrix from a pre created one dimensional list or IEnumerable
        /// </summary>
        /// <param name="tmpMatrix">List containing 9 elements</param>
        public Matrix3X3(IEnumerable<float> tmpMatrix) {
            var matrix2 = new float[3, 3];
            var enumerable = tmpMatrix as float[] ?? tmpMatrix.ToArray();
            for (var i = 0; i < 3; i++) {
                for (int i2 = 0; i2 < 3; i2++) {
                    matrix2[i, i2] = enumerable[i * 3 + i2];
                }
            }

            this.matrix = matrix2;
        }

        /// <summary>Create a matrix from 9 individual floats. m1 - m3 => row 1 and so on</summary>
        public Matrix3X3(float m1, float m2, float m3, float m4, float m5, float m6, float m7, float m8, float m9) {
            var tmpMatrix = new[,] {{m1, m2, m3}, {m4, m5, m6}, {m7, m8, m9}};
            this.matrix = tmpMatrix;
        }

        /// <summary>
        /// Create an identity matrix
        /// </summary>
        public Matrix3X3() {
            this.matrix = new float[3, 3];
            matrix[0, 0] = 1f;
            matrix[1, 1] = 1f;
            matrix[2, 2] = 1f;
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

        public static Matrix3X3 operator +(Matrix3X3 a, Matrix3X3 b) {
            var ret = new Matrix3X3();
            for (int i = 0; i < 3; i++) {
                for (int i2 = 0; i2 < 3; i2++) {
                    ret[i, i2] = a[i, i2] + b[i, i2];
                }
            }

            return ret;
        }

        public static Matrix3X3 operator -(Matrix3X3 a, Matrix3X3 b) {
            return a + !b;
        }

        public static Matrix3X3 operator !(Matrix3X3 a) {
            var ret = new Matrix3X3();
            for (int i = 0; i < 3; i++) {
                for (int i2 = 0; i2 < 3; i2++) {
                    ret[i, i2] = -a[i, i2];
                }
            }

            return ret;
        }

        public static Matrix3X3 operator *(float a, Matrix3X3 b) {
            for (var i = 0; i < 3; i++) {
                for (var i2 = 0; i2 < 3; i2++) {
                    b[i, i2] *= a;
                }
            }

            return b;
        }

        public static Matrix3X3 operator *(Matrix3X3 a, float b) {
            return b * a;
        }

        public static Matrix3X3 operator *(Matrix3X3 a, Matrix3X3 b) {
            var ret = new Matrix3X3();
            for (var i = 0; i < 3; i++) {
                for (var i2 = 0; i2 < 3; i2++) {
                    var erg = 0f;
                    for (var i3 = 0; i3 < 3; i3++) {
                        erg += a[i, i3] * b[i3, i2];
                    }

                    ret[i, i2] = erg;
                }


            }

            return ret;
        }

        public static Vector3 operator *(Matrix3X3 a, Vector3 b) {
            var bArr = new[] {b.x, b.y, b.z, 1f};
            var ret = new float[3];
            for (var i = 0; i < 3; i++) {
                var erg = 0f;
                for (var i2 = 0; i2 < 3; i2++) {
                    erg += a[i, i2] * bArr[i2];
                }

                ret[i] = erg;
            }

            return new Vector3(ret[0], ret[1], ret[2]);
        }

        /// <summary>
        /// Calculate the inverse of the matrix
        /// </summary>
        public Matrix3X3 Inverse {
            get {
                var det = Determinant; // Calculate Determinant
                var adj = Adjoint; // Calculate Adjoint. Adjoint = transpose(cofactor matrix)
                return 1 / det * adj; // (1/Determinant) * Adjoint
            }
        }
        
        /// <summary>
        /// Calculate the determinant of the matrix
        /// </summary>
        public float Determinant => matrix[0, 0]*(matrix[1, 1]*matrix[2, 2] - matrix[1, 2]*matrix[2, 1]) - 
                                    matrix[0, 1]*(matrix[1, 0]*matrix[2, 2] - matrix[1, 2]*matrix[2, 0]) +
                                    matrix[0, 2]*(matrix[1, 0]*matrix[2, 1] - matrix[1, 1]*matrix[2, 0]);
        
        /// <summary>
        /// Calculate the adjoint of this matrix
        /// </summary>
        public Matrix3X3 Adjoint => this.Cofactor.Transpose;
        
        /// <summary>
        /// Calculate the cofactor of the matrix
        /// </summary>
        public Matrix3X3 Cofactor {
            get {
                var ret = new Matrix3X3 {
                    [0, 0] = new Matrix2X2(this[1, 1], this[1, 2], this[2, 1], this[2, 2]).Determinant,
                    [0, 1] = -new Matrix2X2(this[1, 0], this[1, 2], this[2, 0], this[2, 2]).Determinant,
                    [0, 2] = new Matrix2X2(this[1, 0], this[1, 1], this[2, 0], this[2, 1]).Determinant,
                    
                    [1, 0] = -new Matrix2X2(this[0, 1], this[0, 2], this[2, 1], this[2, 2]).Determinant,
                    [1, 1] = new Matrix2X2(this[0, 0], this[0, 2], this[2, 0], this[2, 2]).Determinant,
                    [1, 2] = -new Matrix2X2(this[0, 0], this[0, 1], this[2, 0], this[2, 1]).Determinant,
                    
                    [2, 0] = new Matrix2X2(this[0, 1], this[0, 2], this[1, 1], this[1, 2]).Determinant,
                    [2, 1] = -new Matrix2X2(this[0, 0], this[0, 2], this[1, 0], this[1, 2]).Determinant,
                    [2, 2] = new Matrix2X2(this[0, 0], this[0, 1], this[1, 0], this[1, 1]).Determinant
                };


                return ret;
            }
        }
        
        /// <summary>
        /// Calculates the transpose of the matrix
        /// </summary>
        public Matrix3X3 Transpose {
            get {
                var ret = new Matrix3X3 {
                    [0, 0] = this[0, 0],
                    [0, 1] = this[1, 0],
                    [0, 2] = this[2, 0],
                    
                    [1, 0] = this[0, 1],
                    [1, 1] = this[1, 1],
                    [1, 2] = this[2, 1],
                    
                    [2, 0] = this[0, 2],
                    [2, 1] = this[1, 2],
                    [2, 2] = this[2, 2]
                };
                return ret;
            }
        }

        public override string ToString() {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("|\t" + Math.Round(matrix[0, 0], 2) + "\t" + Math.Round(matrix[0, 1], 2) + "\t" + Math.Round(matrix[0, 2], 2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(matrix[1, 0], 2) + "\t" + Math.Round(matrix[1, 1], 2) + "\t" + Math.Round(matrix[1, 2], 2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(matrix[2, 0], 2) + "\t" + Math.Round(matrix[2, 1], 2) + "\t" + Math.Round(matrix[2, 2], 2) + "\t|");
            return stringBuilder.ToString();
        }
    }
}
