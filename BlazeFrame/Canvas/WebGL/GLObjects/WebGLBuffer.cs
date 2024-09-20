using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.WebGL.GLObjects;

public record WebGLBuffer(IJSObjectReference JSObject) : IWrapJSObject;
