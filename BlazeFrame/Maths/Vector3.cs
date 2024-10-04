namespace BlazeFrame.Maths;

public class Vector3(float x, float y, float z) : IEquatable<Vector3>
{
    public static Vector3 Zero => new(0, 0, 0);
    public static Vector3 One => new(1, 1, 1);
    public static Vector3 UnitX => new(1, 0, 0);
    public static Vector3 UnitY => new(0, 1, 0);
    public static Vector3 UnitZ => new(0, 0, 1);

    public float X { get; set; } = x;
    public float Y { get; set; } = y;
    public float Z { get; set; } = z;

    public Vector3(Vector2 vector2, float z = 0) : this(vector2.X, vector2.Y, z) { }
    
    /// <summary>
    /// Returns the magnitude/Length of the vector.
    /// </summary>
    public float Magnitude => MathF.Sqrt(MagnitudeSqr);

    /// <summary>
    /// Returns the squared magnitude/Length of the vector. 
    /// This is slightly faster than Magnitude as it skips the square root operation.
    /// </summary>
    public float MagnitudeSqr => X * X + Y * Y + Z * Z;

    /// <summary>
    /// Returns the normalized version of the vector.
    /// </summary>
    public Vector3 Normalized => this / Magnitude;

    /// <summary>
    /// Normalizes the vector.
    /// </summary>
    public void Normalize() 
    {
        var length = Magnitude;
        X /= length;
        Y /= length;
        Z /= length;
    }

    /// <summary>
    /// Returns the dot product of this vector and another vector.
    /// </summary>
    /// <param name="other">The other vector to get the dot product from</param>
    /// <returns>A float of the dot product of the 2 vectors</returns>
    public float Dot(Vector3 other) => this * other;

    /// <summary>
    /// Returns the cross product of this vector and another vector.
    /// Creates a vector which is perpendicular to the 2 vectors.
    /// </summary>
    /// <param name="other">The other vector in combination with this to create the cross from</param>
    /// <returns>A new vector which is perpendicular to the 2 vectors</returns>
    public Vector3 Cross(Vector3 other) => new(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z, X * other.Y - Y * other.X);

    public Vector3 Negated => -this;

    public void Negate()
    {
        X = -X;
        Y = -Y;
        Z = -Z;
    }

    /// <summary>
    /// Returns the cross product of 2 vectors.
    /// Creates a vector which is perpendicular to the 2 vectors.
    /// </summary>
    /// <param name="a">One of the vectors to calculate to dot with</param>
    /// <param name="b">the other vector to calculate to dot with</param>
    /// <returns>A new vector which is perpendicular to the 2 vectors</returns>
    public static Vector3 Cross(Vector3 a, Vector3 b) => a.Cross(b);

    public static Vector3 Lerp(Vector3 start, Vector3 end, float amount) => (start * (1.0f - amount)) + (end * amount);

    public bool Equals(Vector3? other) => other is not null && other.X == X && other.Y == Y && other.Z == Z; 
    
    public override bool Equals(object? obj) => obj is Vector3 other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
    
    public override string ToString() => $"({X}, {Y}, {Z})";

    public Vector3 Copy() => new(X, Y, Z);

    public static Vector3 Max(Vector3 a, Vector3 b) => new(MathF.Max(a.X, b.X), MathF.Max(a.Y, b.Y), MathF.Max(a.Z, b.Z));
    public static Vector3 Min(Vector3 a, Vector3 b) => new(MathF.Min(a.X, b.X), MathF.Min(a.Y, b.Y), MathF.Min(a.Z, b.Z));

    public static Vector3 operator -(Vector3 a) => new(-a.X, -a.Y, -a.Z);
    public static Vector3 operator +(Vector3 a, Vector3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vector3 operator -(Vector3 a, Vector3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static float operator *(Vector3 a, Vector3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    public static Vector3 operator *(Vector3 a, float b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Vector3 operator *(float a, Vector3 b) => new(b.X * a, b.Y * a, b.Z * a);
    public static Vector3 operator /(Vector3 a, float b) => new(a.X / b, a.Y / b, a.Z / b);
    public static bool operator ==(Vector3? a, Vector3? b) => 
        (a is null && b is null) || 
        (a is not null && b is not null && 
        a.X == b.X && a.Y == b.Y && a.Z == b.Z);

    public static bool operator !=(Vector3? a, Vector3? b) => !(a == b);
    
    public static implicit operator Vector3((float x, float y, float z) tuple) => new(tuple.x, tuple.y, tuple.z);
    public static implicit operator float[](Vector3 vector) => [vector.X, vector.Y, vector.Z];

    public float this[int index]
    {
        get => index switch
        {
            0 => X,
            1 => Y,
            2 => Z,
            _ => throw new IndexOutOfRangeException()
        };
        set => _ = index switch
        {
            0 => X = value,
            1 => Y = value,
            2 => Z = value,
            _ => throw new IndexOutOfRangeException()
        };
    }
}
