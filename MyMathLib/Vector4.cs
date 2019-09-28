using System;
using System.Text;

namespace MyMathLib {
    /// <summary>A standard vector class using float components</summary>
    public class Vector4 {
        /// <summary>This vectors components</summary>
        private readonly float[] vector;

        /// <summary>Construct a vector initialized with (0,0,0,0)</summary>
        public Vector4() {
            this.vector = new float[4];
        }

        /// <summary>
        /// Construct a vector with 4 components
        /// </summary>
        public Vector4(float x, float y, float z, float w) {
            this.vector = new[] { x, y, z, w };
        }

        /// <summary>
        /// Construct a Vector4 from a Vector3 and the w component
        /// </summary>
        public Vector4(Vector3 vec, float w) {
            this.vector = new[] { vec.x, vec.y, vec.z, w };
        }

        /// <summary>
        /// Access the vectors components trough this indexer
        /// </summary>
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

        /// <summary>Access the vectors w component</summary>
        public float w {
            get => vector[3];
            set => vector[3] = value;
        }

        /// <summary>Calculate the length of this vector</summary>
        public float Length => (float)Math.Sqrt(Math.Pow(x,2) + Math.Pow(y,2) + Math.Pow(z,2) + Math.Pow(w,2));
        
        /// <summary>Get this vector normalized (returns a copy)</summary>
        public Vector4 Normalized => this / this.Length;

        public static Vector4 operator +(Vector4 a, Vector4 b) {
            var ret = new Vector4();
            for (var i = 0; i < 4; i++) {
                ret[i] = a[i] + b[i];
            }

            return ret;
        }

        public static Vector4 operator *(float a, Vector4 b) {
            var ret = new Vector4();
            for (var i = 0; i < 4; i++) {
                ret[i] = a * b[i];
            }

            return ret;
        }

        public static Vector4 operator *(Vector4 a, float b) {
            return b * a;
        }

        public static Vector4 operator /(Vector4 a, float b) {
            var ret = new Vector4();
            for (var i = 0; i < 4; i++) {
                ret[i] = a[i] / b;
            }
            
            return ret;
        }

        public static Vector4 operator -(Vector4 a, Vector4 b) {
            return a + !b;
        }

        public static Vector4 operator !(Vector4 a) {
            var ret = new Vector4();
            for (int i = 0; i < 4; i++) {
                ret[i] = -a[i];
            }

            return ret;
        }

        /// <summary>
        /// Calculates the dot product of two vectors
        /// </summary>
        public static double Dot(Vector4 a, Vector4 b) {
            var ret = 0d;
            for (var i = 0; i < 4; i++) {
                ret += a[i] * b[i];
            }

            return ret;
        }

        public override string ToString() {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("|\t" + Math.Round(vector[0],2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(vector[1],2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(vector[2],2) + "\t|");
            stringBuilder.AppendLine("|\t" + Math.Round(vector[3],2) + "\t|");
            return stringBuilder.ToString();
        }
    }
}
