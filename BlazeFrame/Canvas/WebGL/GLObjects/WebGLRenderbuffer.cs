using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.WebGL.GLObjects;

public record WebGLRenderbuffer(IJSObjectReference JSObject) : IWrapJSObject;
