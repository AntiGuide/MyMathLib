using System;
using System.Text;

namespace MyMathLib {
    /// <summary>A standard vector class using float components</summary>
    public class Vector3 {
        /// <summary>This vectors components</summary>
        private readonly float[] vector;

        /// <summary>Construct a vector initialized with (0,0,0)</summary>
        public Vector3() {
            this.vector = new float[3];
        }

        /// <summary>
        /// Construct a vector with 3 components
        /// </summary>
        public Vector3(float x, float y, float z) {
            this.vector = new[] { x, y, z };
        }

        /// <summary>
        /// Access the vectors components trough this indexer
        /// </summary>
        /// <param name="i"></param>
        public float this[int i] {
            get => vector[i];
            set => vector[i] = value;
        }

        /// <summary>Access the vectors x component</summary>
        public float x {
            get => vector[0];
            set => vector[0] = value;
        }

        /// <summary>Access the vectors y component</summary>
        public float y {
            get => vector[1];
            set => vector[1] = value;
        }

        /// <summary>Access the vectors z component</summary>
        public float z {
            get => vector[2];
            set => vector[2] = value;
        }

        /// <summary>Calculate the length of this vector</summary>
        public float Length => (float)Math.Sqrt(Math.Pow(x,2) + Math.Pow(y,2) + Math.Pow(z,2));
        
        /// <summary>Get this vector normalized (returns a copy)</summary>
        public Vector3 Normalized => this / this.Length;

        public static Vector3 operator +(Vector3 a, Vector3 b) {
            var ret = new Vector3();
            for (var i = 0; i < 3; i++) {
                ret[i] = a[i] + b[i];
            }

            return ret;
        }

        public static Vector3 operator *(float a, Vector3 b) {
            var ret = new Vector3();
            for (var i = 0; i < 3; i++) {
                ret[i] = a * b[i];
            }

            return ret;
        }

        public static Vector3 operator *(Vector3 a, float b) {
            return b * a;
        }

        public static Vector3 operator /(Vector3 a, float b) {
            var ret = new Vector3();
            for (var i = 0; i < 3; i++) {
                ret[i] = a[i] / b;
            }
            
            return ret;
        }

        public static Vector3 operator -(Vector3 a, Vector3 b) {
            return a + !b;
        }

        public static Vector3 operator !(Vector3 a) {
            var ret = new Vector3();
            for (int i = 0; i < 3; i++) {
                ret[i] = -a[i];
            }

            return ret;
        }

        /// <summary>
        /// Calculates the dot product of two vectors
        /// </summary>
        public static float Dot(Vector3 a, Vector3 b) {
            var ret = 0f;
            for (var i = 0; i < 3; i++) {
                ret += a[i] * b[i];
            }

            return ret;
        }

        /// <summary>
        /// Caclulates the cross product of two vectors. (returns a copy)
        /// </summary>
        public static Vector3 Cross(Vector3 a, Vector3 b) {
            return new Vector3{
                x = a.y * b.z - a.z * b.y,
                y = a.z * b.x - a.x * b.z,
                z = a.x * b.y - a.y * b.x,
            };
        }

        public override string ToString() {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("|\t" + Math.Round(vector[0],2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(vector[1],2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(vector[2],2) + "\t|");
            return stringBuilder.ToString();
        }
    }
}
