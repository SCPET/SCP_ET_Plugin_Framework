using System;
using System.Globalization;

namespace SCP_ET.API.Classes
{
    public partial struct Vector : IEquatable<Vector>, IFormattable
    {
        public const float kEpsilon = 0.00001F;
        public const float kEpsilonNormalSqrt = 1e-15F;

        public float x;
        public float y;
        public float z;

        public static Vector LerpUnclamped(Vector a, Vector b, float t)
        {
            return new Vector(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t
            );
        }

        public static Vector MoveTowards(Vector current, Vector target, float maxDistanceDelta)
        {
            float toVector_x = target.x - current.x;
            float toVector_y = target.y - current.y;
            float toVector_z = target.z - current.z;

            float sqdist = toVector_x * toVector_x + toVector_y * toVector_y + toVector_z * toVector_z;

            if (sqdist == 0 || (maxDistanceDelta >= 0 && sqdist <= maxDistanceDelta * maxDistanceDelta))
                return target;
            var dist = (float)Math.Sqrt(sqdist);

            return new Vector(current.x + toVector_x / dist * maxDistanceDelta,
                current.y + toVector_y / dist * maxDistanceDelta,
                current.z + toVector_z / dist * maxDistanceDelta);
        }


        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector index!");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector index!");
                }
            }
        }

        public Vector(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
        public Vector(float x, float y) { this.x = x; this.y = y; z = 0F; }

        public void Set(float newX, float newY, float newZ) { x = newX; y = newY; z = newZ; }

        public static Vector Scale(Vector a, Vector b) { return new Vector(a.x * b.x, a.y * b.y, a.z * b.z); }

        public void Scale(Vector scale) { x *= scale.x; y *= scale.y; z *= scale.z; }

        public static Vector Cross(Vector lhs, Vector rhs)
        {
            return new Vector(
                lhs.y * rhs.z - lhs.z * rhs.y,
                lhs.z * rhs.x - lhs.x * rhs.z,
                lhs.x * rhs.y - lhs.y * rhs.x);
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector)) return false;

            return Equals((Vector)other);
        }

        public bool Equals(Vector other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public static Vector Reflect(Vector inDirection, Vector inNormal)
        {
            float factor = -2F * Dot(inNormal, inDirection);
            return new Vector(factor * inNormal.x + inDirection.x,
                factor * inNormal.y + inDirection.y,
                factor * inNormal.z + inDirection.z);
        }

        public static Vector Normalize(Vector value)
        {
            float mag = Magnitude(value);
            if (mag > kEpsilon)
                return value / mag;
            else
                return zero;
        }

        public void Normalize()
        {
            float mag = Magnitude(this);
            if (mag > kEpsilon)
                this = this / mag;
            else
                this = zero;
        }

        public Vector normalized { get { return Vector.Normalize(this); } }

        public static float Dot(Vector lhs, Vector rhs) { return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z; }

        public static float Distance(Vector a, Vector b)
        {
            float diff_x = a.x - b.x;
            float diff_y = a.y - b.y;
            float diff_z = a.z - b.z;
            return (float)Math.Sqrt(diff_x * diff_x + diff_y * diff_y + diff_z * diff_z);
        }

        public static Vector ClampMagnitude(Vector vector, float maxLength)
        {
            float sqrmag = vector.sqrMagnitude;
            if (sqrmag > maxLength * maxLength)
            {
                float mag = (float)Math.Sqrt(sqrmag);
                float normalized_x = vector.x / mag;
                float normalized_y = vector.y / mag;
                float normalized_z = vector.z / mag;
                return new Vector(normalized_x * maxLength,
                    normalized_y * maxLength,
                    normalized_z * maxLength);
            }
            return vector;
        }

        public static float Magnitude(Vector vector) { return (float)Math.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z); }

        public float magnitude { get { return (float)Math.Sqrt(x * x + y * y + z * z); } }

        public static float SqrMagnitude(Vector vector) { return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z; }

        public float sqrMagnitude { get { return x * x + y * y + z * z; } }

        static readonly Vector zeroVector = new Vector(0F, 0F, 0F);
        static readonly Vector oneVector = new Vector(1F, 1F, 1F);
        static readonly Vector upVector = new Vector(0F, 1F, 0F);
        static readonly Vector downVector = new Vector(0F, -1F, 0F);
        static readonly Vector leftVector = new Vector(-1F, 0F, 0F);
        static readonly Vector rightVector = new Vector(1F, 0F, 0F);
        static readonly Vector forwardVector = new Vector(0F, 0F, 1F);
        static readonly Vector backVector = new Vector(0F, 0F, -1F);
        static readonly Vector positiveInfinityVector = new Vector(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        static readonly Vector negativeInfinityVector = new Vector(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        public static Vector zero { get { return zeroVector; } }
        public static Vector one { get { return oneVector; } }
        public static Vector forward { get { return forwardVector; } }
        public static Vector back { get { return backVector; } }
        public static Vector down { get { return downVector; } }
        public static Vector left { get { return leftVector; } }
        public static Vector right { get { return rightVector; } }
        public static Vector positiveInfinity { get { return positiveInfinityVector; } }
        public static Vector negativeInfinity { get { return negativeInfinityVector; } }

        public static Vector operator +(Vector a, Vector b) { return new Vector(a.x + b.x, a.y + b.y, a.z + b.z); }
        public static Vector operator -(Vector a, Vector b) { return new Vector(a.x - b.x, a.y - b.y, a.z - b.z); }
        public static Vector operator -(Vector a) { return new Vector(-a.x, -a.y, -a.z); }
        public static Vector operator *(Vector a, float d) { return new Vector(a.x * d, a.y * d, a.z * d); }
        public static Vector operator *(float d, Vector a) { return new Vector(a.x * d, a.y * d, a.z * d); }
        public static Vector operator /(Vector a, float d) { return new Vector(a.x / d, a.y / d, a.z / d); }

        public static bool operator ==(Vector lhs, Vector rhs)
        {
            float diff_x = lhs.x - rhs.x;
            float diff_y = lhs.y - rhs.y;
            float diff_z = lhs.z - rhs.z;
            float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
            return sqrmag < kEpsilon * kEpsilon;
        }

        public static bool operator !=(Vector lhs, Vector rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return ToString(null, CultureInfo.InvariantCulture.NumberFormat);
        }

        public string ToString(string format)
        {
            return ToString(format, CultureInfo.InvariantCulture.NumberFormat);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F1";
            return string.Format("({0}, {1}, {2})", x.ToString(format, formatProvider), y.ToString(format, formatProvider), z.ToString(format, formatProvider));
        }

        [System.Obsolete("Use Vector.forward instead.")]
        public static Vector fwd { get { return new Vector(0F, 0F, 1F); } }
    }
}
