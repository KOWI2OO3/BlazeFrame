using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.WebGL.GLObjects;

public record WebGLFramebuffer(IJSObjectReference JSObject) : IWrapJSObject;
