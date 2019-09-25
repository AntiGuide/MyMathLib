using NUnit.Framework;

namespace MyMathLib.Tests {
    [TestFixture]
    public class QuaternionTest {
        [Test]
        public void CreationAngleAxis() {
            var axis = new Vector3(1, 0, 1);
            var qat = new Quaternion(45f, axis);
            qat.AssertValues(0.2705981f, 0f, 0.2705981f, 0.9238795f);

            axis = new Vector3(1, 0, 0);
            qat = new Quaternion(90f, axis);
            qat.AssertValues(0.7071f, 0f, 0f, 0.7071f);
        }

        [Test]
        public void CreationEuler() {
//            var qat = new Quaternion(0, 90, 0);
//            qat.AssertValues(0f, 0.7071f, 0f, 0.7071f);
            var qat = new Quaternion(0, 180, 90);
            qat.AssertValues(0.7071f, 0.7071f, -0.00000003090862f, -0.00000003090862f);
        }
    }

    public static class QuaternionTestExtension {
        public static void AssertValues(this Quaternion qat, float x, float y, float z, float w) {
            Assert.That(qat.x, Is.EqualTo(x).Within(1).Percent);
            Assert.That(qat.y, Is.EqualTo(y).Within(1).Percent);
            Assert.That(qat.z, Is.EqualTo(z).Within(1).Percent);
            Assert.That(qat.w, Is.EqualTo(w).Within(1).Percent);
        }
    }
}