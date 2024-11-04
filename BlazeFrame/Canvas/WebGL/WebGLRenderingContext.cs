using BlazeFrame.Canvas.WebGL.GLObjects;
using BlazeFrame.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.WebGL;

public class WebGLRenderingContext(JSInvoker invoker, IJSObjectReference JSObject) : ModuleJSObject(invoker, JSObject)
{
    private double drawingBufferWidth { get; set; }
    public double DrawingBufferWidth { get => drawingBufferWidth; }

    private double drawingBufferHeight { get; set; }
    public double DrawingBufferHeight { get => drawingBufferHeight; }

    // TODO: Add Color space properties https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext#uniforms_and_attributes
    
    internal async Task InitializeProperties()
    {
        drawingBufferWidth = await GetProperty<double>(nameof(drawingBufferWidth));
        drawingBufferHeight = await GetProperty<double>(nameof(drawingBufferHeight));
    }

#region The WebGL context

    /// <returns>true if the context is lost, otherwise returns false</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isContextLost</remarks>
    public async Task<bool> IsContextLost() => await JSObject.InvokeAsync<bool>("isContextLost");

    /// <summary>
    /// Ensures the context is compatible with the user's XR hardware, re-creating the context if necessary with a new configuration to do so. 
    /// This can be used to start an application using standard 2D presentation, then transition to using a VR or AR mode later.
    /// </summary>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/makeXRCompatible</remarks>
    public async Task MakeXRCompatible() => await Invoke("makeXRCompatible");

#endregion

#region Viewing and clipping

    /// <summary>
    /// sets a scissor box, which limits the drawing to a specified rectangle.
    /// </summary>
    /// <param name="x">A GLint specifying the horizontal coordinate for the lower left corner of the box. Default value: 0.</param>
    /// <param name="y">A GLint specifying the vertical coordinate for the lower left corner of the box. Default value: 0.</param>
    /// <param name="width">A non-negative GLsizei specifying the width of the scissor box. Default value: width of the canvas.</param>
    /// <param name="height">A non-negative GLsizei specifying the height of the scissor box. Default value: height of the canvas.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/scissor</remarks>
    public async Task Scissor(int x, int y, long width, long height) => await Invoke("scissor", x, y, width, height);

    /// <summary>
    /// sets the viewport, which specifies the affine transformation of x and y from normalized device coordinates to window coordinates.
    /// </summary>
    /// <param name="x">A GLint specifying the horizontal coordinate for the lower left corner of the viewport origin. Default value: 0.</param>
    /// <param name="y">A GLint specifying the vertical coordinate for the lower left corner of the viewport origin. Default value: 0.</param>
    /// <param name="width">A non-negative GLsizei specifying the width of the viewport. Default value: width of the canvas.</param>
    /// <param name="height">A non-negative GLsizei specifying the height of the viewport. Default value: height of the canvas.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/viewport</remarks>
    public async Task Viewport(int x, int y, long width, long height) => await Invoke("viewport", x, y, width, height);

#endregion

#region State information

    /// <summary>
    /// Specifies which texture unit to make active.
    /// </summary>
    /// <param name="texture">The texture unit to make active. The value is a gl.TEXTUREI where I is within the range from 0 to gl.MAX_COMBINED_TEXTURE_IMAGE_UNITS - 1.</param>
    /// <exception cref="GL.INVALID_ENUM">If texture is not one of gl.TEXTUREI, where I is within the range from 0 to gl.MAX_COMBINED_TEXTURE_IMAGE_UNITS - 1, a gl.INVALID_ENUM error is thrown.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/activeTexture</remarks>
    public async Task ActiveTexture(int texture) => await Invoke("activeTexture", texture);

    /// <summary>
    /// used to set the source and destination blending factors.
    /// </summary>
    /// <param name="red">A GLclampf for the red component in the range of 0 to 1. Default value is 0.</param>
    /// <param name="green">A GLclampf for the green component in the range of 0 to 1. Default value is 0.</param>
    /// <param name="blue">A GLclampf for the blue component in the range of 0 to 1. Default value is 0.</param>
    /// <param name="alpha">A GLclampf for the alpha component (transparency) in the range of 0. to 1. Default value is 0.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/blendColor</remarks>
    public async Task BlendColor(float red, float green, float blue, float alpha) => await Invoke("blendColor", red, green, blue, alpha);

    /// <summary>
    /// set both the RGB blend equation and alpha blend equation to a single equation.
    /// The blend equation determines how a new pixel is combined with a pixel already in the WebGLFramebuffer
    /// </summary>
    /// <param name="mode">
    /// A GLenum specifying how source and destination colors are combined. Must be either:
    /// - gl.FUNC_ADD: source + destination (default value)
    /// - gl.FUNC_SUBTRACT: source - destination
    /// - gl.FUNC_REVERSE_SUBTRACT: destination - source
    /// - When using the EXT_blend_minmax extension:
    ///     - ext.MIN_EXT: Minimum of source and destination
    ///     - ext.MAX_EXT: Maximum of source and destination
    /// - When using a WebGL 2 context, the following values are available additionally:
    ///     - gl.MIN: Minimum of source and destination
    ///     - gl.MAX: Maximum of source and destination
    /// </param>
    /// <exception cref="GL.INVALID_ENUM">If mode is not one of the three possible values, a gl.INVALID_ENUM error is thrown.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/blendEquation</remarks>
    public async Task BlendEquation(ulong mode) => await Invoke("blendEquation", mode);

    /// <summary>
    /// used to set the RGB blend equation and alpha blend equation separately.
    /// 
    /// The blend equation determines how a new pixel is combined with a pixel already in the WebGLFramebuffer.
    /// </summary>
    /// <param name="modeRGB">
    /// A GLenum specifying how the red, green and blue components of source and destination colors are combined. Must be either:
    /// - <c>gl.FUNC_ADD</c>: source + destination (default value),
    /// - gl.FUNC_SUBTRACT: source - destination,
    /// - gl.FUNC_REVERSE_SUBTRACT: destination - source,
    /// - When using the EXT_blend_minmax extension:
    ///     - ext.MIN_EXT: Minimum of source and destination,
    ///     - ext.MAX_EXT: Maximum of source and destination.
    /// - When using a WebGL 2 context, the following values are available additionally:
    ///     - gl.MIN: Minimum of source and destination,
    ///     - gl.MAX: Maximum of source and destination. </param>
    /// <param name="modeAlpha"></param>
    /// <exception cref="GL.INVALID_ENUM">If mode is not one of the three possible values, a gl.INVALID_ENUM error is thrown.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/blendEquationSeparate</remarks>
    public async Task BlendEquationSeparate(uint modeRGB, uint modeAlpha) => await Invoke("blendEquationSeparate", modeRGB, modeAlpha);

    /// <summary>
    /// defines which function is used for blending pixel arithmetic.
    /// </summary>
    /// <param name="sfactor">A GLenum specifying a multiplier for the source blending factors. The default value is gl.ONE. For possible values, see below.</param>
    /// <param name="dfactor">A GLenum specifying a multiplier for the destination blending factors. The default value is gl.ZERO. For possible values, see below.</param>
    /// <exception cref="GL.INVALID_ENUM">If sfactor or dfactor is not one of the listed possible values, a gl.INVALID_ENUM error is thrown.</exception>
    /// <exception cref="GL.INVALID_ENUM">If a constant color and a constant alpha value are used together as source and destination factors, a gl.INVALID_ENUM error is thrown.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/blendFunc</remarks>
    public async Task BlendFunc(uint sfactor, uint dfactor) => await Invoke("blendFunc", sfactor, dfactor);

    /// <summary>
    /// defines which function is used for blending pixel arithmetic for RGB and alpha components separately.
    /// </summary>
    /// <param name="srcRGB">A GLenum specifying a multiplier for the red, green and blue (RGB) source blending factors. The default value is gl.ONE. For possible values, see below.</param>
    /// <param name="dstRGB">A GLenum specifying a multiplier for the red, green and blue (RGB) destination blending factors. The default value is gl.ZERO. For possible values, see below.</param>
    /// <param name="srcAlpha">A GLenum specifying a multiplier for the alpha source blending factor. The default value is gl.ONE. For possible values, see below.</param>
    /// <param name="dstAlpha">A GLenum specifying a multiplier for the alpha destination blending factor. The default value is gl.ZERO. For possible values, see below.</param>
    /// <exception cref="GL.INVALID_ENUM">If srcRGB, dstRGB, srcAlpha, or dstAlpha is not one of the listed possible values, a gl.INVALID_ENUM error is thrown.</exception>
    /// <exception cref="GL.INVALID_ENUM">If a constant color and a constant alpha value are used together as source (srcRGB) and destination (dstRGB) factors, a gl.INVALID_ENUM error is thrown.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/blendFuncSeparate</remarks>
    public async Task BlendFuncSeparate(uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha) => await Invoke("blendFuncSeparate", srcRGB, dstRGB, srcAlpha, dstAlpha);

    /// <summary>
    /// specifies the color values used when clearing color buffers.
    /// This specifies what color values to use when calling the clear() method. The values are clamped between 0 and 1.
    /// </summary>
    /// <param name="red">A GLclampf specifying the red color value used when the color buffers are cleared. Default value: 0.</param>
    /// <param name="green">A GLclampf specifying the green color value used when the color buffers are cleared. Default value: 0.</param>
    /// <param name="blue">A GLclampf specifying the blue color value used when the color buffers are cleared. Default value: 0.</param>
    /// <param name="alpha">A GLclampf specifying the alpha (transparency) value used when the color buffers are cleared. Default value: 0.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/clearColor</remarks>
    public async Task ClearColor(float red, float green, float blue, float alpha) => await Invoke("clearColor", red, green, blue, alpha);

    /// <summary>
    /// specifies the clear value for the depth buffer.
    /// This specifies what depth value to use when calling the clear() method. The value is clamped between 0 and 1.
    /// </summary>
    /// <param name="depth">A GLclampf specifying the depth value used when the depth buffer is cleared. Default value: 1.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/clearDepth</remarks>
    public async Task ClearDepth(float depth) => await Invoke("clearDepth", depth);

    /// <summary>
    /// specifies the clear value for the stencil buffer.
    /// This specifies what stencil value to use when calling the clear() method.
    /// </summary>
    /// <param name="s">A GLint specifying the index used when the stencil buffer is cleared. Default value: 0.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/clearStencil</remarks>
    public async Task ClearStencil(int s) => await Invoke("clearStencil", s);

    /// <summary>
    /// sets which color components to enable or to disable when drawing or rendering to a WebGLFramebuffer.
    /// </summary>
    /// <param name="red">A GLboolean specifying whether or not the red color component can be written into the frame buffer. Default value: true.</param>
    /// <param name="green">A GLboolean specifying whether or not the green color component can be written into the frame buffer. Default value: true.</param>
    /// <param name="blue">A GLboolean specifying whether or not the blue color component can be written into the frame buffer. Default value: true.</param>
    /// <param name="alpha">A GLboolean specifying whether or not the alpha (transparency) component can be written into the frame buffer. Default value: true.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/colorMask</remarks>
    public async Task ColorMask(bool red, bool green, bool blue, bool alpha) => await Invoke("colorMask", red, green, blue, alpha);

    /// <summary>
    /// specifies whether or not front- and/or back-facing polygons can be culled.
    /// </summary>
    /// <param name="mode">
    /// A GLenum specifying whether front- or back-facing polygons are candidates for culling. 
    /// The default value is gl.BACK. Possible values are:
    /// - gl.FRONT
    /// - gl.BACK
    /// - gl.FRONT_AND_BACK
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/cullFace</remarks>
    public async Task CullFace(uint mode) => await Invoke("cullFace", mode);

    /// <summary>
    /// specifies a function that compares incoming pixel depth to the current depth buffer value.
    /// </summary>
    /// <param name="func">
    /// A GLenum specifying the depth comparison function, which sets the conditions under which the pixel will be drawn. 
    /// The default value is gl.LESS. Possible values are:
    /// - gl.NEVER (never pass)
    /// - gl.LESS (pass if the incoming value is less than the depth buffer value)
    /// - gl.EQUAL (pass if the incoming value equals the depth buffer value)
    /// - gl.LEQUAL (pass if the incoming value is less than or equal to the depth buffer value)
    /// - gl.GREATER (pass if the incoming value is greater than the depth buffer value)
    /// - gl.NOTEQUAL (pass if the incoming value is not equal to the depth buffer value)
    /// - gl.GEQUAL (pass if the incoming value is greater than or equal to the depth buffer value)
    /// - gl.ALWAYS (always pass)
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/depthFunc</remarks>
    public async Task DepthFunc(uint func) => await Invoke("depthFunc", func);

    /// <summary>
    /// sets whether writing into the depth buffer is enabled or disabled.
    /// </summary>
    /// <param name="flag">A GLboolean specifying whether or not writing into the depth buffer is enabled. Default value: true, meaning that writing is enabled.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/depthMask</remarks>
    public async Task DepthMask(bool flag) => await Invoke("depthMask", flag);

    /// <summary>
    /// specifies the depth range mapping from normalized device coordinates to window or viewport coordinates.
    /// </summary>
    /// <param name="zNear">A GLclampf specifying the mapping of the near clipping plane to window or viewport coordinates. Clamped to the range 0 to 1 and must be less than or equal to zFar. The default value is 0.</param>
    /// <param name="zFar">A GLclampf specifying the mapping of the far clipping plane to window or viewport coordinates. Clamped to the range 0 to 1. The default value is 1.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/depthRange</remarks>
    public async Task DepthRange(float zNear, float zFar) => await Invoke("depthRange", zNear, zFar);

    /// <summary>
    /// disables specific WebGL capabilities for this context.
    /// </summary>
    /// <param name="capability">
    /// A GLenum specifying which WebGL capability to disable. Possible values:
    /// - gl.BLEND  	                Deactivates blending of the computed fragment color values. See WebGLRenderingContext.blendFunc().
    /// - gl.CULL_FACE  	            Deactivates culling of polygons. See WebGLRenderingContext.cullFace().
    /// - gl.DEPTH_TEST 	            Deactivates depth comparisons and updates to the depth buffer. See WebGLRenderingContext.depthFunc().
    /// - gl.DITHER 	                Deactivates dithering of color components before they get written to the color buffer.
    /// - gl.POLYGON_OFFSET_FILL    	Deactivates adding an offset to depth values of polygon's fragments. See WebGLRenderingContext.polygonOffset().
    /// - gl.SAMPLE_ALPHA_TO_COVERAGE   Deactivates the computation of a temporary coverage value determined by the alpha value.
    /// - gl.SAMPLE_COVERAGE    	    Deactivates ANDing the fragment's coverage with the temporary coverage value. See WebGLRenderingContext.sampleCoverage().
    /// - gl.SCISSOR_TEST   	        Deactivates the scissor test that discards fragments that are outside of the scissor rectangle. See WebGLRenderingContext.scissor().
    /// - gl.STENCIL_TEST   	        Deactivates stencil testing and updates to the stencil buffer. See WebGLRenderingContext.stencilFunc().
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/disable</remarks>
    public async Task Disable(uint capability) => await Invoke("disable", capability);

    /// <summary>
    /// enables specific WebGL capabilities for this context.
    /// </summary>
    /// <param name="capability">
    /// A GLenum specifying which WebGL capability to disable. Possible values:
    /// - gl.BLEND  	                Deactivates blending of the computed fragment color values. See WebGLRenderingContext.blendFunc().
    /// - gl.CULL_FACE  	            Deactivates culling of polygons. See WebGLRenderingContext.cullFace().
    /// - gl.DEPTH_TEST 	            Deactivates depth comparisons and updates to the depth buffer. See WebGLRenderingContext.depthFunc().
    /// - gl.DITHER 	                Deactivates dithering of color components before they get written to the color buffer.
    /// - gl.POLYGON_OFFSET_FILL    	Deactivates adding an offset to depth values of polygon's fragments. See WebGLRenderingContext.polygonOffset().
    /// - gl.SAMPLE_ALPHA_TO_COVERAGE   Deactivates the computation of a temporary coverage value determined by the alpha value.
    /// - gl.SAMPLE_COVERAGE    	    Deactivates ANDing the fragment's coverage with the temporary coverage value. See WebGLRenderingContext.sampleCoverage().
    /// - gl.SCISSOR_TEST   	        Deactivates the scissor test that discards fragments that are outside of the scissor rectangle. See WebGLRenderingContext.scissor().
    /// - gl.STENCIL_TEST   	        Deactivates stencil testing and updates to the stencil buffer. See WebGLRenderingContext.stencilFunc().
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/enable</remarks>
    public async Task Enable(uint capability) => await Invoke("enable", capability);

    /// <summary>
    /// specifies whether polygons are front- or back-facing by setting a winding orientation.
    /// </summary>
    /// <param name="mode">
    /// A GLenum type winding orientation. The default value is gl.CCW. Possible values:
    /// - gl.CW: Clock-wise winding.
    /// - gl.CCW: Counter-clock-wise winding.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/frontFace</remarks>
    public async Task FrontFace(uint mode) => await Invoke("frontFace", mode);

    /// <summary>
    /// returns a value for the passed parameter name. 
    /// using GLParameters you get a strongly typed return value.
    /// </summary>
    /// <typeparam name="T">The type returned from the webGL instance</typeparam>
    /// <param name="pname">A GLenum specifying which parameter value to return. See below for possible values.</param>
    /// <returns>a value for the passed parameter name.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getParameter</remarks>
    public Proxy<T> GetPrarameter<T>(uint pname) => InvokeBatched<Proxy<T>>("getParameter", pname); 
    
    // TODO: C# Cache the values of the parameters when set

    /// <summary>
    /// returns a value for the passed parameter name.
    /// </summary>
    /// <typeparam name="T">The type returned from the webGL instance</typeparam>
    /// <param name="pname">A GLenum specifying which parameter value to return. See below for possible values.</param>
    /// <returns>a value for the passed parameter name.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getParameter</remarks>
    public Proxy<T> GetPrarameter<T>(GLParameter<T> pname) => InvokeBatched<Proxy<T>>("getParameter", pname.Name);

    /// <summary>
    /// returns error information.
    /// </summary>
    /// <returns>error information.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getError</remarks>
    public BinaryNumberProxy<uint> GetError() =>  InvokeBatched<BinaryNumberProxy<uint>>("getError");

    /// <summary>
    /// specifies hints for certain behaviors. The interpretation of these hints depend on the implementation.
    /// </summary>
    /// <param name="target">
    /// Sets which behavior to be controlled. Possible values:
    ///     gl.GENERATE_MIPMAP_HINT
    ///         Quality of filtering when generating mipmap images with WebGLRenderingContext.generateMipmap().
    /// 
    /// When using the OES_standard_derivatives extension:
    ///     ext.FRAGMENT_SHADER_DERIVATIVE_HINT_OES
    ///         Accuracy of the derivative calculation for the GLSL built-in functions: dFdx, dFdy, and fwidth.
    /// 
    /// When using a WebGL 2 context, the following values are available additionally:
    ///     gl.FRAGMENT_SHADER_DERIVATIVE_HINT
    ///         Same as ext.FRAGMENT_SHADER_DERIVATIVE_HINT_OES</param>
    /// <param name="mode">
    /// Sets the behavior. The default value is gl.DONT_CARE. The possible values are:
    /// - gl.FASTEST: The most efficient behavior should be used.
    /// - gl.NICEST: The most correct or the highest quality option should be used.
    /// - gl.DONT_CARE: There is no preference for this behavior.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/hint</remarks>
    public async Task Hint(uint target, uint mode) => await Invoke("hint", target, mode);

    /// <summary>
    /// tests whether a specific WebGL capability is enabled or not for this context.
    /// By default, all capabilities except gl.DITHER are disabled.
    /// </summary>
    /// <param name="capability">
    /// A GLenum specifying which WebGL capability to disable. Possible values:
    /// - gl.BLEND  	                Deactivates blending of the computed fragment color values. See WebGLRenderingContext.blendFunc().
    /// - gl.CULL_FACE  	            Deactivates culling of polygons. See WebGLRenderingContext.cullFace().
    /// - gl.DEPTH_TEST 	            Deactivates depth comparisons and updates to the depth buffer. See WebGLRenderingContext.depthFunc().
    /// - gl.DITHER 	                Deactivates dithering of color components before they get written to the color buffer.
    /// - gl.POLYGON_OFFSET_FILL    	Deactivates adding an offset to depth values of polygon's fragments. See WebGLRenderingContext.polygonOffset().
    /// - gl.SAMPLE_ALPHA_TO_COVERAGE   Deactivates the computation of a temporary coverage value determined by the alpha value.
    /// - gl.SAMPLE_COVERAGE    	    Deactivates ANDing the fragment's coverage with the temporary coverage value. See WebGLRenderingContext.sampleCoverage().
    /// - gl.SCISSOR_TEST   	        Deactivates the scissor test that discards fragments that are outside of the scissor rectangle. See WebGLRenderingContext.scissor().
    /// - gl.STENCIL_TEST   	        Deactivates stencil testing and updates to the stencil buffer. See WebGLRenderingContext.stencilFunc().
    /// </param>
    /// <returns>A GLboolean indicating if the capability cap is enabled (true), or not (false).</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isEnabled</remarks>
    public BooleanProxy IsEnabled(uint capability) => InvokeBatched<BooleanProxy>("isEnabled", capability);

    /// <summary>
    /// sets the line width of rasterized lines.
    /// </summary>
    /// <param name="width">A GLfloat specifying the width of rasterized lines. Default value: 1.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/lineWidth</remarks>
    [Obsolete("""
        Warning: The webgl spec, based on the OpenGL ES 2.0/3.0 specs points out that the minimum and maximum width for a line is implementation defined. 
        The maximum minimum width is allowed to be 1.0. The minimum maximum width is also allowed to be 1.0. 
        Because of these implementation defined limits it is not recommended to use line widths other than 1.0 since there is no guarantee any user's browser will display any other width. 
        As of January 2017 most implementations of WebGL only support a minimum of 1 and a maximum of 1 as the technology they are based on has these same limits.
        """)]
    public async Task LineWidth(float width) => await Invoke("lineWidth", width);

    /// <summary>
    /// specifies the pixel storage modes.
    /// </summary>
    /// <param name="pname">A GLenum specifying which parameter to set. See below for possible values.</param>
    /// <param name="param">A GLint specifying a value to set the pname parameter to. See below for possible values.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/pixelStorei</remarks>
    public async Task PixelStorei(uint pname, int param) => await Invoke("pixelStorei", pname, param);

    /// <summary>
    /// specifies the scale factors and units to calculate depth values.
    /// The offset is added before the depth test is performed and before the value is written into the depth
    /// </summary>
    /// <param name="factor">A GLfloat which sets the scale factor for the variable depth offset for each polygon. The default value is 0.</param>
    /// <param name="units">A GLfloat which sets the multiplier by which an implementation-specific value is multiplied with to create a constant depth offset. The default value is 0.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/polygonOffset</remarks>
    public async Task PolygonOffset(float factor, float units) => await Invoke("polygonOffset", factor, units);

    /// <summary>
    /// specifies multi-sample coverage parameters for anti-aliasing effects.
    /// </summary>
    /// <param name="value">A GLclampf which sets a single floating-point coverage value clamped to the range [0,1]. The default value is 1.0.</param>
    /// <param name="invert">A GLboolean which sets whether or not the coverage masks should be inverted. The default value is false.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/sampleCoverage</remarks>
    public async Task SampleCoverage(float value, bool invert) => await Invoke("sampleCoverage", value, invert);
    
    /// <summary>
    /// sets the front and back function and reference value for stencil testing.
    /// Stenciling enables and disables drawing on a per-pixel basis. It is typically used in multipass rendering to achieve special effects.
    /// </summary>
    /// <param name="func">
    /// A GLenum specifying the test function. The default function is gl.ALWAYS. The possible values are:
    /// - gl.NEVER: Never pass.
    /// - gl.LESS: Pass if (ref & mask) < (stencil & mask).
    /// - gl.EQUAL: Pass if (ref & mask) = (stencil & mask).
    /// - gl.LEQUAL: Pass if (ref & mask) <= (stencil & mask).
    /// - gl.GREATER: Pass if (ref & mask) > (stencil & mask).
    /// - gl.NOTEQUAL: Pass if (ref & mask) !== (stencil & mask).
    /// - gl.GEQUAL: Pass if (ref & mask) >= (stencil & mask).
    /// - gl.ALWAYS: Always pass.
    /// </param>
    /// <param name="ref">A GLint specifying the reference value for the stencil test. This value is clamped to the range 0 to 2^n - 1 where n is the number of bitplanes in the stencil buffer. The default value is 0.</param>
    /// <param name="mask">A GLuint specifying a bit-wise mask that is used to AND the reference value and the stored stencil value when the test is done. The default value is all 1.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilFunc</remarks>
    public async Task StencilFunc(uint func, int @ref, uint mask) => await Invoke("stencilFunc", func, @ref, mask);

    /// <summary>
    /// sets the front and/or back function and reference value for stencil testing.
    /// Stencilling enables and disables drawing on a per-pixel basis. It is typically used in multipass rendering to achieve special effects.
    /// </summary>
    /// <param name="face">
    /// A GLenum specifying whether the front and/or back stencil state is updated. The possible values are:
    /// - gl.FRONT
    /// - gl.BACK
    /// - gl.FRONT_AND_BACK
    /// </param>
    /// <param name="func">
    /// A GLenum specifying the test function. The default function is gl.ALWAYS. The possible values are:
    /// - gl.NEVER: Never pass.
    /// - gl.LESS: Pass if (ref & mask) < (stencil & mask).
    /// - gl.EQUAL: Pass if (ref & mask) = (stencil & mask).
    /// - gl.LEQUAL: Pass if (ref & mask) <= (stencil & mask).
    /// - gl.GREATER: Pass if (ref & mask) > (stencil & mask).
    /// - gl.NOTEQUAL: Pass if (ref & mask) !== (stencil & mask).
    /// - gl.GEQUAL: Pass if (ref & mask) >= (stencil & mask).
    /// - gl.ALWAYS: Always pass.
    /// </param>
    /// <param name="ref">A GLint specifying the reference value for the stencil test. This value is clamped to the range 0 to 2^n - 1 where n is the number of bitplanes in the stencil buffer. The default value is 0.</param>
    /// <param name="mask">A GLuint specifying a bit-wise mask that is used to AND the reference value and the stored stencil value when the test is done. The default value is all 1.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilFuncSeparate</remarks>
    public async Task StencilFuncSeparate(uint face, uint func, int @ref, uint mask) => await Invoke("stencilFuncSeparate", face, func, @ref, mask);

    /// <summary>
    /// controls enabling and disabling of both the front and back writing of individual bits in the stencil planes.
    /// The WebGLRenderingContext.stencilMaskSeparate() method can set front and back stencil writemasks to different values.
    /// </summary>
    /// <param name="mask">A GLuint specifying a bit mask to enable or disable writing of individual bits in the stencil planes. By default, the mask is all 1.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilMask</remarks>
    public async Task StencilMask(uint mask) => await Invoke("stencilMask", mask);

    /// <summary>
    /// controls enabling and disabling of front and/or back writing of individual bits in the stencil planes.
    /// The WebGLRenderingContext.stencilMask() method can set both, the front and back stencil writemasks to one value at the same time.
    /// </summary>
    /// <param name="face">
    /// A GLenum specifying whether the front and/or back stencil state is updated. The possible values are:
    /// - gl.FRONT
    /// - gl.BACK
    /// - gl.FRONT_AND_BACK
    /// </param>
    /// <param name="mask">A GLuint specifying a bit mask to enable or disable writing of individual bits in the stencil planes. By default, the mask is all 1.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilMaskSeparate</remarks>
    public async Task StencilMaskSeparate(uint face, uint mask) => await Invoke("stencilMaskSeparate", face, mask);

    /// <summary>
    /// sets both the front and back-facing stencil test actions.
    /// </summary>
    /// <param name="fail">A GLenum specifying the function to use when the stencil test fails. The default value is gl.KEEP.</param>
    /// <param name="zfail">A GLenum specifying the function to use when the stencil test passes, but the depth test fails. The default value is gl.KEEP.</param>
    /// <param name="zpass">A GLenum specifying the function to use when both the stencil test and the depth test pass, 
    /// or when the stencil test passes and there is no depth buffer or depth testing is disabled. The default value is gl.KEEP.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilOp</remarks>
    public async Task StencilOp(uint fail, uint zfail, uint zpass) => await Invoke("stencilOp", fail, zfail, zpass);

    /// <summary>
    /// sets the front and/or back-facing stencil test actions.
    /// </summary>
    /// <param name="face">
    /// A GLenum specifying whether the front and/or back stencil state is updated. The possible values are:
    /// - gl.FRONT
    /// - gl.BACK
    /// - gl.FRONT_AND_BACK
    /// </param>
    /// <param name="fail">A GLenum specifying the function to use when the stencil test fails. The default value is gl.KEEP.</param>
    /// <param name="zfail">A GLenum specifying the function to use when the stencil test passes, but the depth test fails. The default value is gl.KEEP.</param>
    /// <param name="zpass">A GLenum specifying the function to use when both the stencil test and the depth test pass, 
    /// or when the stencil test passes and there is no depth buffer or depth testing is disabled. The default value is gl.KEEP.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/stencilOpSeparate</remarks>
    public async Task StencilOpSeparate(uint face, uint fail, uint zfail, uint zpass) => await Invoke("stencilOpSeparate", face, fail, zfail, zpass);

#endregion

#region Buffers

    /// <summary>
    /// binds a given WebGLBuffer to a target.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.ARRAY_BUFFER
    ///     Buffer containing vertex attributes, such as vertex coordinates, texture coordinate data, or vertex color data.
    /// 
    /// - gl.ELEMENT_ARRAY_BUFFER
    ///     Buffer used for element indices.
    /// </param>
    /// <param name="buffer">A WebGLBuffer to bind.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bindBuffer</remarks>
    public async Task BindBuffer(uint target, WebGLBuffer buffer) => await Invoke("bindBuffer", target, buffer);

    /// <summary>
    /// initializes and creates the buffer object's data store.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.ARRAY_BUFFER
    ///     Buffer containing vertex attributes, such as vertex coordinates, texture coordinate data, or vertex color data.
    /// 
    /// - gl.ELEMENT_ARRAY_BUFFER
    ///     Buffer used for element indices.
    /// </param>
    /// <param name="size">A GLsizeiptr setting the size in bytes of the buffer object's data store.</param>
    /// <param name="usage">
    /// A GLenum specifying the intended usage pattern of the data store for optimization purposes. Possible values:
    ///  - gl.STATIC_DRAW
    ///      The contents are intended to be specified once by the application, and used many times as the source for WebGL drawing and image specification commands.
    /// 
    ///  - gl.DYNAMIC_DRAW
    ///      The contents are intended to be respecified repeatedly by the application, and used many times as the source for WebGL drawing and image specification commands.
    /// 
    ///  - gl.STREAM_DRAW
    ///      The contents are intended to be specified once by the application, and used at most a few times as the source for WebGL drawing and image specification commands.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bufferData</remarks>
    public async Task BufferData(uint target, long size, uint usage) => await Invoke("bufferData", target, size, usage);

    /// <summary>
    /// initializes and creates the buffer object's data store.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.ARRAY_BUFFER
    ///     Buffer containing vertex attributes, such as vertex coordinates, texture coordinate data, or vertex color data.
    /// 
    /// - gl.ELEMENT_ARRAY_BUFFER
    ///     Buffer used for element indices.
    /// </param>
    /// <param name="srcData">An ArrayBuffer, SharedArrayBuffer, a TypedArray or a DataView that will be copied into the data store. If null, a data store is still created, but the content is uninitialized and undefined.</param>
    /// <param name="usage">
    /// A GLenum specifying the intended usage pattern of the data store for optimization purposes. Possible values:
    ///  - gl.STATIC_DRAW
    ///      The contents are intended to be specified once by the application, and used many times as the source for WebGL drawing and image specification commands.
    /// 
    ///  - gl.DYNAMIC_DRAW
    ///      The contents are intended to be respecified repeatedly by the application, and used many times as the source for WebGL drawing and image specification commands.
    /// 
    ///  - gl.STREAM_DRAW
    ///      The contents are intended to be specified once by the application, and used at most a few times as the source for WebGL drawing and image specification commands.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bufferData</remarks>
    public async Task BufferData(uint target, byte[] srcData, uint usage) => await Invoke("bufferData", target, srcData, usage);

    /// <summary>
    /// initializes and creates the buffer object's data store.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.ARRAY_BUFFER
    ///     Buffer containing vertex attributes, such as vertex coordinates, texture coordinate data, or vertex color data.
    /// 
    /// - gl.ELEMENT_ARRAY_BUFFER
    ///     Buffer used for element indices.
    /// </param>
    /// <param name="srcData">An ArrayBuffer, SharedArrayBuffer, a TypedArray or a DataView that will be copied into the data store. If null, a data store is still created, but the content is uninitialized and undefined.</param>
    /// <param name="usage">
    /// A GLenum specifying the intended usage pattern of the data store for optimization purposes. Possible values:
    ///  - gl.STATIC_DRAW
    ///      The contents are intended to be specified once by the application, and used many times as the source for WebGL drawing and image specification commands.
    /// 
    ///  - gl.DYNAMIC_DRAW
    ///      The contents are intended to be respecified repeatedly by the application, and used many times as the source for WebGL drawing and image specification commands.
    /// 
    ///  - gl.STREAM_DRAW
    ///      The contents are intended to be specified once by the application, and used at most a few times as the source for WebGL drawing and image specification commands.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bufferData</remarks>
    public async Task BufferData(uint target, float[] srcData, uint usage) => await Invoke("bufferData", target, srcData, usage);
    
    /// <summary>
    /// initializes and creates the buffer object's data store.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.ARRAY_BUFFER
    ///     Buffer containing vertex attributes, such as vertex coordinates, texture coordinate data, or vertex color data.
    /// 
    /// - gl.ELEMENT_ARRAY_BUFFER
    ///     Buffer used for element indices.
    /// </param>
    /// <param name="srcData">An ArrayBuffer, SharedArrayBuffer, a TypedArray or a DataView that will be copied into the data store. If null, a data store is still created, but the content is uninitialized and undefined.</param>
    /// <param name="usage">
    /// A GLenum specifying the intended usage pattern of the data store for optimization purposes. Possible values:
    ///  - gl.STATIC_DRAW
    ///      The contents are intended to be specified once by the application, and used many times as the source for WebGL drawing and image specification commands.
    /// 
    ///  - gl.DYNAMIC_DRAW
    ///      The contents are intended to be respecified repeatedly by the application, and used many times as the source for WebGL drawing and image specification commands.
    /// 
    ///  - gl.STREAM_DRAW
    ///      The contents are intended to be specified once by the application, and used at most a few times as the source for WebGL drawing and image specification commands.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bufferData</remarks>
    public async Task BufferData(uint target, double[] srcData, uint usage) => await Invoke("bufferData", target, srcData, usage);
    
    /// <summary>
    /// initializes and creates the buffer object's data store.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.ARRAY_BUFFER
    ///     Buffer containing vertex attributes, such as vertex coordinates, texture coordinate data, or vertex color data.
    /// 
    /// - gl.ELEMENT_ARRAY_BUFFER
    ///     Buffer used for element indices.
    /// </param>
    /// <param name="srcData">An ArrayBuffer, SharedArrayBuffer, a TypedArray or a DataView that will be copied into the data store. If null, a data store is still created, but the content is uninitialized and undefined.</param>
    /// <param name="usage">
    /// A GLenum specifying the intended usage pattern of the data store for optimization purposes. Possible values:
    ///  - gl.STATIC_DRAW
    ///      The contents are intended to be specified once by the application, and used many times as the source for WebGL drawing and image specification commands.
    /// 
    ///  - gl.DYNAMIC_DRAW
    ///      The contents are intended to be respecified repeatedly by the application, and used many times as the source for WebGL drawing and image specification commands.
    /// 
    ///  - gl.STREAM_DRAW
    ///      The contents are intended to be specified once by the application, and used at most a few times as the source for WebGL drawing and image specification commands.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bufferData</remarks>
    public async Task BufferData(uint target, uint[] srcData, uint usage) => await Invoke("bufferData", target, srcData, usage);

    /// <summary>
    /// updates a subset of a buffer object's data store.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.ARRAY_BUFFER
    ///     Buffer containing vertex attributes, such as vertex coordinates, texture coordinate data, or vertex color data.
    /// 
    /// - gl.ELEMENT_ARRAY_BUFFER
    ///     Buffer used for element indices.
    /// </param>
    /// <param name="offset">A GLintptr specifying an offset in bytes where the data replacement will start.</param>
    /// <param name="srcData">
    /// An ArrayBuffer, SharedArrayBuffer, a TypedArray or a DataView that will be copied into the data store. 
    /// If null, a data store is still created, but the content is uninitialized and undefined.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bufferSubData</remarks>
    public async Task BufferSubData(uint target, long offset) => await Invoke("bufferSubData", target, offset);
    
    /// <summary>
    /// updates a subset of a buffer object's data store.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.ARRAY_BUFFER
    ///     Buffer containing vertex attributes, such as vertex coordinates, texture coordinate data, or vertex color data.
    /// 
    /// - gl.ELEMENT_ARRAY_BUFFER
    ///     Buffer used for element indices.
    /// </param>
    /// <param name="offset">A GLintptr specifying an offset in bytes where the data replacement will start.</param>
    /// <param name="srcData">An ArrayBuffer, SharedArrayBuffer, a DataView, or a TypedArray that will be copied into the data store.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bufferSubData</remarks>
    public async Task BufferSubData(uint target, long offset, byte[] srcData) => await Invoke("bufferSubData", target, offset, srcData);

    /// <summary>
    /// creates and initializes a WebGLBuffer storing data such as vertices or colors.
    /// </summary>
    /// <returns>A WebGLBuffer storing data such as vertices or colors.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createBuffer</remarks>
    public async Task<WebGLBuffer> CreateBuffer() => await Invoke<WebGLBuffer>("createBuffer");

    /// <summary>
    /// deletes a given WebGLBuffer. This method has no effect if the buffer has already been deleted. 
    /// Normally you don't need to call this method yourself, when the buffer object is dereferenced it will be marked as free.
    /// </summary>
    /// <param name="buffer">A WebGLBuffer object to delete.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/deleteBuffer</remarks>
    public async Task DeleteBuffer(WebGLBuffer buffer) => await Invoke("deleteBuffer", buffer);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">either int or uint</typeparam>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.ARRAY_BUFFER
    ///     Buffer containing vertex attributes, such as vertex coordinates, texture coordinate data, or vertex color data.
    /// 
    /// - gl.ELEMENT_ARRAY_BUFFER
    ///     Buffer used for element indices.
    /// </param>
    /// <param name="pname">
    /// A GLenum specifying information to query. Possible values:
    /// - gl.BUFFER_SIZE
    ///     Returns a GLint indicating the size of the buffer in bytes.
    /// 
    /// - gl.BUFFER_USAGE
    ///     Returns a GLenum indicating the usage pattern of the buffer. One of the following:
    /// 
    ///     - gl.STATIC_DRAW
    ///     - gl.DYNAMIC_DRAW
    ///     - gl.STREAM_DRAW
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getBufferParameter</remarks>
    public BinaryNumberProxy<int> GetBufferParameter(uint target, uint pname) => InvokeBatched<BinaryNumberProxy<int>>("getBufferParameter", target, pname);

    /// <summary>
    /// returns true if the passed WebGLBuffer is valid and false otherwise.
    /// </summary>
    /// <param name="buffer">A WebGLBuffer to check.</param>
    /// <returns>A GLboolean indicating whether or not the buffer is valid.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isBuffer</remarks>
    public BooleanProxy IsBuffer(WebGLBuffer buffer) => InvokeBatched<BooleanProxy>("isBuffer", buffer);

#endregion

#region Framebuffers

    /// <summary>
    /// binds to the specified target the provided WebGLFramebuffer, or, if the framebuffer argument is null, 
    /// the default WebGLFramebuffer, which is associated with the canvas rendering context.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.FRAMEBUFFER
    ///     Collection buffer data storage of color, alpha, depth and stencil buffers used as both a destination for drawing and as a source for reading (see below).
    /// 
    /// When using a WebGL 2 context, the following values are available additionally:
    /// - gl.DRAW_FRAMEBUFFER
    ///     Used as a destination for drawing operations such as gl.draw*, gl.clear* and gl.blitFramebuffer.
    /// 
    /// - gl.READ_FRAMEBUFFER
    ///     Used as a source for reading operations such as gl.readPixels and gl.blitFramebuffer.
    /// </param>
    /// <param name="framebuffer">A WebGLFramebuffer object to bind, or null for binding the HTMLCanvasElement or OffscreenCanvas object associated with the rendering context.</param>
    /// <exception cref="GL.INVALID_ENUM">If target is not gl.FRAMEBUFFER, gl.DRAW_FRAMEBUFFER, or gl.READ_FRAMEBUFFER.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bindFramebuffer</remarks>
    public async Task BindFramebuffer(uint target, WebGLFramebuffer framebuffer) => await Invoke("bindFramebuffer", target, framebuffer);

    /// <summary>
    /// returns the completeness status of the WebGLFramebuffer object.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.FRAMEBUFFER
    ///     Collection buffer data storage of color, alpha, depth and stencil buffers used as both a destination for drawing and as a source for reading (see below).
    /// 
    /// When using a WebGL 2 context, the following values are available additionally:
    /// - gl.DRAW_FRAMEBUFFER
    ///     Used as a destination for drawing operations such as gl.draw*, gl.clear* and gl.blitFramebuffer.
    /// 
    /// - gl.READ_FRAMEBUFFER
    ///     Used as a source for reading operations such as gl.readPixels and gl.blitFramebuffer.
    /// </param>
    /// <returns>
    /// A GLenum indicating the completeness status of the framebuffer or 0 if an error occurs. Possible enum return values:
    /// - gl.FRAMEBUFFER_COMPLETE: The framebuffer is ready to display.
    /// - gl.FRAMEBUFFER_INCOMPLETE_ATTACHMENT: The attachment types are mismatched or not all framebuffer attachment points are framebuffer attachment complete.
    /// - gl.FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT: There is no attachment.
    /// - gl.FRAMEBUFFER_INCOMPLETE_DIMENSIONS: Height and width of the attachment are not the same.
    /// - gl.FRAMEBUFFER_UNSUPPORTED: The format of the attachment is not supported or if depth and stencil attachments are not the same renderbuffer.
    /// When using a WebGL 2 context, the following values can be returned additionally:
    ///  - gl.FRAMEBUFFER_INCOMPLETE_MULTISAMPLE: The values of gl.RENDERBUFFER_SAMPLES are different among attached renderbuffers, or are non-zero if the attached images are a mix of renderbuffers and textures.
    /// When using the OVR_multiview2 extension, the following value can be returned additionally:
    ///  - ext.FRAMEBUFFER_INCOMPLETE_VIEW_TARGETS_OVR: If baseViewIndex is not the same for all framebuffer attachment points where the value of FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE is not NONE, the framebuffer is considered incomplete.
    /// </returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/checkFramebufferStatus</remarks>
    public BinaryNumberProxy<uint> CheckFramebufferStatus(uint target) => InvokeBatched<BinaryNumberProxy<uint>>("checkFramebufferStatus", target);

    /// <summary>
    /// creates and initializes a WebGLFramebuffer object.
    /// </summary>
    /// <returns>A WebGLFramebuffer object.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createFramebuffer</remarks>
    public WebGLFramebuffer CreateFramebuffer() => InvokeBatched<WebGLFramebuffer>("createFramebuffer");

    /// <summary>
    /// deletes a given WebGLFramebuffer object. This method has no effect if the frame buffer has already been deleted.
    /// </summary>
    /// <param name="framebuffer">A WebGLFramebuffer object to delete.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/deleteFramebuffer</remarks>
    public async Task DeleteFramebuffer(WebGLFramebuffer framebuffer) => await Invoke("deleteFramebuffer", framebuffer);

    /// <summary>
    /// attaches a WebGLRenderbuffer object to a WebGLFramebuffer object.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.FRAMEBUFFER
    ///     Collection buffer data storage of color, alpha, depth and stencil buffers used as both a destination for drawing and as a source for reading (see below).
    /// 
    /// When using a WebGL 2 context, the following values are available additionally:
    /// - gl.DRAW_FRAMEBUFFER
    ///     Used as a destination for drawing operations such as gl.draw*, gl.clear* and gl.blitFramebuffer.
    /// 
    /// - gl.READ_FRAMEBUFFER
    ///     Used as a source for reading operations such as gl.readPixels and gl.blitFramebuffer.
    /// </param>
    /// <param name="attachment">
    /// A GLenum specifying the attachment point for the render buffer. Possible values:
    /// - gl.COLOR_ATTACHMENT0: color buffer.
    /// - gl.DEPTH_ATTACHMENT: depth buffer.
    /// - gl.DEPTH_STENCIL_ATTACHMENT: depth and stencil buffer.
    /// - gl.STENCIL_ATTACHMENT: stencil buffer.
    /// </param>
    /// <param name="renderbuffertarget">
    /// A GLenum specifying the binding point (target) for the render buffer. Possible values:
    /// - gl.RENDERBUFFER
    ///     Buffer data storage for single images in a renderable internal format.
    /// </param>
    /// <param name="renderbuffer">A WebGLRenderbuffer object to attach.</param>
    /// <exception cref="GL.INVALID_ENUM">If target is not gl.FRAMEBUFFER, gl.DRAW_FRAMEBUFFER, or gl.READ_FRAMEBUFFER.</exception>
    /// <exception cref="GL.INVALID_ENUM">If renderbuffertarget is not gl.RENDERBUFFER.</exception>
    /// <exception cref="GL.INVALID_ENUM">If attachment is not one of the allowed enums.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/framebufferRenderbuffer</remarks>
    public async Task FramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, WebGLRenderbuffer renderbuffer) => 
        await Invoke("framebufferRenderbuffer", target, attachment, renderbuffertarget, renderbuffer);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.FRAMEBUFFER
    ///     Collection buffer data storage of color, alpha, depth and stencil buffers used as both a destination for drawing and as a source for reading (see below).
    /// 
    /// When using a WebGL 2 context, the following values are available additionally:
    /// - gl.DRAW_FRAMEBUFFER
    ///     Used as a destination for drawing operations such as gl.draw*, gl.clear* and gl.blitFramebuffer.
    /// 
    /// - gl.READ_FRAMEBUFFER
    ///     Used as a source for reading operations such as gl.readPixels and gl.blitFramebuffer.
    /// </param>
    /// <param name="attachment">
    /// A GLenum specifying the attachment point for the render buffer. Possible values:
    /// - gl.COLOR_ATTACHMENT0: Attaches the texture to the framebuffer's color buffer.
    /// - gl.DEPTH_ATTACHMENT: Attaches the texture to the framebuffer's depth buffer.
    /// - gl.STENCIL_ATTACHMENT: Attaches the texture to the framebuffer's stencil buffer.
    /// </param>
    /// <param name="textarget">
    /// A GLenum specifying the texture target. Possible values:
    /// - gl.TEXTURE_2D: A 2D image.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_X: Image for the positive X face of the cube.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_X: Image for the negative X face of the cube.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Y: Image for the positive Y face of the cube.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Y: Image for the negative Y face of the cube.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Z: Image for the positive Z face of the cube.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Z: Image for the negative Z face of the cube.
    /// </param>
    /// <param name="texture">A WebGLTexture object whose image to attach.</param>
    /// <param name="level">A GLint specifying the mipmap level of the texture image to be attached. Must be 0.</param>
    /// <exception cref="GL.INVALID_ENUM">
    /// Error is thrown if
    /// - target is not gl.FRAMEBUFFER.
    /// - attachment is not one of the accepted attachment points.
    /// - textarget is not one of the accepted texture targets.
    /// </exception>
    /// <exception cref="GL.INVALID_VALUE">Error is thrown if level is not 0.</exception>
    /// <exception cref="GL.INVALID_OPERATION ">error is thrown if texture isn't 0 or the name of an existing texture object.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/framebufferTexture2D</remarks>
    public async Task FramebufferTexture2D(uint target, uint attachment, uint textarget, WebGLTexture texture, int level) => 
        await Invoke("framebufferTexture2D", target, attachment, textarget, texture, level);

    /// <summary>
    /// returns information about a framebuffer's attachment. Use GLFramebufferAttachmentParameter as pname to make it type safe.
    /// </summary>
    /// <typeparam name="T">The return type</typeparam>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.FRAMEBUFFER
    ///     Collection buffer data storage of color, alpha, depth and stencil buffers used as both a destination for drawing and as a source for reading (see below).
    /// 
    /// When using a WebGL 2 context, the following values are available additionally:
    /// - gl.DRAW_FRAMEBUFFER
    ///     Used as a destination for drawing operations such as gl.draw*, gl.clear* and gl.blitFramebuffer.
    /// 
    /// - gl.READ_FRAMEBUFFER
    ///     Used as a source for reading operations such as gl.readPixels and gl.blitFramebuffer.
    /// </param>
    /// <param name="attachment">
    /// A GLenum specifying the attachment point for the texture. Possible values:
    /// - gl.COLOR_ATTACHMENT0: Texture attachment for the framebuffer's color buffer.
    /// - gl.DEPTH_ATTACHMENT: Texture attachment for the framebuffer's depth buffer.
    /// - gl.STENCIL_ATTACHMENT: Texture attachment for the framebuffer's stencil buffer.
    /// - gl.DEPTH_STENCIL_ATTACHMENT: Texture attachment for both, the depth and stencil buffer.
    /// </param>
    /// <param name="pname">
    /// A GLenum specifying information to query. Possible values:
    /// - gl.FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE: The type which contains the attached image.
    /// - gl.FRAMEBUFFER_ATTACHMENT_OBJECT_NAME: The texture or renderbuffer of the attached image (WebGLRenderbuffer or WebGLTexture).
    /// - gl.FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL: Mipmap level. Default value: 0.
    /// - gl.FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE: The name of cube-map face of the texture.
    /// </param>
    /// <returns>Depends on the requested information (as specified with pname). Either a GLint, a GLenum, a WebGLRenderbuffer, or a WebGLTexture.</returns>
    /// <exception cref="GL.INVALID_VALUE">Thrown if target is not gl.FRAMEBUFFER, gl.DRAW_FRAMEBUFFER, gl.READ_FRAMEBUFFER or if attachment is not one of the accepted attachment points.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getFramebufferAttachmentParameter</remarks>
    public Proxy<T> GetFramebufferAttachmentParameter<T>(uint target, uint attachment, uint pname) => InvokeBatched<Proxy<T>>("getFramebufferAttachmentParameter", target, attachment, pname);

    /// <summary>
    /// returns information about a framebuffer's attachment.
    /// </summary>
    /// <typeparam name="T">The return type</typeparam>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.FRAMEBUFFER
    ///     Collection buffer data storage of color, alpha, depth and stencil buffers used as both a destination for drawing and as a source for reading (see below).
    /// 
    /// When using a WebGL 2 context, the following values are available additionally:
    /// - gl.DRAW_FRAMEBUFFER
    ///     Used as a destination for drawing operations such as gl.draw*, gl.clear* and gl.blitFramebuffer.
    /// 
    /// - gl.READ_FRAMEBUFFER
    ///     Used as a source for reading operations such as gl.readPixels and gl.blitFramebuffer.
    /// </param>
    /// <param name="attachment">
    /// A GLenum specifying the attachment point for the texture. Possible values:
    /// - gl.COLOR_ATTACHMENT0: Texture attachment for the framebuffer's color buffer.
    /// - gl.DEPTH_ATTACHMENT: Texture attachment for the framebuffer's depth buffer.
    /// - gl.STENCIL_ATTACHMENT: Texture attachment for the framebuffer's stencil buffer.
    /// - gl.DEPTH_STENCIL_ATTACHMENT: Texture attachment for both, the depth and stencil buffer.
    /// </param>
    /// <param name="pname">
    /// A GLenum specifying information to query. Possible values:
    /// - gl.FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE: The type which contains the attached image.
    /// - gl.FRAMEBUFFER_ATTACHMENT_OBJECT_NAME: The texture or renderbuffer of the attached image (WebGLRenderbuffer or WebGLTexture).
    /// - gl.FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL: Mipmap level. Default value: 0.
    /// - gl.FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE: The name of cube-map face of the texture.
    /// </param>
    /// <returns>Depends on the requested information (as specified with pname). Either a GLint, a GLenum, a WebGLRenderbuffer, or a WebGLTexture.</returns>
    /// <exception cref="GL.INVALID_VALUE">Thrown if target is not gl.FRAMEBUFFER, gl.DRAW_FRAMEBUFFER, gl.READ_FRAMEBUFFER or if attachment is not one of the accepted attachment points.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getFramebufferAttachmentParameter</remarks>
    public Proxy<T> GetFramebufferAttachmentParameter<T>(uint target, uint attachment, GLParameter<T> pname) => InvokeBatched<Proxy<T>>("getFramebufferAttachmentParameter", target, attachment, pname.Name);

    /// <summary>
    /// returns true if the passed WebGLFramebuffer is valid and false otherwise.
    /// </summary>
    /// <param name="framebuffer">A WebGLFramebuffer to check.</param>
    /// <returns>A GLboolean indicating whether or not the frame buffer is valid.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isFramebuffer</remarks>
    public BooleanProxy IsFramebuffer(WebGLFramebuffer framebuffer) => InvokeBatched<BooleanProxy>("isFramebuffer", framebuffer);

    /// <summary>
    /// reads a block of pixels from a specified rectangle of the current color framebuffer into a TypedArray or a DataView object.
    /// </summary>
    /// <param name="x">A GLint specifying the first horizontal pixel that is read from the lower left corner of a rectangular block of pixels.</param>
    /// <param name="y">A GLint specifying the first vertical pixel that is read from the lower left corner of a rectangular block of pixels.</param>
    /// <param name="width">A GLsizei specifying the width of the rectangle.</param>
    /// <param name="height">A GLsizei specifying the height of the rectangle.</param>
    /// <param name="format">
    /// A GLenum specifying the format of the pixel data. Possible values:
    /// - gl.ALPHA
    ///     Discards the red, green and blue components and reads the alpha component.
    /// - gl.RGB
    ///     Discards the alpha components and reads the red, green and blue components.
    /// - gl.RGBA
    ///     Red, green, blue and alpha components are read from the color buffer.
    /// </param>
    /// <param name="type">
    /// A GLenum specifying the data type of the pixel data. Possible values:
    /// - gl.UNSIGNED_BYTE
    /// - gl.UNSIGNED_SHORT_5_6_5
    /// - gl.UNSIGNED_SHORT_4_4_4_4
    /// - gl.UNSIGNED_SHORT_5_5_5_1
    /// - gl.FLOAT
    /// </param>
    /// <param name="pixels">
    /// TODO: reaturn the pixel array instead of passing it as this doesn't work bc of references
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/readPixels</remarks>
    public async Task ReadPixels(int x, int y, int width, int height, uint format, uint type, byte[] pixels) => await Invoke("readPixels", x, y, width, height, format, type, pixels);

#endregion

#region Renderbuffers

    /// <summary>
    /// binds a given WebGLRenderbuffer to a target, which must be gl.RENDERBUFFER.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.RENDERBUFFER
    ///     Buffer data storage for single images in a renderable internal format.
    /// </param>
    /// <param name="renderbuffer">A WebGLRenderbuffer object to bind.</param>
    /// <exception cref="GL.INVALID_ENUM">Error is thrown if target is not gl.RENDERBUFFER.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bindRenderbuffer</remarks>
    public async Task BindRenderbuffer(uint target, WebGLRenderbuffer renderbuffer) => await Invoke("bindRenderbuffer", target, renderbuffer);

    /// <summary>
    /// creates and initializes a WebGLRenderbuffer object.
    /// </summary>
    /// <returns>A WebGLRenderbuffer object that stores data such an image, or can be source or target of an rendering operation.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createRenderbuffer</remarks>
    public WebGLRenderbuffer CreateRenderbuffer() => InvokeBatched<WebGLRenderbuffer>("createRenderbuffer");

    /// <summary>
    /// returns information about the renderbuffer.Use GLRenderbufferParameter as pname to make it type safe.
    /// </summary>
    /// <typeparam name="T">the return type</typeparam>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.RENDERBUFFER
    ///     Buffer data storage for single images in a renderable internal format.
    /// </param>
    /// <param name="pname">A GLenum specifying the information to query. check documentation for more information</param>
    /// <returns>Depends on the requested information (as specified with pname). Either a GLint or a GLenum.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getRenderbufferParameter</remarks>
    public Proxy<T> GetRenderbufferParameter<T>(uint target, uint pname) => InvokeBatched<Proxy<T>>("getRenderbufferParameter", target, pname);

    /// <summary>
    /// returns information about the renderbuffer.
    /// </summary>
    /// <typeparam name="T">the return type</typeparam>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.RENDERBUFFER
    ///     Buffer data storage for single images in a renderable internal format.
    /// </param>
    /// <param name="pname">A GLenum specifying the information to query. check documentation for more information</param>
    /// <returns>Depends on the requested information (as specified with pname). Either a GLint or a GLenum.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getRenderbufferParameter</remarks>
    public Proxy<T> GetRenderbufferParameter<T>(uint target, GLParameter<T> pname) => InvokeBatched<Proxy<T>>("getRenderbufferParameter", target, pname.Name);

    /// <summary>
    ///  returns true if the passed WebGLRenderbuffer is valid and false otherwise.
    /// </summary>
    /// <param name="renderbuffer">A WebGLRenderbuffer to check.</param>
    /// <returns>A GLboolean indicating whether or not the renderbuffer is valid.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isRenderbuffer</remarks>
    public BooleanProxy IsRenderbuffer(WebGLRenderbuffer renderbuffer) => InvokeBatched<BooleanProxy>("isRenderbuffer", renderbuffer);

    /// <summary>
    /// creates and initializes a renderbuffer object's data store.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.RENDERBUFFER
    ///     Buffer data storage for single images in a renderable internal format.
    /// </param>
    /// <param name="internalformat">
    /// A GLenum specifying the internal format of the renderbuffer. Possible values:
    /// - gl.RGBA4: 4 red bits, 4 green bits, 4 blue bits 4 alpha bits.
    /// - gl.RGB565: 5 red bits, 6 green bits, 5 blue bits.
    /// - gl.RGB5_A1: 5 red bits, 5 green bits, 5 blue bits, 1 alpha bit.
    /// - gl.DEPTH_COMPONENT16: 16 depth bits.
    /// - gl.STENCIL_INDEX8: 8 stencil bits.
    /// - gl.DEPTH_STENCIL
    /// </param>
    /// <param name="width">A GLsizei specifying the width of the renderbuffer in pixels.</param>
    /// <param name="height">A GLsizei specifying the height of the renderbuffer in pixels.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/renderbufferStorage</remarks>
    public async Task RenderbufferStorage(uint target, uint internalformat, int width, int height) => await Invoke("renderbufferStorage", target, internalformat, width, height);

#endregion

#region Textures

    /// <summary>
    /// binds a given WebGLTexture to a target (binding point).
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP: A cube-mapped texture. When using a WebGL 2 context, the following values are available additionally:
    /// - gl.TEXTURE_3D: A three-dimensional texture.
    /// - gl.TEXTURE_2D_ARRAY: A two-dimensional array texture.
    /// </param>
    /// <param name="texture">A WebGLTexture object to bind.</param>
    /// <exception cref="GL.INVALID_ENUM">Thrown if target is not gl.TEXTURE_2D, gl.TEXTURE_CUBE_MAP, gl.TEXTURE_3D, or gl.TEXTURE_2D_ARRAY.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bindTexture</remarks>
    public async Task BindTexture(uint target, WebGLTexture texture) => await Invoke("bindTexture", target, texture);

    /// <summary>
    /// interface of the WebGL API specifies a two-dimensional texture image in a compressed format. 
    /// Compressed image formats must be enabled by WebGL extensions before using these methods.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target) of the active texture. Possible values for compressedTexImage2D:
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_X: Positive X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_X: Negative X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Y: Positive Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Y: Negative Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Z: Positive Z face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Z: Negative Z face for a cube-mapped texture.
    /// </param>
    /// <param name="level">A GLint specifying the level of detail. Level 0 is the base image level and level n is the n-th mipmap reduction level.</param>
    /// <param name="internalformat">
    /// A GLenum specifying the compressed image format. Compressed image formats must be enabled by WebGL extensions before using this method. 
    /// All values are possible for compressedTexImage2D.
    /// </param>
    /// <param name="width">A GLsizei specifying the width of the texture.</param>
    /// <param name="height">A GLsizei specifying the height of the texture.</param>
    /// <param name="border">A GLint specifying the width of the border. Must be 0.</param>
    /// <param name="pixels">A TypedArray or a DataView that will be used as a data store for the compressed image data in memory.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/compressedTexImage2D</remarks>
    public async Task CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, byte[] pixels) => await Invoke("compressedTexImage2D", target, level, internalformat, width, height, border, pixels);

    /// <summary>
    /// interface of the WebGL API specifies a two-dimensional texture image in a compressed format. 
    /// Compressed image formats must be enabled by WebGL extensions before using these methods.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target) of the active texture. Possible values for compressedTexImage2D:
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_X: Positive X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_X: Negative X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Y: Positive Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Y: Negative Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Z: Positive Z face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Z: Negative Z face for a cube-mapped texture.
    /// </param>
    /// <param name="level">A GLint specifying the level of detail. Level 0 is the base image level and level n is the n-th mipmap reduction level.</param>
    /// <param name="internalformat">
    /// A GLenum specifying the compressed image format. Compressed image formats must be enabled by WebGL extensions before using this method. 
    /// All values are possible for compressedTexImage2D.
    /// </param>
    /// <param name="width">A GLsizei specifying the width of the texture.</param>
    /// <param name="height">A GLsizei specifying the height of the texture.</param>
    /// <param name="border">A GLint specifying the width of the border. Must be 0.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/compressedTexImage2D</remarks>
    public async Task CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border) => await Invoke("compressedTexImage2D", target, level, internalformat, width, height, border);

    /// <summary>
    /// specifies a two-dimensional sub-rectangle for a texture image in a compressed format.
    /// Compressed image formats must be enabled by WebGL extensions before using this method or a WebGL2RenderingContext must be used.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target) of the active compressed texture. Possible values:
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_X: Positive X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_X: Negative X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Y: Positive Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Y: Negative Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Z: Positive Z face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Z: Negative Z face for a cube-mapped texture.
    /// </param>
    /// <param name="level">A GLint specifying the level of detail. Level 0 is the base image level and level n is the n-th mipmap reduction level.</param>
    /// <param name="xoffser">A GLint specifying the horizontal offset within the compressed texture image.</param>
    /// <param name="yoffset">A GLint specifying the vertical offset within the compressed texture image.</param>
    /// <param name="width">A GLsizei specifying the width of the compressed texture.</param>
    /// <param name="height">A GLsizei specifying the height of the compressed texture.</param>
    /// <param name="format">A GLenum specifying the compressed image format. Compressed image formats must be enabled by WebGL extensions before using this method.</param>
    /// <param name="srcData">A TypedArray or a DataView that will be used as a data store for the compressed image data in memory.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/compressedTexSubImage2D</remarks>
    public async Task CompressedTexSubImage2D(uint target, int level, int xoffser, int yoffset, int width, int height, uint format, byte[] srcData) => await Invoke("compressedTexSubImage2D", target, level, xoffser, yoffset, width, height, format, srcData);

    /// <summary>
    /// copies pixels from the current WebGLFramebuffer into a 2D texture image.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target) of the active texture. Possible values:
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_X: Positive X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_X: Negative X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Y: Positive Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Y: Negative Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Z: Positive Z face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Z: Negative Z face for a cube-mapped texture.
    /// </param>
    /// <param name="level">A GLint specifying the level of detail. Level 0 is the base image level and level n is the n-th mipmap reduction level.</param>
    /// <param name="internalformat">A GLenum specifying the color components in the texture.</param>
    /// <param name="x">A GLint specifying the x coordinate of the lower left corner where to start copying.</param>
    /// <param name="y">A GLint specifying the y coordinate of the lower left corner where to start copying.</param>
    /// <param name="width">A GLsizei specifying the width of the texture.</param>
    /// <param name="height">A GLsizei specifying the height of the texture.</param>
    /// <param name="border">A GLint specifying the width of the border. Must be 0.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/copyTexImage2D</remarks>
    public async Task CopyTexImage2D(uint target, int level, uint internalformat, int x, int y, int width, int height, int border) => await Invoke("copyTexImage2D", target, level, internalformat, x, y, width, height, border);

    /// <summary>
    /// Copies pixels from the current WebGLFramebuffer into an existing 2D texture sub-image.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target) of the active texture. Possible values:
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_X: Positive X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_X: Negative X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Y: Positive Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Y: Negative Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Z: Positive Z face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Z: Negative Z face for a cube-mapped texture.
    /// </param>
    /// <param name="level">A GLint specifying the level of detail. Level 0 is the base image level and level n is the n-th mipmap reduction level.</param>
    /// <param name="xoffset">A GLint specifying the horizontal offset within the texture image.</param>
    /// <param name="yoffset">A GLint specifying the vertical offset within the texture image.</param>
    /// <param name="x">A GLint specifying the x coordinate of the lower left corner where to start copying.</param>
    /// <param name="y">A GLint specifying the y coordinate of the lower left corner where to start copying.</param>
    /// <param name="width">A GLsizei specifying the width of the texture.</param>
    /// <param name="height">A GLsizei specifying the height of the texture.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/copyTexSubImage2D</remarks>
    public async Task CopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height) => await Invoke("copyTexSubImage2D", target, level, xoffset, yoffset, x, y, width, height);

    /// <summary>
    /// Creates and initializes a WebGLTexture object.
    /// </summary>
    /// <returns>A WebGLTexture object to which images can be bound to.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createTexture</remarks>
    public WebGLTexture CreateTexture() => InvokeBatched<WebGLTexture>("createTexture");

    /// <summary>
    /// deletes a given WebGLTexture object. This method has no effect if the texture has already been deleted.
    /// </summary>
    /// <param name="texture">A WebGLTexture object to delete.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/deleteTexture</remarks>
    public async Task DeleteTexture(WebGLTexture texture) => await Invoke("deleteTexture", texture);

    /// <summary>
    /// generates a set of mipmaps for a WebGLTexture object. 
    /// 
    /// Mipmaps are used to create distance with objects. A higher-resolution mipmap is used for objects that are closer, 
    /// and a lower-resolution mipmap is used for objects that are farther away. 
    /// It starts with the resolution of the texture image and halves the resolution until a 1x1 dimension texture image is created.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target) of the active texture whose mipmaps will be generated. Possible values:
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP: A cube-mapped texture.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/generateMipmap</remarks>
    public async Task GenerateMipmap(uint target) => await Invoke("generateMipmap", target);

    /// <summary>
    /// returns information about the given texture. use GLTextureParameter as pname to make it type safe.
    /// </summary>
    /// <typeparam name="T">the return type of the parameter</typeparam>
    /// <param name="target">
    /// A GLenum specifying the binding point (target) of the active texture whose mipmaps will be generated. Possible values:
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP: A cube-mapped texture.
    /// </param>
    /// <param name="pname">A GLenum specifying the information to query</param>
    /// <returns>The requested texture information (as specified with pname). If an error occurs, null is returned.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getTexParameter</remarks>
    public Proxy<T> GetTexParameter<T>(uint target, uint pname) => InvokeBatched<Proxy<T>>("getTexParameter", target, pname);

    
    /// <summary>
    /// returns information about the given texture. use GLTextureParameter as pname to make it type safe.
    /// </summary>
    /// <typeparam name="T">the return type of the parameter</typeparam>
    /// <param name="target">
    /// A GLenum specifying the binding point (target) of the active texture whose mipmaps will be generated. Possible values:
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP: A cube-mapped texture.
    /// </param>
    /// <param name="pname">A GLenum specifying the information to query</param>
    /// <returns>The requested texture information (as specified with pname). If an error occurs, null is returned.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getTexParameter</remarks>
    public Proxy<T> GetTexParameter<T>(uint target, GLParameter<T> pname) => InvokeBatched<Proxy<T>>("getTexParameter", target, pname);

    /// <summary>
    /// returns true if the passed WebGLTexture is valid and false otherwise.
    /// </summary>
    /// <param name="texture">A WebGLTexture to check.</param>
    /// <returns>A GLboolean indicating whether or not the texture is valid.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isTexture</remarks>
    public BooleanProxy IsTexture(WebGLTexture texture) => InvokeBatched<BooleanProxy>("isTexture", texture);

    /// <summary>
    /// specifies a two-dimensional texture image.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target) of the active texture. Possible values:/
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_X: Positive X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_X: Negative X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Y: Positive Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Y: Negative Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Z: Positive Z face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Z: Negative Z face for a cube-mapped texture.
    /// </param>
    /// <param name="level">A GLint specifying the level of detail. Level 0 is the base image level and level n is the n-th mipmap reduction level</param>
    /// <param name="internalformat">A GLenum specifying the color components in the texture.</param>
    /// <param name="width">A GLsizei specifying the width of the texture.</param>
    /// <param name="height">A GLsizei specifying the height of the texture.</param>
    /// <param name="border">A GLint specifying the width of the border. Must be 0.</param>
    /// <param name="format">A GLenum specifying the format of the texel data. In WebGL 1, this must be the same as internalformat</param>
    /// <param name="type">A GLenum specifying the data type of the texel data.</param>
    /// <param name="pixels"></param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texImage2D</remarks>
    public async Task TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, byte[] pixels) => await Invoke("texImage2D", target, level, internalformat, width, height, border, format, type, pixels);
    
    /// <summary>
    /// specifies a two-dimensional texture image.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target) of the active texture. Possible values:/
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_X: Positive X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_X: Negative X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Y: Positive Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Y: Negative Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Z: Positive Z face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Z: Negative Z face for a cube-mapped texture.
    /// </param>
    /// <param name="level">A GLint specifying the level of detail. Level 0 is the base image level and level n is the n-th mipmap reduction level</param>
    /// <param name="internalformat">A GLenum specifying the color components in the texture.</param>
    /// <param name="format">A GLenum specifying the format of the texel data. In WebGL 1, this must be the same as internalformat</param>
    /// <param name="type">A GLenum specifying the data type of the texel data.</param>
    /// <param name="pixels">
    /// The following types can always be used as a pixel source for the texture:
    /// - ImageData,
    /// - HTMLImageElement,
    /// - HTMLCanvasElement,
    /// - HTMLVideoElement,
    /// - ImageBitmap.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texImage2D</remarks>
    public async Task TexImage2D(uint target, int level, uint internalformat, uint format, uint type, ElementReference pixels) =>
        await Invoke("texImage2D", target, level, internalformat, format, type, pixels);

    /// <summary>
    /// specifies a sub-rectangle of the current texture.
    /// </summary><param name="target">
    /// A GLenum specifying the binding point (target) of the active texture. Possible values:/
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_X: Positive X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_X: Negative X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Y: Positive Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Y: Negative Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Z: Positive Z face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Z: Negative Z face for a cube-mapped texture.
    /// </param>
    /// <param name="level">A GLint specifying the level of detail. Level 0 is the base image level and level n is the n-th mipmap reduction level</param>
    /// <param name="xoffset">A GLint specifying the lower left texel x coordinate of a width-wide by height-wide rectangular subregion of the texture array.</param>
    /// <param name="yoffset">A GLint specifying the lower left texel y coordinate of a width-wide by height-wide rectangular subregion of the texture array.</param>
    /// <param name="width">A GLsizei specifying the width of the texture in texels.</param>
    /// <param name="height">A GLsizei specifying the height of the texture in texels.</param>
    /// <param name="format">A GLenum specifying the format of the texel data. In WebGL 1, this must be the same as internalformat</param>
    /// <param name="type">A GLenum specifying the data type of the texel data.</param>
    /// <param name="pixels">
    /// The following types can always be used as a pixel source for the texture:
    /// - ImageData,
    /// - HTMLImageElement,
    /// - HTMLCanvasElement,
    /// - HTMLVideoElement,
    /// - ImageBitmap.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texImage2D</remarks>
    public async Task TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, ElementReference pixels)  =>
        await Invoke("texSubImage2D", target, level, xoffset, yoffset, width, height, format, type, pixels);

    /// <summary>
    /// specifies a sub-rectangle of the current texture.
    /// </summary><param name="target">
    /// A GLenum specifying the binding point (target) of the active texture. Possible values:/
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_X: Positive X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_X: Negative X face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Y: Positive Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Y: Negative Y face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_POSITIVE_Z: Positive Z face for a cube-mapped texture.
    /// - gl.TEXTURE_CUBE_MAP_NEGATIVE_Z: Negative Z face for a cube-mapped texture.
    /// </param>
    /// <param name="level">A GLint specifying the level of detail. Level 0 is the base image level and level n is the n-th mipmap reduction level</param>
    /// <param name="xoffset">A GLint specifying the lower left texel x coordinate of a width-wide by height-wide rectangular subregion of the texture array.</param>
    /// <param name="yoffset">A GLint specifying the lower left texel y coordinate of a width-wide by height-wide rectangular subregion of the texture array.</param>
    /// <param name="format">A GLenum specifying the format of the texel data. In WebGL 1, this must be the same as internalformat</param>
    /// <param name="type">A GLenum specifying the data type of the texel data.</param>
    /// <param name="pixels">
    /// The following types can always be used as a pixel source for the texture:
    /// - ImageData,
    /// - HTMLImageElement,
    /// - HTMLCanvasElement,
    /// - HTMLVideoElement,
    /// - ImageBitmap.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texImage2D</remarks>
    public async Task TexSubImage2D(uint target, int level, int xoffset, int yoffset, uint format, uint type, ElementReference pixels)  =>
        await Invoke("texSubImage2D", target, level, xoffset, yoffset, format, type, pixels);

    /// <summary>
    /// set texture parameters.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP: A cube-mapped texture.
    /// </param>
    /// <param name="pname">The param parameter is a GLfloat specifying the value for the specified parameter</param>
    /// <param name="param">The pname parameter is a GLenum specifying the texture parameter to set.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texParameter</remarks>
    public async Task TexParameterf(uint target, uint pname, float param) => await Invoke("texParameterf", target, pname, param);

    
    /// <summary>
    /// set texture parameters.
    /// </summary>
    /// <param name="target">
    /// A GLenum specifying the binding point (target). Possible values:
    /// - gl.TEXTURE_2D: A two-dimensional texture.
    /// - gl.TEXTURE_CUBE_MAP: A cube-mapped texture.
    /// </param>
    /// <param name="pname">The param parameter is a GLint specifying the value for the specified parameter</param>
    /// <param name="param">The pname parameter is a GLenum specifying the texture parameter to set.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/texParameter</remarks>
    public async Task TexParameteri(uint target, uint pname, int param) => await Invoke("texParameterf", target, pname, param);

#endregion

#region Programs and shaders

    /// <summary>
    /// attaches either a fragment or vertex WebGLShader to a WebGLProgram.
    /// </summary>
    /// <param name="program">A WebGLProgram.</param>
    /// <param name="shader">A fragment or vertex WebGLShader.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/attachShader</remarks>
    public async Task AttachShader(WebGLProgram program, WebGLShader shader) => await Invoke("attachShader", program, shader);

    /// <summary>
    /// binds a generic vertex index to an attribute variable.
    /// </summary>
    /// <param name="program">A WebGLProgram object to bind.</param>
    /// <param name="index">A GLuint specifying the index of the generic vertex to bind.</param>
    /// <param name="name">
    /// A string specifying the name of the variable to bind to the generic vertex index. 
    /// This name cannot start with "webgl_" or "_webgl_", as these are reserved for use by WebGL.
    /// </param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/bindAttribLocation</remarks>
    public async Task BindAttribLocation(WebGLProgram program, uint index, string name) => await Invoke("bindAttribLocation", program, index, name);

    /// <summary>
    /// compiles a GLSL shader into binary data so that it can be used by a WebGLProgram.
    /// </summary>
    /// <param name="shader">A fragment or vertex WebGLShader.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/compileShader</remarks>
    public async Task CompileShader(WebGLShader shader) => await Invoke("compileShader", shader);

    /// <summary>
    /// creates and initializes a WebGLProgram object.
    /// </summary>
    /// <returns>
    /// A WebGLProgram object that is a combination of two compiled WebGLShaders consisting of a vertex shader and a fragment shader (both written in GLSL).
    /// These are then linked into a usable program.
    /// </returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createProgram</remarks>
    public WebGLProgram CreateProgram() => InvokeBatched<WebGLProgram>("createProgram");

    /// <summary>
    /// creates a WebGLShader that can then be configured further using WebGLRenderingContext.shaderSource() and WebGLRenderingContext.compileShader().
    /// </summary>
    /// <param name="type">Either gl.VERTEX_SHADER or gl.FRAGMENT_SHADER</param>
    /// <returns>A new (WebGLShader).</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/createShader</remarks>
    public WebGLShader CreateShader(uint type) => InvokeBatched<WebGLShader>("createShader", type);

    /// <summary>
    /// deletes a given WebGLProgram object. This method has no effect if the program has already been deleted.
    /// </summary>
    /// <param name="program">A WebGLProgram object to delete.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/deleteProgram</remarks>
    public async Task DeleteProgram(WebGLProgram program) => await Invoke("deleteProgram", program);

    /// <summary>
    /// marks a given WebGLShader object for deletion. It will then be deleted whenever the shader is no longer in use. 
    /// This method has no effect if the shader has already been deleted, 
    /// and the WebGLShader is automatically marked for deletion when it is destroyed by the garbage collector.
    /// </summary>
    /// <param name="shader">A WebGLShader object to delete.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/deleteShader</remarks>
    public async Task DeleteShader(WebGLShader shader) => await Invoke("deleteShader", shader);

    /// <summary>
    /// detaches a previously attached WebGLShader from a WebGLProgram.
    /// </summary>
    /// <param name="program">A WebGLProgram.</param>
    /// <param name="shader">A fragment or vertex WebGLShader.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/detachShader</remarks>
    public async Task DetachShader(WebGLProgram program, WebGLShader shader) => await Invoke("detachShader", program, shader);

    /// <summary>
    /// returns a list of WebGLShader objects attached to a WebGLProgram.
    /// </summary>
    /// <param name="program">A WebGLProgram object to get attached shaders for.</param>
    /// <returns>An Array of WebGLShader objects that are attached to the given WebGLProgram.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getAttachedShaders</remarks>
    public ArrayProxy<WebGLShader> GetAttachedShaders(WebGLProgram program) => InvokeBatched<ArrayProxy<WebGLShader>>("getAttachedShaders", program);

    /// <summary>
    /// returns information about the given program. use GLProgramParameters to make it type safe.
    /// </summary>
    /// <typeparam name="T">the return type of the parameter</typeparam>
    /// <param name="program">A WebGLProgram to get parameter information from.</param>
    /// <param name="pname">A GLenum specifying the information to query. Use GLProgramParameters to get strong typed returns</param>
    /// <returns>Returns the requested program information (as specified with pname).</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getProgramParameter</remarks>
    public Proxy<T> GetProgramParameter<T>(WebGLProgram program, uint pname) => InvokeBatched<Proxy<T>>("getProgramParameter", program, pname);

    /// <summary>
    /// returns information about the given program.
    /// </summary>
    /// <typeparam name="T">the return type of the parameter</typeparam>
    /// <param name="program">A WebGLProgram to get parameter information from.</param>
    /// <param name="pname">A GLenum specifying the information to query. Use GLProgramParameters to get strong typed returns</param>
    /// <returns>Returns the requested program information (as specified with pname).</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getProgramParameter</remarks>
    public Proxy<T> GetProgramParameter<T>(WebGLProgram program, GLParameter<T> pname) => InvokeBatched<Proxy<T>>("getProgramParameter", program, pname);

    /// <summary>
    /// returns the information log for the specified WebGLProgram object. It contains errors that occurred during failed linking or validation of WebGLProgram objects.
    /// </summary>
    /// <param name="program">The WebGLProgram to query.</param>
    /// <returns>
    /// A string that contains diagnostic messages, warning messages, and other information about the last linking or validation operation. 
    /// When a WebGLProgram object is initially created, its information log will be a string of length 0.
    /// </returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getProgramInfoLog</remarks>
    public StringProxy GetProgramInfoLog(WebGLProgram program) => InvokeBatched<StringProxy>("getProgramInfoLog", program);	

    /// <summary>
    /// returns information about the given shader. use GLShaderParameters to make it type safe.
    /// </summary>
    /// <typeparam name="T">the return type of the parameter</typeparam>
    /// <param name="shader">A WebGLShader to get parameter information from.</param>
    /// <param name="pname">A GLenum specifying the information to query. use GLShaderParameters</param>
    /// <returns>Returns the requested shader information (as specified with pname).</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getShaderParameter</remarks>
    public Proxy<T> GetShaderParameter<T>(WebGLShader shader, uint pname) => InvokeBatched<Proxy<T>>("getShaderParameter", shader, pname);

    /// <summary>
    /// returns information about the given shader. Use GLShaderParameters.
    /// </summary>
    /// <typeparam name="T">the return type of the parameter</typeparam>
    /// <param name="shader">A WebGLShader to get parameter information from.</param>
    /// <param name="pname">A GLenum specifying the information to query. use GLShaderParameters</param>
    /// <returns>Returns the requested shader information (as specified with pname).</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getShaderParameter</remarks>
    public Proxy<T> GetShaderParameter<T>(WebGLShader shader, GLParameter<T> pname) => InvokeBatched<Proxy<T>>("getShaderParameter", shader, pname);

    /// <summary>
    /// returns a new WebGLShaderPrecisionFormat object describing the range and precision for the specified shader
    /// </summary>
    /// <param name="shadertype">Either a gl.FRAGMENT_SHADER or a gl.VERTEX_SHADER.</param>
    /// <param name="precisiontype">A precision type value. Either gl.LOW_FLOAT, gl.MEDIUM_FLOAT, gl.HIGH_FLOAT, gl.LOW_INT, gl.MEDIUM_INT, or gl.HIGH_INT.</param>
    /// <returns>A WebGLShaderPrecisionFormat object or null, if an error occurs.</returns>
    /// <exception cref="GL.INVALID_ENUM">If the shader or precision types aren't recognized.</exception>
    /// <exception cref="GL.INVALID_OPERATION">If the shader compiler isn't supported.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getShaderPrecisionFormat</remarks>
    public WebGLShaderPrecisionFormat GetShaderPrecisionFormat(uint shadertype, uint precisiontype) => 
        InvokeBatched<WebGLShaderPrecisionFormat>("getShaderPrecisionFormat", shadertype, precisiontype);

    /// <summary>
    /// returns the information log for the specified WebGLShader object. It contains warnings, debugging and compile information.
    /// </summary>
    /// <param name="shader">A WebGLShader to query.</param>
    /// <returns>
    /// A string that contains diagnostic messages, warning messages, and other information about the last compile operation. 
    /// When a WebGLShader object is initially created, its information log will be a string of length 0.
    /// </returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getShaderInfoLog</remarks>
    public StringProxy GetShaderInfoLog(WebGLShader shader) => InvokeBatched<StringProxy>("getShaderInfoLog", shader);

    /// <summary>
    /// returns the source code of a WebGLShader as a string.
    /// </summary>
    /// <param name="shader">A WebGLShader object to get the source code from.</param>
    /// <returns>A string containing the source code of the shader.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getShaderSource</remarks>
    public StringProxy GetShaderSource(WebGLShader shader) => InvokeBatched<StringProxy>("getShaderSource", shader);

    /// <summary>
    /// returns true if the passed WebGLProgram is valid, false otherwise.
    /// </summary>
    /// <param name="program">A WebGLProgram to check.</param>
    /// <returns>A GLboolean indicating whether or not the program is valid.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isProgram</remarks>
    public BooleanProxy IsProgram(WebGLProgram program) => InvokeBatched<BooleanProxy>("isProgram", program);

    /// <summary>
    /// returns true if the passed WebGLShader is valid, false otherwise.
    /// </summary>
    /// <param name="shader">A WebGLShader to check.</param>
    /// <returns>A GLboolean indicating whether or not the shader is valid.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/isShader</remarks>
    public BooleanProxy IsShader(WebGLShader shader) => InvokeBatched<BooleanProxy>("isShader", shader);

    /// <summary>
    /// links a given WebGLProgram, completing the process of preparing the GPU code for the program's fragment and vertex shaders.
    /// </summary>
    /// <param name="program">The WebGLProgram to link.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/linkProgram</remarks>
    public async Task LinkProgram(WebGLProgram program) => await Invoke("linkProgram", program);

    /// <summary>
    /// sets the source code of a WebGLShader.
    /// </summary>
    /// <param name="shader">A WebGLShader object in which to set the source code.</param>
    /// <param name="source">A string containing the GLSL source code to set.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/shaderSource</remarks>
    public async Task ShaderSource(WebGLShader shader, string source) => await Invoke("shaderSource", shader, source);

    /// <summary>
    /// sets the specified WebGLProgram as part of the current rendering state.
    /// </summary>
    /// <param name="program">A WebGLProgram to use.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/useProgram</remarks>
    public async Task UseProgram(WebGLProgram program) => await Invoke("useProgram", program);

    /// <summary>
    /// validates a WebGLProgram. It checks if it is successfully linked and if it can be used in the current WebGL state.
    /// </summary>
    /// <param name="program">A WebGLProgram to validate.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/validateProgram</remarks>
    public async Task ValidateProgram(WebGLProgram program) => await Invoke("validateProgram", program);

#endregion

#region Uniforms and attributes

    /// <summary>
    /// turns the generic vertex attribute array off at a given index position.
    /// </summary>
    /// <param name="index">A GLuint specifying the index of the vertex attribute to disable.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/disableVertexAttribArray</remarks>
    public async Task DisableVertexAttribArray(uint index) => await Invoke("disableVertexAttribArray", index);

    /// <summary>
    /// turns on the generic vertex attribute array at the specified index into the list of attribute arrays. (more info on MDN)
    /// </summary>
    /// <param name="index">
    /// A GLuint specifying the index number that uniquely identifies the vertex attribute to enable. 
    /// If you know the name of the attribute but not its index, 
    /// you can get the index by calling getAttribLocation().
    /// </param>
    /// <exception cref="GL.INVALID_VALUE">
    /// The specified index is invalid; that is, it's greater than or equal to the maximum number of entries permitted in the context's vertex attribute list, 
    /// as indicated by the value of WebGLRenderingContext.MAX_VERTEX_ATTRIBS.
    /// </exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/enableVertexAttribArray</remarks>
    public async Task EnableVertexAttribArray(uint index) => await Invoke("enableVertexAttribArray", index);

    /// <summary>
    ///  returns a WebGLActiveInfo object containing size, type, and name of a vertex attribute. 
    ///  It is generally used when querying unknown attributes either for debugging or generic library creation.
    /// </summary>
    /// <param name="program">A WebGLProgram containing the vertex attribute.</param>
    /// <param name="index">
    /// A GLuint specifying the index of the vertex attribute to get. 
    /// This value is an index 0 to N - 1 as returned by gl.getProgramParameter(program, gl.ACTIVE_ATTRIBUTES).
    /// </param>
    /// <returns>A WebGLActiveInfo object.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getActiveAttrib</remarks>
    public Proxy<WebGLActiveInfo> GetActiveAttrib(WebGLProgram program, uint index) => InvokeBatched<Proxy<WebGLActiveInfo>>("getActiveAttrib", program, index);

    /// <summary>
    /// returns a WebGLActiveInfo object containing size, type, and name of a uniform attribute. 
    /// It is generally used when querying unknown uniforms either for debugging or generic library creation.
    /// </summary>
    /// <param name="program">A WebGLProgram specifying the WebGL shader program from which to obtain the uniform variable's information.</param>
    /// <param name="index">A GLuint specifying the index of the uniform attribute to get. This value is an index 0 to N - 1 as returned by gl.getProgramParameter(program, gl.ACTIVE_UNIFORMS).</param>
    /// <returns>A WebGLActiveInfo object describing the uniform.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getActiveUniform</remarks>
    public Proxy<WebGLActiveInfo> GetActiveUniform(WebGLProgram program, uint index) => InvokeBatched<Proxy<WebGLActiveInfo>>("getActiveUniform", program, index);

    /// <summary>
    /// returns the location of an attribute variable in a given WebGLProgram.
    /// </summary>
    /// <param name="program">A WebGLProgram containing the attribute variable.</param>
    /// <param name="name">A string specifying the name of the attribute variable whose location to get.</param>
    /// <returns>A GLint number indicating the location of the variable name if found. Returns -1 otherwise.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getAttribLocation</remarks>
    public BinaryNumberProxy<int> GetAttribLocation(WebGLProgram program, string name) => InvokeBatched<BinaryNumberProxy<int>>("getAttribLocation", program, name);

    /// <summary>
    /// returns the value of a uniform variable at a given location.
    /// </summary>
    /// <typeparam name="T">The type of the uniform</typeparam>
    /// <param name="program">A WebGLProgram containing the uniform attribute.</param>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to get.</param>
    /// <returns>The returned type depends on the uniform type</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getUniform</remarks>
    public Proxy<T> GetUniform<T>(WebGLProgram program, WebGLUniformLocation location) => InvokeBatched<Proxy<T>>("getUniform", program, location);

    /// <summary>
    /// returns the location of a specific uniform variable which is part of a given WebGLProgram.
    /// 
    /// The uniform variable is returned as a WebGLUniformLocation object, which is an opaque identifier used to specify where in the GPU's memory that uniform variable is located.
    /// </summary>
    /// <param name="program">The WebGLProgram in which to locate the specified uniform variable.</param>
    /// <param name="name">
    /// A string specifying the name of the uniform variable whose location is to be returned. 
    /// The name can't have any whitespace in it, and you can't use this function to get the location of any uniforms starting with the reserved string "gl_", 
    /// since those are internal to the WebGL layer.
    /// </param>
    /// <returns>A WebGLUniformLocation value indicating the location of the named variable, if it exists. If the specified variable doesn't exist, null is returned instead.</returns>
    /// <exception cref="GL.INVALID_VALUE">TThe program parameter is not a value or object generated by WebGL.</exception>
    /// <exception cref="GL.INVALID_OPERATION">The program parameter doesn't correspond to a GLSL program generated by WebGL, or the specified program hasn't been linked successfully.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getUniformLocation</remarks>
    public WebGLUniformLocation GetUniformLocation(WebGLProgram program, string name) => InvokeBatched<WebGLUniformLocation>("getUniformLocation", program, name);

    /// <summary>
    /// returns information about a vertex attribute at a given position. Use GLVertexAttribParameters to make it type safe.
    /// </summary>
    /// <typeparam name="T">the return type of the parameter</typeparam>
    /// <param name="index">A GLuint specifying the index of the vertex attribute.</param>
    /// <param name="pname">A GLenum specifying the information to query.</param>
    /// <returns>Returns the requested vertex attribute information</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getVertexAttrib</remarks>
    public Proxy<T> GetVertexAttrib<T>(uint index, uint pname) => InvokeBatched<Proxy<T>>("getVertexAttrib", index, pname);


    /// <summary>
    /// returns the address of a specified vertex attribute. Use GLVertexAttribParameters to make it type safe.
    /// </summary>
    /// <param name="index">A GLuint specifying the index of the vertex attribute.</param>
    /// <param name="pname">A GLenum which must be gl.VERTEX_ATTRIB_ARRAY_POINTER.</param>
    /// <returns>A GLintptr indicating the address of the vertex attribute.</returns>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getVertexAttribOffset</remarks>
    public BinaryNumberProxy<int> GetVertexAttribOffset(int index, uint pname) => InvokeBatched<BinaryNumberProxy<int>>("getVertexAttribOffset", index, pname);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="v0">A floating point Number for floating point values</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform1f(WebGLUniformLocation location, float v0) => await Invoke("uniform1f", location, v0);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="value">A sequence of floating point numbers for floating point vector methods</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform1fv(WebGLUniformLocation location, float[] value) => await Invoke("uniform1fv", location, value);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="v0">An integer Number for integer values</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform1i(WebGLUniformLocation location, int v0) => await Invoke("uniform1i", location, v0);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="value">An Int32Array for integer vector methods</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform1iv(WebGLUniformLocation location, int[] value) => await Invoke("uniform1iv", location, value);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="v0">A floating point Number for floating point values</param>
    /// <param name="v1">A floating point Number for floating point values</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform2f(WebGLUniformLocation location, float v0, float v1) => await Invoke("uniform2f", location, v0, v1);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="value">A sequence of floating point numbers for floating point vector methods</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform2fv(WebGLUniformLocation location, float[] value) => await Invoke("uniform2fv", location, value);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="v0">An integer Number for integer values</param>
    /// <param name="v1">An integer Number for integer values</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform2i(WebGLUniformLocation location, int v0, int v1) => await Invoke("uniform2i", location, v0, v1);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="value">An Int32Array for integer vector methods</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform2iv(WebGLUniformLocation location, int[] value) => await Invoke("uniform2iv", location, value);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="v0">A floating point Number for floating point values</param>
    /// <param name="v1">A floating point Number for floating point values</param>
    /// <param name="v2">A floating point Number for floating point values</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform3f(WebGLUniformLocation location, float v0, float v1, float v2) => await Invoke("uniform3f", location, v0, v1, v2);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="value">A sequence of floating point numbers for floating point vector methods</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform3fv(WebGLUniformLocation location, float[] value) => await Invoke("uniform3fv", location, value);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="v0">An integer Number for integer values</param>
    /// <param name="v1">An integer Number for integer values</param>
    /// <param name="v2">An integer Number for integer values</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform3i(WebGLUniformLocation location, int v0, int v1, int v2) => await Invoke("uniform3i", location, v0, v1, v2);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="value">An Int32Array for integer vector methods</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform3iv(WebGLUniformLocation location, int[] value) => await Invoke("uniform3iv", location, value);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="v0">A floating point Number for floating point values</param>
    /// <param name="v1">A floating point Number for floating point values</param>
    /// <param name="v2">A floating point Number for floating point values</param>
    /// <param name="v3">A floating point Number for floating point values</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform4f(WebGLUniformLocation location, float v0, float v1, float v2, float v3) => await Invoke("uniform4f", location, v0, v1, v2, v3);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="value">A sequence of floating point numbers for floating point vector methods</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform4fv(WebGLUniformLocation location, float[] value) => await Invoke("uniform4fv", location, value);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="v0">An integer Number for integer values</param>
    /// <param name="v1">An integer Number for integer values</param>
    /// <param name="v2">An integer Number for integer values</param>
    /// <param name="v3">An integer Number for integer values</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform4i(WebGLUniformLocation location, int v0, int v1, int v2, int v3) => await Invoke("uniform4i", location, v0, v1, v2, v3);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// </summary>
    /// <param name="location">A WebGLUniformLocation object containing the location of the uniform attribute to modify.</param>
    /// <param name="value">An Int32Array for integer vector methods</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniform</remarks>
    public async Task Uniform4iv(WebGLUniformLocation location, int[] value) => await Invoke("uniform4iv", location, value);

    /// <summary>
    /// specify matrix values for uniform variables.
    /// The three versions of this method (uniformMatrix2fv(), uniformMatrix3fv(), and uniformMatrix4fv()) take as the input value 
    /// 2-component, 3-component, and 4-component square matrices, respectively. They are expected to have 4, 9 or 16 floats.
    /// </summary>
    /// <param name="location">
    /// A WebGLUniformLocation object containing the location of the uniform attribute to modify. 
    /// The location is obtained using getUniformLocation().
    /// </param>
    /// <param name="transpose">A GLboolean specifying whether to transpose the matrix. Must be false.</param>
    /// <param name="value">A Float32Array or sequence of GLfloat values. The values are assumed to be supplied in column major order.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniformMatrix</remarks>
    public async Task UniformMatrix2fv(WebGLUniformLocation location, bool transpose, float[] value) => await Invoke("uniformMatrix2fv", location, transpose, value);

    /// <summary>
    /// specify matrix values for uniform variables.
    /// The three versions of this method (uniformMatrix2fv(), uniformMatrix3fv(), and uniformMatrix4fv()) take as the input value 
    /// 2-component, 3-component, and 4-component square matrices, respectively. They are expected to have 4, 9 or 16 floats.
    /// </summary>
    /// <param name="location">
    /// A WebGLUniformLocation object containing the location of the uniform attribute to modify. 
    /// The location is obtained using getUniformLocation().
    /// </param>
    /// <param name="transpose">A GLboolean specifying whether to transpose the matrix. Must be false.</param>
    /// <param name="value">A Float32Array or sequence of GLfloat values. The values are assumed to be supplied in column major order.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniformMatrix</remarks>
    public async Task UniformMatrix3fv(WebGLUniformLocation location, bool transpose, float[] value) => await Invoke("uniformMatrix3fv", location, transpose, value);

    /// <summary>
    /// specify matrix values for uniform variables.
    /// The three versions of this method (uniformMatrix2fv(), uniformMatrix3fv(), and uniformMatrix4fv()) take as the input value 
    /// 2-component, 3-component, and 4-component square matrices, respectively. They are expected to have 4, 9 or 16 floats.
    /// </summary>
    /// <param name="location">
    /// A WebGLUniformLocation object containing the location of the uniform attribute to modify. 
    /// The location is obtained using getUniformLocation().
    /// </param>
    /// <param name="transpose">A GLboolean specifying whether to transpose the matrix. Must be false.</param>
    /// <param name="value">A Float32Array or sequence of GLfloat values. The values are assumed to be supplied in column major order.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/uniformMatrix</remarks>
    public async Task UniformMatrix4fv(WebGLUniformLocation location, bool transpose, float[] value) => await Invoke("uniformMatrix4fv", location, transpose, value);

    /// <summary>
    /// specify constant values for generic vertex attributes.
    /// </summary>
    /// <param name="index">A GLuint specifying the position of the vertex attribute to be modified.</param>
    /// <param name="v0">A floating point Number for the vertex attribute value.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib</remarks>
    public async Task VertexAttrib1f(uint index, float v0) => await Invoke("vertexAttrib1f", index, v0);
    
    /// <summary>
    /// specify constant values for generic vertex attributes.
    /// </summary>
    /// <param name="index">A GLuint specifying the position of the vertex attribute to be modified.</param>
    /// <param name="v0">A floating point Number for the vertex attribute value.</param>
    /// <param name="v1">A floating point Number for the vertex attribute value.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib</remarks>
    public async Task VertexAttrib2f(uint index, float v0, float v1) => await Invoke("vertexAttrib2f", index, v0, v1);
   
    /// <summary>
    /// specify constant values for generic vertex attributes.
    /// </summary>
    /// <param name="index">A GLuint specifying the position of the vertex attribute to be modified.</param>
    /// <param name="v0">A floating point Number for the vertex attribute value.</param>
    /// <param name="v1">A floating point Number for the vertex attribute value.</param>
    /// <param name="v2">A floating point Number for the vertex attribute value.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib</remarks>
    public async Task VertexAttrib3f(uint index, float v0, float v1, float v2) => await Invoke("vertexAttrib3f", index, v0, v1, v2);
   
    /// <summary>
    /// specify constant values for generic vertex attributes.
    /// </summary>
    /// <param name="index">A GLuint specifying the position of the vertex attribute to be modified.</param>
    /// <param name="v0">A floating point Number for the vertex attribute value.</param>
    /// <param name="v1">A floating point Number for the vertex attribute value.</param>
    /// <param name="v2">A floating point Number for the vertex attribute value.</param>
    /// <param name="v3">A floating point Number for the vertex attribute value.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib</remarks>
    public async Task VertexAttrib4f(uint index, float v0, float v1, float v2, float v3) => await Invoke("vertexAttrib4f", index, v0, v1, v2, v3);

    /// <summary>
    /// specify constant values for generic vertex attributes.
    /// </summary>
    /// <param name="index">A GLuint specifying the position of the vertex attribute to be modified.</param>
    /// <param name="value">A Float32Array for floating point vector vertex attribute values.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib</remarks>
    public async Task VertexAttrib1fv(uint index, float[] value) => await Invoke("vertexAttrib1fv", index, value);
    
    /// <summary>
    /// specify constant values for generic vertex attributes.
    /// </summary>
    /// <param name="index">A GLuint specifying the position of the vertex attribute to be modified.</param>
    /// <param name="value">A Float32Array for floating point vector vertex attribute values.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib</remarks>
    public async Task VertexAttrib2fv(uint index, float[] value) => await Invoke("vertexAttrib2fv", index, value);
    
    /// <summary>
    /// specify constant values for generic vertex attributes.
    /// </summary>
    /// <param name="index">A GLuint specifying the position of the vertex attribute to be modified.</param>
    /// <param name="value">A Float32Array for floating point vector vertex attribute values.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib</remarks>
    public async Task VertexAttrib3fv(uint index, float[] value) => await Invoke("vertexAttrib3fv", index, value);
        
    /// <summary>
    /// specify constant values for generic vertex attributes.
    /// </summary>
    /// <param name="index">A GLuint specifying the position of the vertex attribute to be modified.</param>
    /// <param name="value">A Float32Array for floating point vector vertex attribute values.</param>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttrib</remarks>
    public async Task VertexAttrib4fv(uint index, float[] value) => await Invoke("vertexAttrib4fv", index, value);
    
    /// <summary>
    /// binds the buffer currently bound to gl.ARRAY_BUFFER to a generic vertex attribute of the current vertex buffer object and specifies its layout.
    /// </summary>
    /// <param name="index">A GLuint specifying the index of the vertex attribute that is to be modified.</param>
    /// <param name="size">A GLint specifying the number of components per vertex attribute. Must be 1, 2, 3, or 4.</param>
    /// <param name="type">A GLenum specifying the data type of each component in the array</param>
    /// <param name="normalized">A GLboolean specifying whether integer data values should be normalized into a certain range when being cast to a float.</param>
    /// <param name="stride">
    /// A GLsizei specifying the offset in bytes between the beginning of consecutive vertex attributes. 
    /// Cannot be negative or larger than 255. If stride is 0, the attribute is assumed to be tightly packed, that is, 
    /// the attributes are not interleaved but each attribute is in a separate block, 
    /// and the next vertex' attribute follows immediately after the current vertex.
    /// </param>
    /// <param name="offset">A GLintptr specifying an offset in bytes of the first component in the vertex attribute array. Must be a multiple of the byte length of type.</param>
    /// <exception cref="GL.INVALID_VALUE">error is thrown if stride or offset are negative.</exception>
    /// <exception cref="GL.INVALID_OPERATION">error is thrown if stride and offset are not multiples of the size of the data type.</exception>
    /// <exception cref="GL.INVALID_OPERATION">error is thrown if no WebGLBuffer is bound to the ARRAY_BUFFER target.</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/vertexAttribPointer</remarks>
    public async Task VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, int offset) => 
        await Invoke("vertexAttribPointer", index, size, type, normalized, stride, offset);

#endregion

#region Drawing buffers

    /// <summary>
    /// clears buffers to preset values.
    /// 
    /// The preset values can be set by clearColor(), clearDepth() or clearStencil().
    /// The scissor box, dithering, and buffer writemasks can affect the clear() method. 
    /// </summary>
    /// <param name="mask">
    /// A GLbitfield bitwise OR mask that indicates the buffers to be cleared. Possible values are:
    /// - GL.COLOR_BUFFER_BIT
    /// - GL.DEPTH_BUFFER_BIT
    /// - GL.STENCIL_BUFFER_BIT
    /// </param>
    /// <exception cref="GL.INVALID_ENUM">If mask is not one of the listed possible values</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/clear</remarks>
    public async Task Clear(ulong mask) => await Invoke("clear", mask);

    /// <summary>
    /// renders primitives from array data.
    /// </summary>
    /// <param name="mode">
    /// A GLenum specifying the type primitive to render. Possible values are:
    /// - GL.POINTS: Draws a single dot.
    /// - GL.LINE_STRIP: Draws a straight line to the next vertex.
    /// - GL.LINE_LOOP: Draws a straight line to the next vertex, and connects the last vertex back to the first.
    /// - GL.LINES: Draws a line between a pair of vertices.
    /// - GL.TRIANGLE_STRIP
    /// - GL.TRIANGLE_FAN
    /// - GL.TRIANGLES: Draws a triangle for a group of three vertices.
    /// </param>
    /// <param name="first">A GLint specifying the starting index in the array of vector points.</param>
    /// <param name="count">A GLsizei specifying the number of indices to be rendered.</param>
    /// <exception cref="GL.INVALID_ENUM">If mode is not one of the accepted values</exception>
    /// <exception cref="GL.INVALID_VALUE">If first or count are negative</exception>
    /// <exception cref="GL.INVALID_OPERATION">if gl.CURRENT_PROGRAM is null</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/drawArrays</remarks>
    public async Task DrawArrays(uint mode, int first, int count) => await Invoke("drawArrays", mode, first, count);

    /// <summary>
    /// renders primitives from array data.
    /// </summary>
    /// <param name="mode">
    /// A GLenum specifying the type primitive to render. Possible values are:
    /// - GL.POINTS: Draws a single dot.
    /// - GL.LINE_STRIP: Draws a straight line to the next vertex.
    /// - GL.LINE_LOOP: Draws a straight line to the next vertex, and connects the last vertex back to the first.
    /// - GL.LINES: Draws a line between a pair of vertices.
    /// - GL.TRIANGLE_STRIP
    /// - GL.TRIANGLE_FAN
    /// - GL.TRIANGLES: Draws a triangle for a group of three vertices.
    /// </param>
    /// <param name="count">A GLsizei specifying the number of elements of the bound element array buffer to be rendered. [Consult MDN for more information]</param>
    /// <param name="type">
    /// A GLenum specifying the type of the values in the element array buffer. Possible values are:
    /// - GL.UNSIGNED_BYTE
    /// - GL.UNSIGNED_SHORT
    /// </param>
    /// <param name="offset">
    /// A GLintptr specifying a byte offset in the element array buffer. 
    /// Must be a valid multiple of the size of the given type.
    /// </param>
    /// <exception cref="GL.INVALID_ENUM">If mode is not one of the accepted values</exception>
    /// <exception cref="GL.INVALID_OPERATION">If offset is not a valid multiple of the size of the given type</exception>
    /// <exception cref="GL.INVALID_VALUE ">If count is negative</exception>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/drawElements</remarks>
    public async Task DrawElements(uint mode, int count, uint type, int offset) => await Invoke("drawElements", mode, count, type, offset);

    /// <summary>
    /// blocks execution until all previously called commands are finished.
    /// </summary>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/finish</remarks>
    public async Task Finish() => await Invoke("finish");

    /// <summary>
    /// empties different buffer commands, causing all commands to be executed as quickly as possible.
    /// </summary>
    /// <remarks>https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/flush</remarks>
    public async Task Flush() => await Invoke("flush");

#endregion

}