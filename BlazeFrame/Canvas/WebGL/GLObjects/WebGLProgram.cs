using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.WebGL.GLObjects;

public record WebGLProgram(IJSObjectReference JSObject) : IWrapJSObject;
