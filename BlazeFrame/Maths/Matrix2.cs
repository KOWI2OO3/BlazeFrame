namespace BlazeFrame.Maths;

public class Matrix2(float m11, float m12, float m21, float m22) : IEquatable<Matrix2>
{
    public Vector2 Column1 { get; set; } = new(m11, m12);

    public Vector2 Column2 { get; set; } = new(m21, m22);

    /// <summary>The first element of the first row.</summary>
    public float M11 { get => Column1.X; set => Column1.X = value; }
    
    /// <summary>The second element of the first row.</summary>
    public float M12 { get => Column2.X; set => Column2.X = value; }

    
    /// <summary>The first element of the second row.</summary>
    public float M21 { get => Column1.Y; set => Column1.Y = value; }
    
    /// <summary>The second element of the second row.</summary>
    public float M22 { get => Column2.Y; set => Column2.Y = value; }

    public Matrix2(Vector2 Column1, Vector2 Column2) : this(Column1.X, Column2.X, 
                                                            Column1.Y, Column2.Y) { }

    public Matrix2() : this(1, 0, 0, 1) { }

    public static Matrix2 Identity => new();

    public float Determinant => M11 * M22 - M12 * M21;

    public Matrix2 Transpose => new(M11, M21, M12, M22);

    public void Inverse()
    {
        var det = Determinant;
        float[] tmp = [M11, M12, M21, M22];
        M11 = tmp[3] / det;
        M12 = -tmp[1] / det;
        M21 = -tmp[2] / det;
        M22 = tmp[0] / det;
    }

    public Matrix2 Inverted => new Matrix2(M22, -M12, -M21, M11) / Determinant;

    public static Matrix2 operator +(Matrix2 a, Matrix2 b) => new(a.M11 + b.M11, a.M12 + b.M12, a.M21 + b.M21, a.M22 + b.M22);
    public static Matrix2 operator -(Matrix2 a, Matrix2 b) => new(a.M11 - b.M11, a.M12 - b.M12, a.M21 - b.M21, a.M22 - b.M22);
    public static Matrix2 operator -(Matrix2 a) => new(-a.M11, -a.M12, -a.M21, -a.M22);
    public static Matrix2 operator *(Matrix2 a, float b) => new(a.M11 * b, a.M12 * b, a.M21 * b, a.M22 * b);
    public static Matrix2 operator /(Matrix2 a, float b) => new(a.M11 / b, a.M12 / b, a.M21 / b, a.M22 / b);
    
    public static bool operator ==(Matrix2? a, Matrix2? b) => 
        (a is null && b is null) || 
        (a is not null && b is not null &&
        a.M11 == b.M11 && a.M12 == b.M12 && a.M21 == b.M21 && a.M22 == b.M22);
    
    public static bool operator !=(Matrix2? a, Matrix2? b) => !(a == b);

    public bool Equals(Matrix2? other) => other == this; 
    
    public override bool Equals(object? obj) => obj is Matrix2 other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(M11, M12, M21, M22);

}
