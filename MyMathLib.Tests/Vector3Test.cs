using NUnit.Framework;

namespace MyMathLib.Tests {
    [TestFixture]
    public class Vector3Test {
        [Test]
        public void Creation() {
            var vec = new Vector3(0, 0, 0);
            vec.AssertValues(0, 0, 0);
            
            vec = new Vector3();
            vec.AssertValues(0, 0, 0);
        }

        [Test]
        public void GetComponentsIndexer() {
            var vec = new Vector3(0, 0, 0);
            vec.AssertValuesIndexer(0,0,0);
        }

        [Test]
        public void SetComponentsIndexer() {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var vec = new Vector3(0, 0, 0);
            
            vec[0] = 5;
            vec[1] = 4;
            vec[2] = 3;
            
            vec.AssertValuesIndexer(5,4,3);
        }

        [Test]
        public void SetComponents() {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var vec = new Vector3();
            
            vec.x = 1;
            vec.y = 2;
            vec.z = 3;

            vec.AssertValues(1, 2, 3);
        }

        [Test]
        public void Add() {
            var addVec1 = new Vector3(1, 0, 3);
            var addVec2 = new Vector3(-1, 4, 2);
            var vec = addVec1 + addVec2;
            vec.AssertValues(0, 4, 5);
        }

        [Test]
        public void Subtract() {
            var subVec1 = new Vector3(1, 0, 3);
            var subVec2 = new Vector3(-1, 4, 2);
            var vec = subVec1 - subVec2;
            vec.AssertValues(2, -4, 1);
        }

        [Test]
        public void ScalarMultiplication() {
            var mulVec = new Vector3(-1, 4, 2);
            var vec = 5 * mulVec;
            vec.AssertValues(-5, 20, 10);
        }

        [Test]
        public void DotProduct() {
            var dotVec1 = new Vector3(1, 0, 3);
            var dotVec2 = new Vector3(-1, 4, 2);
            var res = Vector3.Dot(dotVec1, dotVec2);
            Assert.That(res, Is.EqualTo(5).Within(0.00001).Percent);
        }

        [Test]
        public void CrossProduct() {
            var croVec1 = new Vector3(1, 2, 3);
            var croVec2 = new Vector3(1, 5, 7);
            var vec = Vector3.Cross(croVec1, croVec2);
            vec.AssertValues(-1, -4, 3);
        }

        [Test]
        public void Length() {
            var vec = new Vector3(2, 4, 4);
            var res = vec.Length;
            Assert.That(res, Is.EqualTo(6).Within(0.00001).Percent);
        }

        [Test]
        public void Normalize() {
            var unoVec = new Vector3(3, 1, 2);
            var vec = unoVec.Normalized;
            vec.AssertValues(0.801783681f, 0.267261237f, 0.534522474f);
        }
    }

    public static class VectorTestExtension {
        public static void AssertValues(this Vector3 vec, float x, float y, float z) {
            Assert.That(vec.x, Is.EqualTo(x).Within(0.00001));
            Assert.That(vec.y, Is.EqualTo(y).Within(0.00001));
            Assert.That(vec.z, Is.EqualTo(z).Within(0.00001));
        }
        
        public static void AssertValuesIndexer(this Vector3 vec, float x, float y, float z) {
            Assert.That(vec[0], Is.EqualTo(x).Within(0.00001));
            Assert.That(vec[1], Is.EqualTo(y).Within(0.00001));
            Assert.That(vec[2], Is.EqualTo(z).Within(0.00001));
        }
    }
}