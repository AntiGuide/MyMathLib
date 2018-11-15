using System;

namespace MyMathLib {
    public class Quaternion {
        public double x, y, z, w;

        public Quaternion(double x, double y, double z, double w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Quaternion operator +(Quaternion a, Quaternion b) {
            var x = a.x + b.x;
            var y = a.y + b.y;
            var z = a.z + b.z;
            var w = a.w + b.w;

            return new Quaternion(x, y, z, w);
        }

        public static Quaternion operator *(Quaternion a, Quaternion b) {
            var x = a.x * b.x - a.y * b.y - a.z * b.z - a.w * b.w;
            var y = a.x * b.y + a.y * b.x + a.z * b.w - a.w * b.z;
            var z = a.x * b.z - a.y * b.w + a.z * b.x + a.w * b.y;
            var w = a.x * b.w + a.y * b.z - a.z * b.y + a.w * b.x;

            return new Quaternion(x, y, z, w);
        }

        public static Quaternion operator /(Quaternion a, Quaternion b) {
            var bGes2 = Math.Pow(b.x, 2) + Math.Pow(b.y, 2) + Math.Pow(b.z, 2) + Math.Pow(b.w, 2);

            var x = a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
            var y = a.y * b.x - a.x * b.y - a.w * b.z + a.z * b.w;
            var z = a.z * b.x + a.w * b.y - a.x * b.z - a.y * b.w;
            var w = a.w * b.x - a.z * b.y + a.y * b.z - a.x * b.w;

            return new Quaternion(x, y, z, w) / bGes2;
        }

        public static Quaternion operator /(Quaternion a, double b) {
            return new Quaternion(a.x / b, a.y / b, a.z / b, a.w / b);
        }

        public double Abs() {
            return Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2) + Math.Pow(this.z, 2) + Math.Pow(this.w, 2));
        }

        public Quaternion Inverse() {
            var thisGes2 = Math.Pow(this.x, 2) + Math.Pow(this.y, 2) + Math.Pow(this.z, 2) + Math.Pow(this.w, 2);
            return Conjugate(this) / thisGes2;
        }

        public static Quaternion Conjugate(Quaternion a) {
            return new Quaternion(a.x, -a.y, -a.z, -a.w);
        }

        public Quaternion Normalize() {
            return this / this.Abs();
        }

        public static Quaternion operator !(Quaternion a) {
            return new Quaternion(-a.x, -a.y, -a.z, -a.w);
        }

        public static Quaternion operator -(Quaternion a, Quaternion b) {
            return a + !b;
        }

        public static bool operator ==(Quaternion a, Quaternion b) {
            return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
        }

        public static bool operator !=(Quaternion a, Quaternion b) {
            return a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w;
        }

        override public string ToString() {
            return Math.Round(x,2) + "+" + Math.Round(y, 2) + "i+" + Math.Round(z, 2) + "j+" + Math.Round(w, 2) + "k";
        }

        public override bool Equals(object obj) {
            var item = obj as Quaternion;

            if (item == null) {
                return false;
            }

            return this == item;
        }

        public override int GetHashCode() {
            return this.GetHashCode();
        }
    }
}
