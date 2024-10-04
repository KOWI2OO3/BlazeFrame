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

}
