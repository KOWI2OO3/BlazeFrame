using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.WebGL.GLObjects;

public record WebGLShaderPrecisionFormat(IJSObjectReference JSObject) : IWrapJSObject;

