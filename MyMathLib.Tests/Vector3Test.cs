using NUnit.Framework;

namespace MyMathLib.Tests {
    [TestFixture]
    public class Vector3Test {
        private Vector3[] vectors;

        [SetUp]
        protected void SetUp() {
            vectors = new[] {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 3),
                new Vector3(-1, 4, 2),
                new Vector3(1, 2, 3),
                new Vector3(1, 5, 7),
                new Vector3(2, 4, 4),
                new Vector3(3, 1, 2),
            };
        }

        [Test]
        public void Creation() {
            vectors[0].AssertValues(0, 0, 0);
        }

        [Test]
        public void GetComponentsIndexer() {
            Assert.That(vectors[0][0], Is.EqualTo(0).Within(1).Percent);
            Assert.That(vectors[0][1], Is.EqualTo(0).Within(1).Percent);
            Assert.That(vectors[0][2], Is.EqualTo(0).Within(1).Percent);
        }

        [Test]
        public void SetComponentsIndexer() {
            vectors[0][0] = 5;
            vectors[0][1] = 4;
            vectors[0][2] = 3;
            
            Assert.That(vectors[0][0], Is.EqualTo(5).Within(1).Percent);
            Assert.That(vectors[0][1], Is.EqualTo(4).Within(1).Percent);
            Assert.That(vectors[0][2], Is.EqualTo(3).Within(1).Percent);
        }

        [Test]
        public void SetComponents() {
            vectors[0].x = 1;
            vectors[0].y = 2;
            vectors[0].z = 3;

            vectors[0].AssertValues(1, 2, 3);
        }

        [Test]
        public void Add() {
            var vec = vectors[1] + vectors[2];
            vec.AssertValues(0, 4, 5);
        }

        [Test]
        public void Subtract() {
            var vec = vectors[1] - vectors[2];
            vec.AssertValues(2, -4, 1);
        }

        [Test]
        public void ScalarMultiplication() {
            var vec = 5 * vectors[2];
            vec.AssertValues(-5, 20, 10);
        }

        [Test]
        public void DotProduct() {
            var res = Vector3.Dot(vectors[1], vectors[2]);
            Assert.That(res, Is.EqualTo(5).Within(1).Percent);
        }

        [Test]
        public void CrossProduct() {
            var vec = Vector3.Cross(vectors[3], vectors[4]);
            vec.AssertValues(-1, -4, 3);
        }

        [Test]
        public void Length() {
            var res = vectors[5].Length;
            Assert.That(res, Is.EqualTo(6).Within(1).Percent);
        }

        [Test]
        public void Normalize() {
            var vec = vectors[6].Normalized;
            vec.AssertValues(0.801783681f, 0.267261237f, 0.534522474f);
        }
    }

    public static class VectorTestExtension {
        public static void AssertValues(this Vector3 vec, float x, float y, float z) {
            Assert.That(vec.x, Is.EqualTo(x).Within(0.00001));
            Assert.That(vec.y, Is.EqualTo(y).Within(0.00001));
            Assert.That(vec.z, Is.EqualTo(z).Within(0.00001));
        }
    }
}