using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quaternion {
    class Quaternion {
        public double x, y, z, w;

        public Quaternion(double x, double y, double z, double w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Quaternion operator+(Quaternion a, Quaternion b) {
            var x = a.x + b.x;
            var y = a.y + b.y;
            var z = a.z + b.z;
            var w = a.w + b.w;

            return new Quaternion(x, y, z, w);
        }

        public static Quaternion operator*(Quaternion a, Quaternion b) {
            var x = a.x * b.x - a.y * b.y - a.z * b.z - a.w * b.w;
            var y = a.x * b.y + a.y * b.x + a.z * b.w - a.w * b.z;
            var z = a.x * b.z - a.y * b.w + a.z * b.x + a.w * b.y;
            var w = a.x * b.w + a.y * b.z - a.z * b.y + a.w * b.x;

            return new Quaternion(x, y, z, w);
        }

        public static Quaternion operator !(Quaternion a) {
            return new Quaternion(-a.x, -a.y, -a.z, -a.w);
        }

        public static Quaternion operator-(Quaternion a, Quaternion b) {
            return a + !b;
        }

        public static bool operator==(Quaternion a, Quaternion b) {
            return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
        }

        public static bool operator !=(Quaternion a, Quaternion b) {
            return a.x != b.x || a.y != b.y || a.z != b.z || a.w != b.w;
        }

        override public string ToString() {
            return x + "+" + y + "i+" + z + "j+" + w + "k";
        }
    }
}
