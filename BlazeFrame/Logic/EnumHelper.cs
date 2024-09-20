namespace BlazeFrame.Logic;

public static class EnumHelper
{
    public static T GetByName<T>(string name) where T : struct, Enum =>
        Enum.GetValues<T>().FirstOrDefault(e => Enum.GetName(e)?.ToLower() == name);
}
