using Microsoft.JSInterop;

namespace BlazeFrame;

public interface IWrapJSObject
{
    public IJSObjectReference JSObject { get; }

}
