namespace BlazeFrame.Maths;

public static class MathHelper
{
    public static float Determinant3x3(float a, float b, float c, float d, float e, float f, float g, float h, float i) =>
        a * (e * i - f * h) - b * (d * i - f * g) + c * (d * h - e * g);

    public static Vector3 GetDirection(this Axis axis)
    {
        return axis switch
        {
            Axis.X => Vector3.UnitX,
            Axis.Y => Vector3.UnitY,
            Axis.Z => Vector3.UnitZ,
            _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
        };
    }

    public static Matrix4 CreateOrthographic(float left, float right, float top, float bottom, float near, float far)
    {
        var result = Matrix4.Identity;
        result.M11 = 2f / (right - left);
        result.M22 = 2f / (top - bottom);
        result.M33 = -2f / (far - near);
        result.M41 = -(right + left) / (right - left);
        result.M42 = -(top + bottom) / (top - bottom);
        result.M43 = -(far + near) / (far - near);
        return result;
    }

    public static Matrix4 CreatePerspective(float fov, float aspect, float near, float far) 
    {
        var result = Matrix4.Identity;
        var tanHalfFov = MathF.Tan(fov / 2f);

        result.M11 = 1f / (aspect * tanHalfFov);
        result.M22 = 1f / tanHalfFov;
        result.M33 = -(far + near) / (far - near);
        result.M34 = -(2f * far * near) / (far - near);
        result.M43 = -1f;
        result.M44 = 0f;
        return result;
    }

}
