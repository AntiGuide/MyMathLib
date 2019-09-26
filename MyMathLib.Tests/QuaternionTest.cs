using NUnit.Framework;

namespace MyMathLib.Tests {
    [TestFixture]
    public class QuaternionTest {
        [Test]
        public void CreationAngleAxis() {
            var qat = new Quaternion(45f, new Vector3(1, 0, 1));
            qat.AssertValues(0.9238795f, 0.2705981f, 0f, 0.2705981f);
            
            qat = new Quaternion(90f, new Vector3(1, 0, 0));
            qat.AssertValues(0.707106769f, 0.707106769f, 0f, 0f);
        }

        [Test]
        public void CreationEuler() {
            var qat = new Quaternion(90,90,0);
            qat.AssertValues(0.5f, 0.5f, 0.5f, -0.5f);
            
            qat = new Quaternion(90,0,0);
            qat.AssertValues(0.7071067811865476f, 0.7071067811865476f, 0f, 0f);
            
            qat = new Quaternion(0,90,0);
            qat.AssertValues(0.7071067811865476f, 0f, 0.7071067811865476f, 0f);
            
            qat = new Quaternion(0,0,90);
            qat.AssertValues(0.7071067811865476f, 0f, 0f, 0.7071067811865476f);
            
            qat = new Quaternion(33.3, 66.6, 99.9);
            qat.AssertValues(0.6356735f, 0.5567415f, 0.1551428f, 0.5117506f);
            
            qat = new Quaternion (40,30,20);
            qat.AssertValues(0.9092553f, 0.3675801f, 0.1821479f, 0.07043934f);
        }
    }

    public static class QuaternionTestExtension {
        public static void AssertValues(this Quaternion qat, float w, float x, float y, float z) {
            Assert.That(qat.x, Is.EqualTo(x).Within(0.000001));
            Assert.That(qat.y, Is.EqualTo(y).Within(0.000001));
            Assert.That(qat.z, Is.EqualTo(z).Within(0.000001));
            Assert.That(qat.w, Is.EqualTo(w).Within(0.000001));
        }
    }
}