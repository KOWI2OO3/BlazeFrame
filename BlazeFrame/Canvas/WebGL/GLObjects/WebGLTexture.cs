using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.WebGL.GLObjects;

public record WebGLTexture(IJSObjectReference JSObject) : IWrapJSObject;
