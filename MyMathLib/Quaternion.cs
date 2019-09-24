using System;

namespace MyMathLib {
    public class Quaternion {
        public readonly float x, y, z, w;

        public Quaternion(float x, float y, float z, float w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Quaternion(float a, Vector3 v) {
            a *= (float)Math.PI / 180;
            v *= (float)Math.Sin(a / 2);
            this.x = (float)Math.Cos(a/2);
            this.y = v.x;
            this.z = v.y;
            this.w = v.z;
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
            var bGes2 = (float)Math.Pow(b.x, 2) + (float)Math.Pow(b.y, 2) + (float)Math.Pow(b.z, 2) + (float)Math.Pow(b.w, 2);

            var x = a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
            var y = a.y * b.x - a.x * b.y - a.w * b.z + a.z * b.w;
            var z = a.z * b.x + a.w * b.y - a.x * b.z - a.y * b.w;
            var w = a.w * b.x - a.z * b.y + a.y * b.z - a.x * b.w;

            return new Quaternion(x, y, z, w) / bGes2;
        }

        public static Quaternion operator /(Quaternion a, float b) {
            return new Quaternion(a.x / b, a.y / b, a.z / b, a.w / b);
        }

        public float Abs() {
            return (float)Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2) + Math.Pow(this.z, 2) + Math.Pow(this.w, 2));
        }

        public Quaternion Inverse() {
            var thisGes2 = (float)Math.Pow(this.x, 2) + (float)Math.Pow(this.y, 2) + (float)Math.Pow(this.z, 2) + (float)Math.Pow(this.w, 2);
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
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;

            const double tolerance = 0.00001;
            return Math.Abs(a.x - b.x) < tolerance && Math.Abs(a.y - b.y) < tolerance && Math.Abs(a.z - b.z) < tolerance && Math.Abs(a.w - b.w) < tolerance;
        }

        public static bool operator !=(Quaternion a, Quaternion b) {
            if (a == null && b == null) return false;
            if (a == null || b == null) return true;
            
            const double tolerance = 0.00001;
            return Math.Abs(a.x - b.x) > tolerance || Math.Abs(a.y - b.y) > tolerance || Math.Abs(a.z - b.z) > tolerance || Math.Abs(a.w - b.w) > tolerance;
        }

        public override string ToString() {
            return Math.Round(x,2) + "+" + Math.Round(y, 2) + "i+" + Math.Round(z, 2) + "j+" + Math.Round(w, 2) + "k";
        }

        public override bool Equals(object obj) {
            var item = obj as Quaternion;
            if (item == null) return false;

            return this == item;
        }

        public override int GetHashCode() {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }
    }
}
