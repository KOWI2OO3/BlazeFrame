namespace BlazeFrame.Maths;

public class Vector2(float x, float y) : IEquatable<Vector2>
{
    public static Vector2 Zero => new(0, 0);
    public static Vector2 One => new(1, 1);
    public static Vector2 UnitX => new(1, 0);
    public static Vector2 UnitY => new(0, 1);

    public float X { get; set; } = x;
    public float Y { get; set; } = y;

    public float Magnitude => MathF.Sqrt(MagnitudeSqr);

    public float MagnitudeSqr => X * X + Y * Y;

    public Vector2 Normalized => this / Magnitude;

    public void Normalize() 
    {
        var length = Magnitude;
        X /= length;
        Y /= length;
    }

    public float Dot(Vector2 other) => this * other;

    public Vector2 Negated => -this;

    public void Negate()
    {
        X = -X;
        Y = -Y;
    }

    public static Vector2 Lerp(Vector2 start, Vector2 end, float amount) => (start * (1.0f - amount)) + (end * amount);
    
    public bool Equals(Vector2? other) => other is not null && other.X == X && other.Y == Y; 
    
    public override bool Equals(object? obj) => obj is Vector2 other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(X, Y);

    public override string ToString() => $"({X}, {Y})";

    public Vector2 Copy() => new(X, Y);
    
    public static Vector2 Max(Vector2 a, Vector2 b) => new(MathF.Max(a.X, b.X), MathF.Max(a.Y, b.Y));
    public static Vector2 Min(Vector2 a, Vector2 b) => new(MathF.Min(a.X, b.X), MathF.Min(a.Y, b.Y));

    public static Vector2 operator -(Vector2 a) => new(-a.X, -a.Y);
    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
    public static float operator *(Vector2 a, Vector2 b) => a.X * b.X + a.Y * b.Y;
    public static Vector2 operator *(Vector2 a, float b) => new(a.X * b, a.Y * b);
    public static Vector2 operator *(float a, Vector2 b) => new(b.X * a, b.Y * a);
    public static Vector2 operator /(Vector2 a, float b) => new(a.X / b, a.Y / b);

    public static bool operator ==(Vector2? a, Vector2? b) => 
        (a is null && b is null) || 
        (a is not null && b is not null && 
        a.X == b.X && a.Y == b.Y);
    
    public static bool operator !=(Vector2? left, Vector2? right) => !(left == right);
    
    public static implicit operator Vector2((float x, float y) tuple) => new(tuple.x, tuple.y);
    public static implicit operator float[](Vector2 vector) => [vector.X, vector.Y];
}
