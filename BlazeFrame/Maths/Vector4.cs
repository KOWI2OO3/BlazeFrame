namespace BlazeFrame.Maths;

public class Vector4(float x, float y, float z, float w)
{
    public static Vector4 Zero => new(0, 0, 0, 0);
    public static Vector4 One => new(1, 1, 1, 1);
    public static Vector4 UnitX => new(1, 0, 0, 0);
    public static Vector4 UnitY => new(0, 1, 0, 0);
    public static Vector4 UnitZ => new(0, 0, 1, 0);
    public static Vector4 UnitW => new(0, 0, 0, 1);

    public float X { get; set; } = x;
    public float Y { get; set; } = y;
    public float Z { get; set; } = z;
    public float W { get; set; } = w;

    public Vector4(Vector4 vec, float w = 1) : this(vec.X, vec.Y, vec.Z, w) { }
    public Vector4(Vector2 vec, float z = 0, float w = 1) : this(vec.X, vec.Y, z, w) { }

    public float Magnitude => MathF.Sqrt(MagnitudeSqr);
    public float MagnitudeSqr => X * X + Y * Y + Z * Z + W * W;

    public Vector4 Normalized => this / Magnitude;

    public void Normalize()
    {
        var length = Magnitude;
        X /= length;
        Y /= length;
        Z /= length;
        W /= length;
    }

    /// <summary>
    /// Returns the dot product of this vector and another vector.
    /// </summary>
    /// <param name="other">The other vector to get the dot product from</param>
    /// <returns>A float of the dot product of the 2 vectors</returns>
    public float Dot(Vector4 other) => this * other;

    public Vector4 Negated => -this;
    
    public static Vector4 Lerp(Vector4 start, Vector4 end, float amount) => (start * (1.0f - amount)) + (end * amount);

    public bool Equals(Vector4? other) => other is not null && other.X == X && other.Y == Y && other.Z == Z && other.W == W; 
    
    public override bool Equals(object? obj) => obj is Vector4 other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);

    public override string ToString() => $"({X}, {Y}, {Z}, {W})";

    public Vector4 Copy() => new(X, Y, Z, W);

    public static Vector4 operator -(Vector4 a) => new(-a.X, -a.Y, -a.Z, -a.W);
    public static Vector4 operator +(Vector4 a, Vector4 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
    public static Vector4 operator -(Vector4 a, Vector4 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
    public static float operator *(Vector4 a, Vector4 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
    public static Vector4 operator *(Vector4 a, float b) => new(a.X * b, a.Y * b, a.Z * b, a.W * b);
    public static Vector4 operator *(float a, Vector4 b) => new(b.X * a, b.Y * a, b.Z * a, b.W * a);
    public static Vector4 operator /(Vector4 a, float b) => new(a.X / b, a.Y / b, a.Z / b, a.W / b);
    
    public static bool operator ==(Vector4 a, Vector4 b) => a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.W == b.W;
    public static bool operator !=(Vector4 a, Vector4 b) => !(a == b);
    
    public static implicit operator Vector4((float x, float y, float z, float w) tuple) => new(tuple.x, tuple.y, tuple.z, tuple.w);
    public static implicit operator float[](Vector4 vector) => [vector.X, vector.Y, vector.Z, vector.W];
}
