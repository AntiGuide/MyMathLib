using System;
using System.Text;

namespace MyMathLib {
    public class Vector3 {
        private readonly float[] vector;

        public Vector3() {
            this.vector = new float[3];
        }

        public Vector3(float x, float y, float z) {
            this.vector = new[] { x, y, z };
        }

        public float this[int i] {
            get => vector[i];
            set => vector[i] = value;
        }

        public float x {
            get => vector[0];
            set => vector[0] = value;
        }

        public float y {
            get => vector[1];
            set => vector[1] = value;
        }

        public float z {
            get => vector[2];
            set => vector[2] = value;
        }

        public float Length => (float)Math.Sqrt(Math.Pow(x,2) + Math.Pow(y,2) + Math.Pow(z,2));
        
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

        public static double Dot(Vector3 a, Vector3 b) {
            var ret = 0d;
            for (var i = 0; i < 3; i++) {
                ret += a[i] * b[i];
            }

            return ret;
        }

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
