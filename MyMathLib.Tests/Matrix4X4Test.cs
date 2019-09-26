using System;
using NUnit.Framework;

namespace MyMathLib.Tests {
    [TestFixture]
    public class Matrix4X4Test {
        
        [Test]
        public void SetRotationQuaternion() {
            var qat = new Quaternion(0.7071067811865476f, 0f, 0f, 0.7071067811865476f);
            var mat = new Matrix4X4();
            mat.SetRotation(qat);
            var expectedValue = new[,] {
                {1f, 0f, 0f, 0f},
                {0f, 0f, -1f, 0f},
                {0f, 1f, 0f, 0f},
                {0f, 0f, 0f, 1f},
            };
            mat.AssertValues(expectedValue);
        }
        
        [Test]
        public void SetRotationAngleAxis() {
            //throw new NotImplementedException();
        }
        
        [Test]
        public void SetRotationEuler() {
            var mat = new Matrix4X4();
            mat.SetRotation(90f, 0f, 0f);
            var expectedValue = new[,] {
                {1f, 0f, 0f, 0f},
                {0f, 0f, -1f, 0f},
                {0f, 1f, 0f, 0f},
                {0f, 0f, 0f, 1f},
            };
            mat.AssertValues(expectedValue);
            mat = new Matrix4X4();
            mat.SetRotation(0f, 90f, 0f);
            expectedValue = new[,] {
                {0f, 0f, 1f, 0f},
                {0f, 1f, 0f, 0f},
                {-1f, 0f, 0f, 0f},
                {0f, 0f, 0f, 1f}, 
            };
            mat.AssertValues(expectedValue);
            mat = new Matrix4X4();
            mat.SetRotation(0f, 0f, 90f);
            expectedValue = new[,] {
                {0f, -1f, 0f, 0f},
                {1f, 0f, 0f, 0f},
                {0f, 0f, 1f, 0f},
                {0f, 0f, 0f, 1f}, 
            };
            mat.AssertValues(expectedValue);
            mat = new Matrix4X4();
            mat.SetRotation(0f,90f,90f);
            expectedValue = new[,] {
                {0f, 0f, 1f, 0f},
                {1f, 0f, 0f, 0f},
                {0f, 1f, 0f, 0f},
                {0f, 0f, 0f, 1f}, 
            };
            mat.AssertValues(expectedValue);
            mat = new Matrix4X4();
            mat.SetRotation(0f,90f,-90f);
            expectedValue = new[,] {
                {0f, 0f, 1f, 0f},
                {-1f, 0f, 0f, 0f},
                {0f, -1f, 0f, 0f},
                {0f, 0f, 0f, 1f}, 
            };
            mat.AssertValues(expectedValue);
        }

        [Test]
        public void SetTranslation() {
            var mat = new Matrix4X4();
            var vertex = new Vector3(10, 10, 10);
            var translationOffset = new Vector3(3, 4, 5);
            
            mat.SetTranslation(translationOffset);
            
            var result = mat.Transform(vertex);
            result.AssertValues(13, 14, 15);
        }

        [Test]
        public void SetScale() {
            var mat = new Matrix4X4();
            var vertex = new Vector3(10, 10, 10);
            var scale = new Vector3(3, 4, 5);
            
            mat.SetScale(scale);
            
            var result = mat.Transform(vertex);
            result.AssertValues(30, 40, 50);
        }
    }

    public static class Matrix4X4TestExtension {
        public static void AssertValues(this Matrix4X4 mat, float x0, float y0, float z0, float w0, float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float x3, float y3, float z3, float w3) {
            Assert.That(mat[0, 0], Is.EqualTo(x0).Within(0.0000001));
            Assert.That(mat[0, 1], Is.EqualTo(y0).Within(0.0000001));
            Assert.That(mat[0, 2], Is.EqualTo(z0).Within(0.0000001));
            Assert.That(mat[0, 3], Is.EqualTo(w0).Within(0.0000001));
            
            Assert.That(mat[1, 0], Is.EqualTo(x1).Within(0.0000001));
            Assert.That(mat[1, 1], Is.EqualTo(y1).Within(0.0000001));
            Assert.That(mat[1, 2], Is.EqualTo(z1).Within(0.0000001));
            Assert.That(mat[1, 3], Is.EqualTo(w1).Within(0.0000001));
            
            Assert.That(mat[2, 0], Is.EqualTo(x2).Within(0.0000001));
            Assert.That(mat[2, 1], Is.EqualTo(y2).Within(0.0000001));
            Assert.That(mat[2, 2], Is.EqualTo(z2).Within(0.0000001));
            Assert.That(mat[2, 3], Is.EqualTo(w2).Within(0.0000001));
            
            Assert.That(mat[3, 0], Is.EqualTo(x3).Within(0.0000001));
            Assert.That(mat[3, 1], Is.EqualTo(y3).Within(0.0000001));
            Assert.That(mat[3, 2], Is.EqualTo(z3).Within(0.0000001));
            Assert.That(mat[3, 3], Is.EqualTo(w3).Within(0.0000001));
        }

        public static void AssertValues(this Matrix4X4 mat, float[,] val) {
            mat.AssertValues(val[0, 0], val[0, 1], val[0, 2], val[0, 3], val[1, 0], val[1, 1], val[1, 2], val[1, 3], val[2, 0], val[2, 1], val[2, 2], val[2, 3], val[3, 0], val[3, 1], val[3, 2], val[3, 3]);
        }
    }

    public static class Vector4TestExtension {
        public static void AssertValues(this Vector4 vec, float x, float y, float z, float w) {
            Assert.That(vec.x, Is.EqualTo(x).Within(1).Percent);
            Assert.That(vec.y, Is.EqualTo(y).Within(1).Percent);
            Assert.That(vec.z, Is.EqualTo(z).Within(1).Percent);
            Assert.That(vec.w, Is.EqualTo(w).Within(1).Percent);
        }
    }
}