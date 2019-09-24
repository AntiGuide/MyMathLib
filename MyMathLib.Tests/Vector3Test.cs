using NUnit.Framework;

namespace MyMathLib.Tests
{
    [TestFixture]
    public class Vector3Test {
        private Vector3[] vectors;
    
        [SetUp]
        protected void SetUp() {
            vectors = new[] {
                new Vector3(0d, 0d, 0d),
            };
        }

        [Test]
        public void Creation() {
            Assert.AreEqual(vectors[0].x, 0d);
            Assert.AreEqual(vectors[0].y, 0d);
            Assert.AreEqual(vectors[0].z, 0d);
        }
    }
}
