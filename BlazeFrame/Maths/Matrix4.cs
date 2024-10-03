namespace BlazeFrame.Maths;

public class Matrix4(
    float m11, float m12, float m13, float m14,
    float m21, float m22, float m23, float m24, 
    float m31, float m32, float m33, float m34,
    float m41, float m42, float m43, float m44) : IEquatable<Matrix4>
{
    public Vector4 Column1 { get; set; } = new(m11, m21, m31, m41);
    public Vector4 Column2 { get; set; } = new(m12, m22, m32, m42);
    public Vector4 Column3 { get; set; } = new(m13, m23, m33, m43);
    public Vector4 Column4 { get; set; } = new(m14, m24, m34, m44);


    /// <summary>The first element of the first row.</summary>
    public float M11 { get => Column1.X; set => Column1.X = value; }
    
    /// <summary>The second element of the first row.</summary>
    public float M12 { get => Column2.X; set => Column2.X = value; }
    
    /// <summary>The third element of the first row.</summary>
    public float M13 { get => Column3.X; set => Column3.X = value; }

    /// <summary>The fourth element of the first row.</summary>
    public float M14 { get => Column4.X; set => Column4.X = value; }

    
    /// <summary>The first element of the second row.</summary>
    public float M21 { get => Column1.Y; set => Column1.Y = value; }
    
    /// <summary>The second element of the second row.</summary>
    public float M22 { get => Column2.Y; set => Column2.Y = value; }

    /// <summary>The third element of the second row.</summary>
    public float M23 { get => Column3.Y; set => Column3.Y = value; }
    
    /// <summary>The fourth element of the second row.</summary>
    public float M24 { get => Column4.Y; set => Column4.Y = value; }


    /// <summary>The first element of the third row.</summary>
    public float M31 { get => Column1.Z; set => Column1.Z = value; }
    
    /// <summary>The second element of the third row.</summary>
    public float M32 { get => Column2.Z; set => Column2.Z = value; }
    
    /// <summary>The third element of the third row.</summary>
    public float M33 { get => Column3.Z; set => Column3.Z = value; }
    
    /// <summary>The fourth element of the third row.</summary>
    public float M34 { get => Column4.Z; set => Column4.Z = value; }
    
    
    /// <summary>The first element of the fourth row.</summary>
    public float M41 { get => Column1.W; set => Column1.W = value; }
    
    /// <summary>The second element of the fourth row.</summary>
    public float M42 { get => Column2.W; set => Column2.W = value; }
    
    /// <summary>The third element of the fourth row.</summary>
    public float M43 { get => Column3.W; set => Column3.W = value; }
    
    /// <summary>The fourth element of the fourth row.</summary>
    public float M44 { get => Column4.W; set => Column4.W = value; }

    public Matrix4(Vector4 Column1, Vector4 Column2, Vector4 Column3, Vector4 Column4) : this(Column1.X, Column2.X, Column3.X,  Column4.X,
                                                                                              Column1.Y, Column2.Y, Column3.Y, Column4.Y,
                                                                                              Column1.Z, Column2.Z, Column3.Z, Column4.Z,
                                                                                              Column1.W, Column2.W, Column3.W, Column4.W) {} 
    public Matrix4(Matrix3 matrix3) : this(matrix3.M11, matrix3.M12, matrix3.M13, 0,
                                           matrix3.M21, matrix3.M22, matrix3.M23, 0,
                                           matrix3.M31, matrix3.M32, matrix3.M33, 0,
                                           0, 0, 0, 1) { }

    public Matrix4() : this(1, 0, 0, 1,
                            0, 1, 0, 1,
                            0, 0, 1, 1,
                            0, 0, 0, 1) { }

    public static Matrix4 Identity => new(1, 0, 0, 0,
                                          0, 1, 0, 0,
                                          0, 0, 1, 0,
                                          0, 0, 0, 1);

    public float Detrminant => M11 * M22 * M33 * M44 + M12 * M23 * M34 * M41 + M13 * M24 * M31 * M42 + M14 * M21 * M32 * M43
                            - M14 * M23 * M32 * M41 - M13 * M22 * M34 * M41 - M12 * M24 * M31 * M43 - M11 * M21 * M33 * M44
                            - M11 * M24 * M33 * M42 - M12 * M23 * M31 * M44 - M13 * M21 * M32 * M44 - M14 * M22 * M31 * M43;
    
    public Matrix4 Transpose => new(
        M11, M21, M31, M41,
        M12, M22, M32, M42,
        M13, M23, M33, M43,
        M14, M24, M34, M44
    );

    public static Matrix4 operator -(Matrix4 a) =>
        new(-a.Column1, -a.Column2, -a.Column3, -a.Column4);

    public static Matrix4 operator +(Matrix4 a, Matrix4 b) =>
        new(a.Column1 + b.Column1, a.Column2 + b.Column2, a.Column3 + b.Column3, a.Column4 + b.Column4); 

    public static Matrix4 operator -(Matrix4 a, Matrix4 b) => a + (-b);

    public static Matrix4 operator *(Matrix4 a, float b) =>
        new(b * a.Column1, b * a.Column2, b * a.Column3, b * a.Column4);

    public static Matrix4 operator *(float a, Matrix4 b) => b * a;

    public static Matrix4 operator /(Matrix4 a, float b) => a * (1/b);

    public static Matrix4 operator *(Matrix4 a, Matrix4 b) =>
        new(
            a.Column1 * b.Column1.X + a.Column2 * b.Column1.Y + a.Column3 * b.Column1.Z + a.Column4 * b.Column1.W,
            a.Column1 * b.Column2.X + a.Column2 * b.Column2.Y + a.Column3 * b.Column2.Z + a.Column4 * b.Column2.W,
            a.Column1 * b.Column3.X + a.Column2 * b.Column3.Y + a.Column3 * b.Column3.Z + a.Column4 * b.Column3.W,
            a.Column1 * b.Column4.X + a.Column2 * b.Column4.Y + a.Column3 * b.Column4.Z + a.Column4 * b.Column4.W);

    public static bool operator ==(Matrix4? a, Matrix4? b) =>
        (a is null && b is null) || 
        (a is not null && b is not null && 
        a.Column1 == b.Column1 && a.Column2 == b.Column2 && a.Column3 == b.Column3 && a.Column4 == b.Column4);

    public static bool operator !=(Matrix4? a, Matrix4? b) => !(a == b);

    public bool Equals(Matrix4? other) => other == this;

    public override bool Equals(object? obj) => obj is Matrix4 other && Equals(other);

    public override int GetHashCode() =>
        HashCode.Combine(
            Column1.GetHashCode(),
            Column2.GetHashCode(),
            Column3.GetHashCode(),
            Column4.GetHashCode()
        );
}
