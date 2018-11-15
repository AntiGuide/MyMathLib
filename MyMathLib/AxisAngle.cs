using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMathLib {
    class AxisAngle {
        public static Matrix4X4 GetRotationMatrix(double a, Vector3 v) {
            var mat = new double[4, 4];
            a *= (Math.PI / 180);
            var cosa = Math.Cos(a);
            var sina = Math.Sin(a);
            var invCosa = 1 - cosa;

            mat[0, 0] = invCosa * Math.Pow(v.x, 2) + cosa;
            mat[0, 1] = invCosa * v.x * v.y + v.z * sina;
            mat[0, 2] = invCosa * v.x * v.z + v.y * sina;
            mat[0, 3] = 0;

            mat[1, 0] = mat[0, 1];
            mat[1, 1] = invCosa * Math.Pow(v.y, 2) + cosa;
            mat[1, 2] = invCosa * v.y * v.z - v.x * sina;
            mat[1, 3] = 0;

            mat[2, 0] = invCosa * v.x * v.z - v.y * sina;
            mat[2, 1] = invCosa * v.y * v.z + v.x * sina;
            mat[2, 2] = invCosa * Math.Pow(v.z, 2) + cosa;
            mat[2, 3] = 0;

            mat[3, 0] = 0;
            mat[3, 1] = 0;
            mat[3, 2] = 0;
            mat[3, 3] = 1;

            return new Matrix4X4(mat);

        }
    }
}
