namespace BlazeFrame.Maths;

public class Matrix3(
    float m11, float m12, float m13, 
    float m21, float m22, float m23, 
    float m31, float m32, float m33) : IEquatable<Matrix3>
{
    public Vector3 Column1 { get; set; } = new(m11, m21, m31);
    public Vector3 Column2 { get; set; } = new(m12, m22, m32);
    public Vector3 Column3 { get; set; } = new(m13, m23, m33);


    /// <summary>The first element of the first row.</summary>
    public float M11 { get => Column1.X; set => Column1.X = value; }
    
    /// <summary>The second element of the first row.</summary>
    public float M12 { get => Column2.X; set => Column2.X = value; }
    
    /// <summary>The third element of the first row.</summary>
    public float M13 { get => Column3.X; set => Column3.X = value; }

    
    /// <summary>The first element of the second row.</summary>
    public float M21 { get => Column1.Y; set => Column1.Y = value; }
    
    /// <summary>The second element of the second row.</summary>
    public float M22 { get => Column2.Y; set => Column2.Y = value; }

    /// <summary>The third element of the second row.</summary>
    public float M23 { get => Column3.Y; set => Column3.Y = value; }


    /// <summary>The first element of the third row.</summary>
    public float M31 { get => Column1.Z; set => Column1.Z = value; }
    
    /// <summary>The second element of the third row.</summary>
    public float M32 { get => Column2.Z; set => Column2.Z = value; }
    
    /// <summary>The third element of the third row.</summary>
    public float M33 { get => Column3.Z; set => Column3.Z = value; }

    public Matrix3(Vector3 Column1, Vector3 Column2, Vector3 Column3) : this(Column1.X, Column2.X, Column3.X, 
                                                                             Column1.Y, Column2.Y, Column3.Y, 
                                                                             Column1.Z, Column2.Z, Column3.Z) {}

    public Matrix3(Matrix2 matrix2) : this(matrix2.M11, matrix2.M12, 0, 
                                           matrix2.M21, matrix2.M22, 0, 
                                           0, 0, 1) { }

    public static Matrix3 Identity => new(1, 0, 0, 
                                          0, 1, 0, 
                                          0, 0, 1);

    public float Determinant => M11 * M22 * M33 + M12 * M23 * M31 + M13 * M21 * M32 - M13 * M22 * M31 - M12 * M21 * M33 - M11 * M23 * M32;

    public Matrix3 Inverted => 
        new Matrix3(
            M22 * M33 - M23 * M32, M13 * M32 - M12 * M33, M12 * M23 - M13 * M22,
            M23 * M31 - M21 * M33, M11 * M33 - M13 * M31, M13 * M21 - M11 * M23,
            M21 * M32 - M22 * M31, M12 * M31 - M11 * M32, M11 * M22 - M12 * M21
        ) / Determinant;

    public Matrix3 Transposed => 
        new(
            M11, M21, M31,
            M12, M22, M32,
            M13, M23, M33
        );

    public Vector3 Transform(Vector3 vector) => this * vector;

    public Matrix3 Multiply(Matrix3 other) => this * other;

    public static Matrix3 operator -(Matrix3 a) =>
        new(
            -a.M11, -a.M12, -a.M13,
            -a.M21, -a.M22, -a.M23,
            -a.M31, -a.M32, -a.M33
        );

    public static Matrix3 operator +(Matrix3 a, Matrix3 b) => 
        new(
            a.M11 + b.M11, a.M12 + b.M12, a.M13 + b.M13,
            a.M21 + b.M21, a.M22 + b.M22, a.M23 + b.M23,
            a.M31 + b.M31, a.M32 + b.M32, a.M33 + b.M33
        );

    public static Matrix3 operator -(Matrix3 a, Matrix3 b) => 
        new(
            a.M11 - b.M11, a.M12 - b.M12, a.M13 - b.M13,
            a.M21 - b.M21, a.M22 - b.M22, a.M23 - b.M23,
            a.M31 - b.M31, a.M32 - b.M32, a.M33 - b.M33
        );

    public static Matrix3 operator *(Matrix3 a, float b) =>
        new(
            a.M11 * b, a.M12 * b, a.M13 * b,
            a.M21 * b, a.M22 * b, a.M23 * b,
            a.M31 * b, a.M32 * b, a.M33 * b
        );

    public static Matrix3 operator *(float a, Matrix3 b) => b * a;

    public static Vector3 operator *(Matrix3 a, Vector3 b) =>
        a.Column1 * b.X + a.Column2 * b.Y + a.Column3 * b.Z;

    public static Matrix3 operator *(Matrix3 a, Matrix3 b) =>
        new(
            a * b.Column1, a * b.Column2, a * b.Column3
        );
    
    public static Matrix3 operator /(Matrix3 a, float b) => a * (1 / b);

    public static bool operator ==(Matrix3 a, Matrix3 b) => a.M11 == b.M11 && a.M12 == b.M12 && a.M13 == b.M13 &&
                                                           a.M21 == b.M21 && a.M22 == b.M22 && a.M23 == b.M23 &&
                                                           a.M31 == b.M31 && a.M32 == b.M32 && a.M33 == b.M33;
    public static bool operator !=(Matrix3 a, Matrix3 b) => !(a == b);

    public bool Equals(Matrix3? other) => other is not null && other == this;

    public override bool Equals(object? obj) => obj is Matrix3 other && Equals(other);

    public override int GetHashCode() => 
        HashCode.Combine(
            Column1.GetHashCode(),
            Column2.GetHashCode(),
            Column3.GetHashCode()
        );
}
