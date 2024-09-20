namespace BlazeFrame.Canvas.WebGL.GLObjects;

/// <summary>
/// represents the information returned by calling the WebGLRenderingContext.getActiveAttrib() and WebGLRenderingContext.getActiveUniform() methods.
/// </summary>
/// <param name="Name">The read-only name of the requested variable.</param>
/// <param name="Size">The read-only size of the requested variable.</param>
/// <param name="Type">The read-only type of the requested variable.</param>
/// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLActiveInfo</remarks>
public record WebGLActiveInfo(string Name, uint Size, uint Type);