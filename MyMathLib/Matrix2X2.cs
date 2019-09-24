using System;

namespace MyMathLib {
    public class Matrix2X2 {
        private readonly float[,] matrix;

        public Matrix2X2(float[,] matrix) {
            if (matrix.GetLength(0) != 2 || matrix.GetLength(1) != 2) {
                throw new ArgumentOutOfRangeException(nameof(matrix), "Array height and width has to be 2");
            }

            this.matrix = matrix;
        }

        public Matrix2X2(float m1, float m2, float m3, float m4) {
            var tmpMatrix = new[,] {{m1, m2}, {m3, m4}};
            this.matrix = tmpMatrix;
        }

        public float Determinant => matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];
    }
}