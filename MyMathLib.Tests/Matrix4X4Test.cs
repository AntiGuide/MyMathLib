using System;
using NUnit.Framework;

namespace MyMathLib.Tests {
    [TestFixture]
    public class Matrix4X4Test {
        private Quaternion quaternion;

        [Test]
        public void SetRotationQuaternion() {
            var qat = new Quaternion(0.7071f, 0f, 0f, 0.7071f);
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

        #region SetQuaternionDebug
        [Test]
        public void SetRotationEuler1() {
            var mat = new Matrix4X4();
            mat.SetRotation(33.3f, 66.6f, 99.9f);
            Assert.Warn(System.Environment.NewLine + mat);
            var expectedValue = new[,] {
                {0.4280839562f, -0.4778637588f, 0.7670660615f, 0f}, 
                {0.8233616352f, -0.1436996460f, -0.5490228534f, 0f}, 
                {0.3725852370f, 0.8666006923f, 0.3319391012f, 0f}, 
                {0f, 0f, 0f, 1f}, 
            };
            mat.AssertValues(expectedValue);
        }
        
        [Test]
        public void SetRotationEuler2() {
            var mat = new Matrix4X4();
            mat.SetRotation(90f, 0f, 0f);
            var expectedValue = new[,] {
                {1f, 0f, 0f, 0f},
                {0f, 0f, -1f, 0f},
                {0f, 1f, 0f, 0f},
                {0f, 0f, 0f, 1f},
            };
            mat.AssertValues(expectedValue);
        }
        
        [Test]
        public void SetRotationEuler3() {
            var mat = new Matrix4X4();
            mat.SetRotation(0f, 90f, 0f);
            //Assert.Warn(System.Environment.NewLine + mat);
            var expectedValue = new[,] {
                {0f, 0f, 1f, 0f},
                {0f, 1f, 0f, 0f},
                {-1f, 0f, 0f, 0f},
                {0f, 0f, 0f, 1f}, 
            };
            mat.AssertValues(expectedValue);
        }
        
        [Test]
        public void SetRotationEuler4() {
            var mat = new Matrix4X4();
            mat.SetRotation(0f, 0f, 90f);
            //Assert.Warn(System.Environment.NewLine + mat);
            var expectedValue = new[,] {
                {0f, -1f, 0f, 0f},
                {1f, 0f, 0f, 0f},
                {0f, 0f, 1f, 0f},
                {0f, 0f, 0f, 1f}, 
            };
            mat.AssertValues(expectedValue);
        }
        
        [Test]
        public void SetRotationEuler5() {
            var mat = new Matrix4X4();
            //mat.SetRotation(90f, 0f, 90f);
            quaternion = new Quaternion(0f,90f,90f);
            mat.SetRotation(quaternion);
            Assert.Warn(System.Environment.NewLine + quaternion);
            Assert.Warn(System.Environment.NewLine + mat);
            var expectedValue = new[,] {
                {0f, 0f, 1f, 0f},
                {1f, 0f, 0f, 0f},
                {0f, 1f, 0f, 0f},
                {0f, 0f, 0f, 1f}, 
            };
            mat.AssertValues(expectedValue);
        }
        
        [Test]
        public void SetRotationEuler6() {
            var mat = new Matrix4X4();
            var qat = new Quaternion(-45f, -30f, -70f);
            mat.SetRotation(qat);
            Assert.Warn(System.Environment.NewLine + qat);
            Assert.Warn(System.Environment.NewLine + mat);
            var expectedValue = new[,] {
                {0.2961981f, -0.8137977f,  0.5000000f, 0f},
                {0.7853854f, -0.0903867f, -0.6123725f, 0f},
                {0.5435407f,  0.5740763f,  0.6123725f, 0f},
                {0f, 0f, 0f, 1f},
            };
//            var expectedValue = new[,] {
//                {0.6284296513f, -0.6928753853f, 0.3535533845f, 0f},
//                {0.6644630432f, 0.2418446541f, -0.7071068287f, 0f},
//                {0.4044318497f, 0.6792900562f, 0.6123723984f, 0f},
//                {0f, 0f, 0f, 1f}, 
//            };
            mat.AssertValues(expectedValue);
        }
        
        [Test]
        public void SetRotationEuler7() {
            var mat = new Matrix4X4();
            //mat.SetRotation(90f, 0f, 90f);
            quaternion = new Quaternion(0f,90f,-90f);
            mat.SetRotation(quaternion);
            Assert.Warn(System.Environment.NewLine + quaternion);
            Assert.Warn(System.Environment.NewLine + mat);
            var expectedValue = new[,] {
                {0f, 0f, 1f, 0f},
                {-1f, 0f, 0f, 0f},
                {0f, -1f, 0f, 0f},
                {0f, 0f, 0f, 1f}, 
            };
            mat.AssertValues(expectedValue);
        }
        #endregion
        
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