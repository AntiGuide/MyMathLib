using System;

namespace MyMathLib {
    /// <summary>Representation of a mathematical 2x2 matrix which can be used to transform points</summary>
    public class Matrix2X2 {
        /// <summary>The 2D array to represent the combined matrix including translation, rotation and scale</summary>
        private readonly float[,] matrix;
        
        /// <summary>
        /// Create a final matrix from a pre created 2D float array
        /// </summary>
        /// <param name="matrix">A 2D float array with 2X2 dimensions to directly set</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the dimensions don't match the matrix dimensions'</exception>
        public Matrix2X2(float[,] matrix) {
            if (matrix.GetLength(0) != 2 || matrix.GetLength(1) != 2) {
                throw new ArgumentOutOfRangeException(nameof(matrix), "Array height and width has to be 2");
            }

            this.matrix = matrix;
        }

        /// <summary>Create a matrix from 4 individual floats. m1 - m2 => row 1 and so on</summary>
        public Matrix2X2(float m1, float m2, float m3, float m4) {
            var tmpMatrix = new[,] {{m1, m2}, {m3, m4}};
            this.matrix = tmpMatrix;
        }

        /// <summary>Calculate the determinant of the matrix</summary>
        public float Determinant => matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
    }
}