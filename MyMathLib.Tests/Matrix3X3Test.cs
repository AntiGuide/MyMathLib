using NUnit.Framework;

namespace MyMathLib.Tests {
    [TestFixture]
    public class Matrix3X3Test {
        [Test]
        public void DefaultCreation() {
            var mat = new Matrix3X3();
            mat.AssertValues(1, 0, 0, 0, 1, 0, 0, 0, 1);
        }

        [Test]
        public void ArrayCreation() {
            var mat = new Matrix3X3();
            mat.AssertValues(1f,0f,0f,0f,1f,0f,0f,0f,1f);
            
            var initArr = new[,] {
                {1f, 2f, 3f},
                {4f, 5f, 6f},
                {7f, 8f, 9f},
            };
            mat = new Matrix3X3(initArr);
            mat.AssertValues(initArr);
        }

        [Test]
        public void Add() {
            var matrixIn0 = new Matrix3X3(1, 5, -1, 11, 3, 4, 1, -1, 3);
            var matrixIn1 = new Matrix3X3(7, 2, 9, 1, 2, 5, 0, 2, 4);
            var mat = matrixIn0 + matrixIn1;
            mat.AssertValues(8, 7, 8, 12, 5, 9, 1, 1, 7);
        }

        [Test]
        public void ScalarMultiplication() {
            var matrixIn0 = new Matrix3X3(1, 5, -1, 11, 3, 4, 1, -1, 3);
            var mat = 5 * matrixIn0;
            mat.AssertValues(5, 25, -5, 55, 15, 20, 5, -5, 15);
        }

        [Test]
        public void MultiplicationMatrices() {
            var matrixIn0 = new Matrix3X3(1, 5, -1, 11, 3, 4, 1, -1, 3);
            var matrixIn1 = new Matrix3X3(7, 2, 9, 1, 2, 5, 0, 2, 4);
            var mat = matrixIn0 * matrixIn1;
            mat.AssertValues(12, 10, 30, 80, 36, 130, 6, 6, 16);
        }

        [Test]
        public void MultiplicationVector() {
            var matrixIn0 = new Matrix3X3(1, 5, -1, 11, 3, 4, 1, -1, 3);
            var mat = matrixIn0 * new Vector3(1, 2, 3);
            mat.AssertValues(8, 29, 8);
        }

        [Test]
        public void Determinant() {
            var matrixIn0 = new Matrix3X3(1, 5, -1, 11, 3, 4, 1, -1, 3);
            var det = matrixIn0.Determinant;
            Assert.That(det, Is.EqualTo(-118).Within(1).Percent);
        }

        [Test]
        public void Cofactor() {
            var matrixIn0 = new Matrix3X3(1, 5, -1, 11, 3, 4, 1, -1, 3);
            var cof = matrixIn0.Cofactor;
            cof.AssertValues(13, -29, -14, -14, 4, 6, 23, -15, -52);
        }

        [Test]
        public void Transpose() {
            var matrixIn0 = new Matrix3X3(1, 5, -1, 11, 3, 4, 1, -1, 3);
            var cof = matrixIn0.Transpose;
            cof.AssertValues(1, 11, 1, 5, 3, -1, -1, 4, 3);
        }

        [Test]
        public void Adjoint() {
            var matrixIn0 = new Matrix3X3(1, 5, -1, 11, 3, 4, 1, -1, 3);
            var adj = matrixIn0.Adjoint;
            adj.AssertValues(13,-14,23,-29,4,-15,-14,6,-52);
        }

        [Test]
        public void Inverse() {
            var matrixIn0 = new Matrix3X3(1, 5, -1, 11, 3, 4, 1, -1, 3);
            var mat = matrixIn0.Inverse;
            mat.AssertValues(-(13 / 118f), 7 / 59f, -(23 / 118f), 29 / 118f, -(2 / 59f), 15 / 118f, 7 / 59f, -(3 / 59f), 26 / 59f);
        }
    }

    public static class MatrixTestExtension {
        public static void AssertValues(this Matrix3X3 mat, float x0, float y0, float z0, float x1, float y1, float z1, float x2, float y2, float z2) {
            Assert.That(mat[0, 0], Is.EqualTo(x0).Within(0.000001));
            Assert.That(mat[0, 1], Is.EqualTo(y0).Within(0.000001));
            Assert.That(mat[0, 2], Is.EqualTo(z0).Within(0.000001));

            Assert.That(mat[1, 0], Is.EqualTo(x1).Within(0.000001));
            Assert.That(mat[1, 1], Is.EqualTo(y1).Within(0.000001));
            Assert.That(mat[1, 2], Is.EqualTo(z1).Within(0.000001));

            Assert.That(mat[2, 0], Is.EqualTo(x2).Within(0.000001));
            Assert.That(mat[2, 1], Is.EqualTo(y2).Within(0.000001));
            Assert.That(mat[2, 2], Is.EqualTo(z2).Within(0.000001));
        }

        public static void AssertValues(this Matrix3X3 mat, float[,] val) {
            mat.AssertValues(val[0, 0], val[0, 1], val[0, 2], val[1, 0], val[1, 1], val[1, 2], val[2, 0], val[2, 1], val[2, 2]);
        }
    }
}