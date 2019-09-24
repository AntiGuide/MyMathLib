using NUnit.Framework;

namespace MyMathLib.Tests {
    [TestFixture]
    public class Matrix3X3Test {
        private Matrix3X3[] matrices;

        [SetUp]
        protected void SetUp() {
            matrices = new[] {
                new Matrix3X3(1, 5, -1, 11, 3, 4, 1, -1, 3),
                new Matrix3X3(7, 2, 9, 1, 2, 5, 0, 2, 4),
            };
        }

        [Test]
        public void DefaultCreation() {
            var mat = new Matrix3X3();
            mat.AssertValues(0, 0, 0, 0, 0, 0, 0, 0, 0);
        }

        [Test]
        public void ArrayCreation() {
            var initArr = new[,] {
                {1f, 2f, 3f},
                {4f, 5f, 6f},
                {7f, 8f, 9f},
            };
            var mat = new Matrix3X3(initArr);
            mat.AssertValues(initArr);
        }

        [Test]
        public void Add() {
            var mat = matrices[0] + matrices[1];
            mat.AssertValues(8, 7, 8, 12, 5, 9, 1, 1, 7);
        }

        [Test]
        public void ScalarMultiplication() {
            var mat = 5 * matrices[0];
            mat.AssertValues(5, 25, -5, 55, 15, 20, 5, -5, 15);
        }

        [Test]
        public void MultiplicationMatrices() {
            var mat = matrices[0] * matrices[1];
            mat.AssertValues(12, 10, 30, 80, 36, 130, 6, 6, 16);
        }

        [Test]
        public void MultiplicationVector() {
            var mat = matrices[0] * new Vector3(1, 2, 3);
            mat.AssertValues(8, 29, 8);
        }

        [Test]
        public void Determinant() {
            var det = matrices[0].Determinant;
            Assert.That(det, Is.EqualTo(-118).Within(1).Percent);
        }

        [Test]
        public void Cofactor() {
            var cof = matrices[0].Cofactor;
            cof.AssertValues(13, -29, -14, -14, 4, 6, 23, -15, -52);
        }

        [Test]
        public void Transpose() {
            var cof = matrices[0].Transpose;
            cof.AssertValues(1, 11, 1, 5, 3, -1, -1, 4, 3);
        }

        [Test]
        public void Adjoint() {
            var adj = matrices[0].Adjoint;
            adj.AssertValues(13,-14,23,-29,4,-15,-14,6,-52);
        }

        [Test]
        public void Inverse() {
            var mat = matrices[0].Inverse;
            mat.AssertValues(-(13 / 118f), 7 / 59f, -(23 / 118f), 29 / 118f, -(2 / 59f), 15 / 118f, 7 / 59f, -(3 / 59f), 26 / 59f);
        }
    }

    public static class MatrixTestExtension {
        public static void AssertValues(this Matrix3X3 mat, float x0, float y0, float z0, float x1, float y1, float z1, float x2, float y2, float z2) {
            Assert.That(mat[0, 0], Is.EqualTo(x0).Within(1).Percent);
            Assert.That(mat[0, 1], Is.EqualTo(y0).Within(1).Percent);
            Assert.That(mat[0, 2], Is.EqualTo(z0).Within(1).Percent);

            Assert.That(mat[1, 0], Is.EqualTo(x1).Within(1).Percent);
            Assert.That(mat[1, 1], Is.EqualTo(y1).Within(1).Percent);
            Assert.That(mat[1, 2], Is.EqualTo(z1).Within(1).Percent);

            Assert.That(mat[2, 0], Is.EqualTo(x2).Within(1).Percent);
            Assert.That(mat[2, 1], Is.EqualTo(y2).Within(1).Percent);
            Assert.That(mat[2, 2], Is.EqualTo(z2).Within(1).Percent);
        }

        public static void AssertValues(this Matrix3X3 mat, float[,] val) {
            mat.AssertValues(val[0, 0], val[0, 1], val[0, 2], val[1, 0], val[1, 1], val[1, 2], val[2, 0], val[2, 1], val[2, 2]);
        }
    }
}