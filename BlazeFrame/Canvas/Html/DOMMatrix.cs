using System.Numerics;

namespace BlazeFrame.Canvas.Html;

internal struct DOMMatrix
{
    public float m11, m12, m13, m14;
    public float m21, m22, m23, m24;
    public float m31, m32, m33, m34;
    public float m41, m42, m43, m44;

    public static implicit operator Matrix4x4(DOMMatrix matrix) => new(
        matrix.m11, matrix.m12, matrix.m13, matrix.m14,
        matrix.m21, matrix.m22, matrix.m23, matrix.m24,
        matrix.m31, matrix.m32, matrix.m33, matrix.m34,
        matrix.m41, matrix.m42, matrix.m43, matrix.m44
    );

    public static implicit operator DOMMatrix(Matrix4x4 matrix) => new()
    {
        m11 = matrix.M11, m12 = matrix.M12, m13 = matrix.M13, m14 = matrix.M14,
        m21 = matrix.M21, m22 = matrix.M22, m23 = matrix.M23, m24 = matrix.M24,
        m31 = matrix.M31, m32 = matrix.M32, m33 = matrix.M33, m34 = matrix.M34,
        m41 = matrix.M41, m42 = matrix.M42, m43 = matrix.M43, m44 = matrix.M44
    };
        
}
