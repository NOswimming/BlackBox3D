using System;

namespace BlackBoxPlus
{
    public class VectorInt3
    {
        public int x;
        public int y;
        public int z;

        public VectorInt3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static VectorInt3 CrossProduct(VectorInt3 a, VectorInt3 b)
        {
            var x = a.y* b.z - b.y * a.z;
            var y = (a.x * b.z - b.x * a.z) * -1;
            var z = a.x * b.y - b.x * a.y;
            return new VectorInt3(x, y, z);
        }

        public static readonly VectorInt3 UP = new VectorInt3(0, 1, 0);
        public static readonly VectorInt3 DOWN = new VectorInt3(0, -1, 0);
        public static readonly VectorInt3 LEFT = new VectorInt3(-1, 0, 0);
        public static readonly VectorInt3 RIGHT = new VectorInt3(1, 0, 0);
        public static readonly VectorInt3 FORWARD = new VectorInt3(0, 0, 1);
        public static readonly VectorInt3 BACKWARD = new VectorInt3(0, 0, -1);

        public static readonly VectorInt3 ZERO = new VectorInt3(0, 0, 0);


        public static VectorInt3 operator +(VectorInt3 a, VectorInt3 b)
        {
            return new VectorInt3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static VectorInt3 operator -(VectorInt3 a, VectorInt3 b)
        {
            return new VectorInt3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static VectorInt3 operator *(VectorInt3 v, int i)
        {
            return new VectorInt3(v.x * i, v.y * i, v.z * i);
        }

        public static VectorInt3 operator *(int i, VectorInt3 v)
        {
            return new VectorInt3(v.x * i, v.y * i, v.z * i);
        }

        public static bool operator ==(VectorInt3 lhs, VectorInt3 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
        }

        public static bool operator !=(VectorInt3 lhs, VectorInt3 rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
        }

        public override bool Equals(object obj)
        {
            if (obj is VectorInt3)
            {
                var v = (VectorInt3)obj;
                return this == v;
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("[{0},{1},{2}]", x, y, z);
        }

    }

}
