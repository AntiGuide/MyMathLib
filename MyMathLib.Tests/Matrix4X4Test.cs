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
            
            
            mat = new Matrix4X4();
            mat.SetRotation(0f, 0f, 90f);
            var expectedResultRotationOnlyMatrix = new[,] {
                {0f, -1f, 0f, 0f}, 
                {1f,  0f, 0f, 0f}, 
                {0f,  0f, 1f, 0f}, 
                {0f,  0f, 0f, 1f}, 
            };
            mat.AssertValues(expectedResultRotationOnlyMatrix);
        }
        
        [Test]
        public void SetRotationAngleAxis() {
            var mat = new Matrix4X4();
            mat.SetRotation(90f, new Vector3(1,0,0));
            var expectedValue = new[,] {
                {1f, 0f, 0f, 0f},
                {0f, 0f, -1f, 0f},
                {0f, 1f, 0f, 0f},
                {0f, 0f, 0f, 1f},
            };
            mat.AssertValues(expectedValue);
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
            
            mat = new Matrix4X4();
            mat.SetTranslation(new Vector3(20f, 0f, 0f));
            var expectedResultTranslationOnlyMatrix = new[,] {
                {1f, 0f, 0f, 20f}, 
                {0f, 1f, 0f,  0f}, 
                {0f, 0f, 1f,  0f}, 
                {0f, 0f, 0f,  1f}, 
            };
            mat.AssertValues(expectedResultTranslationOnlyMatrix);
        }

        [Test]
        public void SetScale() {
            var mat = new Matrix4X4();
            var vertex = new Vector3(10, 10, 10);
            var scale = new Vector3(3, 4, 5);
            mat.SetScale(scale);
            var result = mat.Transform(vertex);
            result.AssertValues(30, 40, 50);
            
            mat = new Matrix4X4();
            mat.SetScale(new Vector3(3f, 3f, 3f));
            var expectedResultScaleOnlyMatrix = new[,] {
                {3f, 0f, 0f, 0f}, 
                {0f, 3f, 0f, 0f}, 
                {0f, 0f, 3f, 0f}, 
                {0f, 0f, 0f, 1f}, 
            };
            mat.AssertValues(expectedResultScaleOnlyMatrix);
        }
        
        [Test]
        public void CompositingMatrix() {
            var mat = new Matrix4X4();
            mat.SetRotation(0f, 0f, 90f);
            mat.SetScale(new Vector3(3f, 3f, 3f));
            mat.SetTranslation(new Vector3(20f, 0f, 0f));
            var expectedResultMatrix = new[,] {
                {0f, -3f, 0f, 20f},
                {3f, 0f, 0f, 0f},
                {0f, 0f, 3f, 0f},
                {0f, 0f, 0f, 1f},
            };
            mat.AssertValues(expectedResultMatrix);
        }
        
        [Test]
        public void TransformingPoint() {
            var mat = new Matrix4X4();
            mat.SetScale(new Vector3(3f, 3f, 3f));
            mat.SetTranslation(new Vector3(20f, 0f, 0f));
            mat.SetRotation(0f, 0f, 90f);
            var point = new Vector3(0, 10, 0);
            point = mat.Transform(point);
            point.AssertValues(-10, 0, 0);
        }
    }

    public static class Matrix4X4TestExtension {
        private static void AssertValues(this Matrix4X4 mat, float x0, float y0, float z0, float w0, float x1, float y1, float z1, float w1, float x2, float y2, float z2, float w2, float x3, float y3, float z3, float w3) {
            Assert.That(mat[0, 0], Is.EqualTo(x0).Within(0.000001));
            Assert.That(mat[0, 1], Is.EqualTo(y0).Within(0.000001));
            Assert.That(mat[0, 2], Is.EqualTo(z0).Within(0.000001));
            Assert.That(mat[0, 3], Is.EqualTo(w0).Within(0.000001));
            
            Assert.That(mat[1, 0], Is.EqualTo(x1).Within(0.000001));
            Assert.That(mat[1, 1], Is.EqualTo(y1).Within(0.000001));
            Assert.That(mat[1, 2], Is.EqualTo(z1).Within(0.000001));
            Assert.That(mat[1, 3], Is.EqualTo(w1).Within(0.000001));
            
            Assert.That(mat[2, 0], Is.EqualTo(x2).Within(0.000001));
            Assert.That(mat[2, 1], Is.EqualTo(y2).Within(0.000001));
            Assert.That(mat[2, 2], Is.EqualTo(z2).Within(0.000001));
            Assert.That(mat[2, 3], Is.EqualTo(w2).Within(0.000001));
            
            Assert.That(mat[3, 0], Is.EqualTo(x3).Within(0.000001));
            Assert.That(mat[3, 1], Is.EqualTo(y3).Within(0.000001));
            Assert.That(mat[3, 2], Is.EqualTo(z3).Within(0.000001));
            Assert.That(mat[3, 3], Is.EqualTo(w3).Within(0.000001));
        }

        public static void AssertValues(this Matrix4X4 mat, float[,] val) {
            mat.AssertValues(val[0, 0], val[0, 1], val[0, 2], val[0, 3], val[1, 0], val[1, 1], val[1, 2], val[1, 3], val[2, 0], val[2, 1], val[2, 2], val[2, 3], val[3, 0], val[3, 1], val[3, 2], val[3, 3]);
        }
    }
}