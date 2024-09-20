using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.WebGL.GLObjects;

public record WebGLShader(IJSObjectReference JSObject) : IWrapJSObject;
