using System.Reflection;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;

namespace BlazeFrame.Logic;

public static class JSObjectHelper
{
    private static readonly ConstructorInfo? JSObjectConstructor = typeof(JSObjectReference).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).FirstOrDefault();

    public static JSObjectReference? CreateJSObjectReference(JSRuntime runtime, long objectId) =>
        JSObjectConstructor?.Invoke([runtime, objectId]) as JSObjectReference;

}
