using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathLib {
    class Vector3 {
        double[] vector;

        public Vector3() {
            this.vector = new double[3];
        }

        public Vector3(double[] vector) {
            if (vector.Length != 3) {
                throw new ArgumentOutOfRangeException(nameof(vector), "Array Length has to be 3");
            }

            this.vector = vector;
        }

        public Vector3(double x, double y, double z) {
            this.vector = new double[3] { x, y, z };
        }
        
        public double this[int i] {
            get { return vector[i]; }
            set { vector[i] = value; }
        }

        public static Vector3 operator +(Vector3 a, Vector3 b) {
            var ret = new Vector3();
            for (int i = 0; i < 3; i++) {
                ret[i] = a[i] + b[i];
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
            for (int i = 0; i < 3; i++) {
                ret += a[i] * b[i];
            }

            return ret;
        }

        override public string ToString() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("|\t" + vector[0] + "\t|");
            stringBuilder.AppendLine("|\t" + vector[1] + "\t|");
            stringBuilder.AppendLine("|\t" + vector[2] + "\t|");
            return stringBuilder.ToString();
        }
    }
}
