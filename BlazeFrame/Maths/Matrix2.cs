namespace BlazeFrame.Maths;

public class Matrix2(float m11, float m12, float m21, float m22) : IEquatable<Matrix2>
{
    public float M11 { get; set; } = m11;
    public float M12 { get; set; } = m12;
    public float M21 { get; set; } = m21;
    public float M22 { get; set; } = m22;

    public static Matrix2 Identity => new(1, 0, 0, 1);

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

    public Vector2 Column1 => new(M11, M21);
    public Vector2 Column2 => new(M12, M22);

    public Matrix2 Inverted => new Matrix2(M22, -M12, -M21, M11) / Determinant;

    public static Matrix2 operator +(Matrix2 a, Matrix2 b) => new(a.M11 + b.M11, a.M12 + b.M12, a.M21 + b.M21, a.M22 + b.M22);
    public static Matrix2 operator -(Matrix2 a, Matrix2 b) => new(a.M11 - b.M11, a.M12 - b.M12, a.M21 - b.M21, a.M22 - b.M22);
    public static Matrix2 operator -(Matrix2 a) => new(-a.M11, -a.M12, -a.M21, -a.M22);
    public static Matrix2 operator *(Matrix2 a, float b) => new(a.M11 * b, a.M12 * b, a.M21 * b, a.M22 * b);
    public static Matrix2 operator /(Matrix2 a, float b) => new(a.M11 / b, a.M12 / b, a.M21 / b, a.M22 / b);
    
    public static bool operator ==(Matrix2? a, Matrix2? b) => b is not null && a is not null && a.M11 == b.M11 && a.M12 == b.M12 && a.M21 == b.M21 && a.M22 == b.M22;
    public static bool operator !=(Matrix2? a, Matrix2? b) => !(a == b);

    public bool Equals(Matrix2? other) => other == this; 
    
    public override bool Equals(object? obj) => obj is Matrix2 other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(M11, M12, M21, M22);

}
