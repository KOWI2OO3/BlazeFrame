using BlazeFrame.Canvas.WebGL;

namespace BlazeFrame.Logic.WebGL;

public static class GLHelper
{
    public static string? GetParameterName(uint pname) => 
        typeof(GL).GetFields().FirstOrDefault(field => field.GetValue(null) is int fname && fname == pname)?.Name;

    public static uint? GetParameter(string pname) =>
        (uint?)typeof(GL).GetField(pname)?.GetValue(null);
}
