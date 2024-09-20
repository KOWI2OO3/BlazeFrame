using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.WebGL.GLObjects;

public record WebGLUniformLocation(IJSObjectReference JSObject) : IWrapJSObject;
