namespace BlazeFrame.Canvas.WebGL;

public class GL
{

// Constants passed to WebGLRenderingContext.clear() to clear buffer masks.
#region Clearing buffers
    /// <summary>
    /// Passed to clear to clear the current depth buffer.
    /// </summary>
    public const int DEPTH_BUFFER_BIT = 0x00000100;

    /// <summary>
    /// Passed to clear to clear the current stencil buffer.
    /// </summary>
    public const int STENCIL_BUFFER_BIT = 0x00000400;

    /// <summary>
    /// Passed to clear to clear the current color buffer.
    /// </summary>
    public const int COLOR_BUFFER_BIT = 0x00004000;

#endregion

// Constants passed to WebGLRenderingContext.drawElements() or WebGLRenderingContext.drawArrays() to specify what kind of primitive to render.
#region Rendering primitives

    /// <summary>
    /// Passed to drawElements or drawArrays to draw single points.
    /// </summary>
    public const int POINTS = 0x0000;

    /// <summary>
    /// Passed to drawElements or drawArrays to draw lines. Each vertex connects to the one after it.
    /// </summary>
    public const int LINES = 0x0001;

    /// <summary>
    /// Passed to drawElements or drawArrays to draw lines. Each set of two vertices is treated as a separate line segment.
    /// </summary>
    public const int LINE_LOOP = 0x0002;

    /// <summary>
    /// Passed to drawElements or drawArrays to draw a connected group of line segments from the first vertex to the last.
    /// </summary>
    public const int LINE_STRIP = 0x0003;

    /// <summary>
    /// Passed to drawElements or drawArrays to draw triangles. Each set of three vertices creates a separate triangle.
    /// </summary>
    public const int TRIANGLES = 0x0004;

    /// <summary>
    /// Passed to drawElements or drawArrays to draw a connected group of triangles.
    /// </summary>
    public const int TRIANGLE_STRIP = 0x0005;

    /// <summary>
    /// Passed to drawElements or drawArrays to draw a connected group of triangles. Each vertex connects to the previous and the first vertex in the fan.
    /// </summary>
    public const int TRIANGLE_FAN = 0x0006;

#endregion

// Constants passed to WebGLRenderingContext.blendFunc() or WebGLRenderingContext.blendFuncSeparate() to specify the blending mode (for both, RGB and alpha, or separately).
#region Blending modes

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to turn off a component.
    /// </summary>
    public const int ZERO = 0;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to turn on a component.
    /// </summary>
    public const int ONE = 1;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to multiply a component by the source elements color.
    /// </summary>
    public const int SRC_COLOR = 0x0300;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to multiply a component by one minus the source elements color.
    /// </summary>
    public const int ONE_MINUS_SRC_COLOR = 0x0301;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to multiply a component by the source's alpha.
    /// </summary>
    public const int SRC_ALPHA = 0x0302;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to multiply a component by one minus the source's alpha.
    /// </summary>
    public const int ONE_MINUS_SRC_ALPHA = 0x0303;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to multiply a component by the destination's alpha.
    /// </summary>
    public const int DST_ALPHA = 0x0304;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to multiply a component by one minus the destination's alpha.
    /// </summary>
    public const int ONE_MINUS_DST_ALPHA = 0x0305;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to multiply a component by the destination's color.
    /// </summary>
    public const int DST_COLOR = 0x0306;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to multiply a component by one minus the destination's color.
    /// </summary>
    public const int ONE_MINUS_DST_COLOR = 0x0307;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to multiply a component by the minimum of source's alpha or one minus the destination's alpha.
    /// </summary>
    public const int SRC_ALPHA_SATURATE = 0x0308;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to specify a constant color blend function.
    /// </summary>
    public const int CONSTANT_COLOR = 0x8001;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to specify one minus a constant color blend function.
    /// </summary>
    public const int ONE_MINUS_CONSTANT_COLOR = 0x8002;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to specify a constant alpha blend function.
    /// </summary>
    public const int CONSTANT_ALPHA = 0x8003;

    /// <summary>
    /// Passed to blendFunc or blendFuncSeparate to specify one minus a constant alpha blend function.
    /// </summary>
    public const int ONE_MINUS_CONSTANT_ALPHA = 0x8004;
#endregion

// Constants passed to WebGLRenderingContext.blendEquation() or WebGLRenderingContext.blendEquationSeparate() to control how the blending is calculated (for both, RGB and alpha, or separately).
#region Blending equations

    /// <summary>
    /// Passed to blendEquation or blendEquationSeparate to set an addition blend function.
    /// </summary>
    public const int FUNC_ADD = 0x8006;

    /// <summary>
    /// Passed to blendEquation or blendEquationSeparate to specify a subtraction blend function (source - destination).
    /// </summary>
    public const int FUNC_SUBTRACT = 0x800A;

    /// <summary>
    /// Passed to blendEquation or blendEquationSeparate to specify a reverse subtraction blend function (destination - source).
    /// </summary>
    public const int FUNC_REVERSE_SUBTRACT = 0x800B;

#endregion

// Constants passed to WebGLRenderingContext.getParameter() to specify what information to return.
#region Getting GL parameter information

    /// <summary>
    /// Passed to getParameter to get the current RGB blend function.
    /// </summary>
    public const int BLEND_EQUATION = 0x8009;

    /// <summary>
    /// Passed to getParameter to get the current RGB blend function. Same as BLEND_EQUATION
    /// </summary>
    public const int BLEND_EQUATION_RGB = 0x8009;

    /// <summary>
    /// Passed to getParameter to get the current alpha blend function.
    /// </summary>
    public const int BLEND_EQUATION_ALPHA = 0x883D;

    /// <summary>
    /// Passed to getParameter to get the current destination RGB blend function.
    /// </summary>
    public const int BLEND_DST_RGB = 0x80C8;

    /// <summary>
    /// Passed to getParameter to get the current destination RGB blend function.
    /// </summary>
    public const int BLEND_SRC_RGB = 0x80C9;

    /// <summary>
    /// Passed to getParameter to get the current destination alpha blend function.
    /// </summary>
    public const int BLEND_DST_ALPHA = 0x80CA;

    /// <summary>
    /// Passed to getParameter to get the current source alpha blend function.
    /// </summary>
    public const int BLEND_SRC_ALPHA = 0x80CB;

    /// <summary>
    /// Passed to getParameter to return a the current blend color.
    /// </summary>
    public const int BLEND_COLOR = 0x8005;

    /// <summary>
    /// Passed to getParameter to get the array buffer binding.
    /// </summary>
    public const int ARRAY_BUFFER_BINDING = 0x8894;

    /// <summary>
    /// Passed to getParameter to get the current element array buffer.
    /// </summary>
    public const int ELEMENT_ARRAY_BUFFER_BINDING = 0x8895;

    /// <summary>
    /// Passed to getParameter to get the current lineWidth (set by the lineWidth method).
    /// </summary>
    public const int LINE_WIDTH = 0x0B21;

    /// <summary>
    /// Passed to getParameter to get the current size of a point drawn with gl.POINTS
    /// </summary>
    public const int ALIASED_POINT_SIZE_RANGE = 0x846D;

    /// <summary>
    /// Passed to getParameter to get the range of available widths for a line. 
    /// The getParameter method then returns an array with two elements: the first element is the minimum width value and the second element is the maximum width value.
    /// </summary>
    public const int ALIASED_LINE_WIDTH_RANGE = 0x846E;

    /// <summary>
    /// Passed to getParameter to get the current value of cullFace. Should return FRONT, BACK, or FRONT_AND_BACK
    /// </summary>
    public const int CULL_FACE_MODE = 0x0B45;

    /// <summary>
    /// Passed to getParameter to determine the current value of frontFace. Should return CW or CCW.
    /// </summary>
    public const int FRONT_FACE = 0x0B46;

    /// <summary>
    /// Passed to getParameter to return a length-2 array of floats giving the current depth range.
    /// </summary>
    public const int DEPTH_RANGE = 0x0B70;

    /// <summary>
    /// Passed to getParameter to determine if the depth write mask is enabled.
    /// </summary>
    public const int DEPTH_WRITEMASK = 0x0B72;

    /// <summary>
    /// Passed to getParameter to determine the current depth clear value.
    /// </summary>
    public const int DEPTH_CLEAR_VALUE = 0x0B73;

    /// <summary>
    /// Passed to getParameter to get the current depth function. 
    /// Returns NEVER, ALWAYS, LESS, EQUAL, LEQUAL, GREATER, GEQUAL, or NOTEQUAL.
    /// </summary>
    public const int DEPTH_FUNC = 0x0B74;

    /// <summary>
    /// Passed to getParameter to get the value the stencil will be cleared to.
    /// </summary>
    public const int STENCIL_CLEAR_VALUE = 0x0B91;

    /// <summary>
    /// Passed to getParameter to get the current stencil function. 
    /// Returns NEVER, ALWAYS, LESS, EQUAL, LEQUAL, GREATER, GEQUAL, or NOTEQUAL.
    /// </summary>
    public const int STENCIL_FUNC = 0x0B92;

    /// <summary>
    /// Passed to getParameter to get the current stencil fail function. 
    /// Should return KEEP, REPLACE, INCR, DECR, INVERT, INCR_WRAP, or DECR_WRAP.
    /// </summary>
    public const int STENCIL_FAIL = 0x0B94;

    /// <summary>
    /// Passed to getParameter to get the current stencil fail function should the depth buffer test fail. 
    /// Should return KEEP, REPLACE, INCR, DECR, INVERT, INCR_WRAP, or DECR_WRAP.
    /// </summary>
    public const int STENCIL_PASS_DEPTH_FAIL = 0x0B95;

    /// <summary>
    /// Passed to getParameter to get the current stencil fail function should the depth buffer test pass. 
    /// Should return KEEP, REPLACE, INCR, DECR, INVERT, INCR_WRAP, or DECR_WRAP.
    /// </summary>
    public const int STENCIL_PASS_DEPTH_PASS = 0x0B96;

    /// <summary>
    /// Passed to getParameter to get the reference value used for stencil tests.
    /// </summary>
    public const int STENCIL_REF = 0x0B97;

    public const int STENCIL_VALUE_MASK = 0x0B93;

    public const int STENCIL_WRITEMASK = 0x0B98;

    public const int STENCIL_BACK_FUNC = 0x8800;

    public const int STENCIL_BACK_FAIL = 0x8801;

    public const int STENCIL_BACK_PASS_DEPTH_FAIL = 0x8802;

    public const int STENCIL_BACK_PASS_DEPTH_PASS = 0x8803;

    public const int STENCIL_BACK_REF = 0x8CA3;

    public const int STENCIL_BACK_VALUE_MASK = 0x8CA4;

    public const int STENCIL_BACK_WRITEMASK = 0x8CA5;

    /// <summary>
    /// Returns an Int32Array with four elements for the current viewport dimensions.
    /// </summary>
    public const int VIEWPORT = 0x0BA2;

    /// <summary>
    /// Returns an Int32Array with four elements for the current scissor box dimensions.
    /// </summary>
    public const int SCISSOR_BOX = 0x0C10;

    public const int COLOR_CLEAR_VALUE = 0x0C22;

    public const int COLOR_WRITEMASK = 0x0C23;

    public const int UNPACK_ALIGNMENT = 0x0CF5;

    public const int PACK_ALIGNMENT = 0x0D05;

    public const int MAX_TEXTURE_SIZE = 0x0D33;

    public const int MAX_VIEWPORT_DIMS = 0x0D3A;

    public const int SUBPIXEL_BITS = 0x0D50;

    public const int RED_BITS = 0x0D52;

    public const int GREEN_BITS = 0x0D53;

    public const int BLUE_BITS = 0x0D54;

    public const int ALPHA_BITS = 0x0D55;

    public const int DEPTH_BITS = 0x0D56;

    public const int STENCIL_BITS = 0x0D57;

    public const int POLYGON_OFFSET_UNITS = 0x2A00;

    public const int POLYGON_OFFSET_FACTOR = 0x8038;

    public const int TEXTURE_BINDING_2D = 0x8069;

    public const int SAMPLE_BUFFERS = 0x80A8;

    public const int SAMPLES = 0x80A9;

    public const int SAMPLE_COVERAGE_VALUE = 0x80AA;

    public const int SAMPLE_COVERAGE_INVERT = 0x80AB;

    public const int COMPRESSED_TEXTURE_FORMATS = 0x86A3;

    public const int VENDOR = 0x1F00;

    public const int RENDERER = 0x1F01;

    public const int VERSION = 0x1F02;

    public const int IMPLEMENTATION_COLOR_READ_TYPE = 0x8B9A;

    public const int IMPLEMENTATION_COLOR_READ_FORMAT = 0x8B9B;

    public const int BROWSER_DEFAULT_WEBGL = 0x9244;

#endregion

// Constants passed to WebGLRenderingContext.bufferData(), WebGLRenderingContext.bufferSubData(), WebGLRenderingContext.bindBuffer(), or WebGLRenderingContext.getBufferParameter().
#region Buffers

    /// <summary>
    /// Passed to bufferData as a hint about whether the contents of the buffer are likely to be used often and not change often.
    /// </summary>
    public const int STATIC_DRAW = 0x88E4;

    /// <summary>
    /// Passed to bufferData as a hint about whether the contents of the buffer are likely to not be used often.
    /// </summary>
    public const int STREAM_DRAW = 0x88E0;

    /// <summary>
    /// Passed to bufferData as a hint about whether the contents of the buffer are likely to be used often and change often.
    /// </summary>
    public const int DYNAMIC_DRAW = 0x88E8;

    /// <summary>
    /// Passed to bindBuffer or bufferData to specify the type of buffer being used.
    /// </summary>
    public const int ARRAY_BUFFER = 0x8892;

    /// <summary>
    /// Passed to bindBuffer or bufferData to specify the type of buffer being used.
    /// </summary>
    public const int ELEMENT_ARRAY_BUFFER = 0x8893;

    /// <summary>
    /// Passed to getBufferParameter to get a buffer's size.
    /// </summary>
    public const int BUFFER_SIZE = 0x8764;

    /// <summary>
    /// Passed to getBufferParameter to get the hint for the buffer passed in when it was created.
    /// </summary>
    public const int BUFFER_USAGE = 0x8765;

#endregion

// Constants passed to WebGLRenderingContext.getVertexAttrib().
#region Vertex attributes

    /// <summary>
    /// Passed to getVertexAttrib to read back the current vertex attribute.
    /// </summary>
    public const int CURRENT_VERTEX_ATTRIB = 0x8626;

    public const int VERTEX_ATTRIB_ARRAY_ENABLED = 0x8622;

    public const int VERTEX_ATTRIB_ARRAY_SIZE = 0x8623;

    public const int VERTEX_ATTRIB_ARRAY_STRIDE = 0x8624;

    public const int VERTEX_ATTRIB_ARRAY_TYPE = 0x8625;

    public const int VERTEX_ATTRIB_ARRAY_NORMALIZED	= 0x886A;

    public const int VERTEX_ATTRIB_ARRAY_POINTER = 0x8645; 

    public const int VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = 0x889F;

#endregion

// Constants passed to WebGLRenderingContext.cullFace().
#region Culling

    /// <summary>
    /// Passed to enable/disable to turn on/off culling. Can also be used with getParameter to find the current culling method.
    /// </summary>
    public const int CULL_FACE = 0x0B44;

    /// <summary>
    /// Passed to cullFace to specify that only front faces should be culled.
    /// </summary>
    public const int FRONT = 0x0404;

    /// <summary>
    /// Passed to cullFace to specify that only back faces should be culled.
    /// </summary>
    public const int BACK = 0x0405;

    /// <summary>
    /// Passed to cullFace to specify that front and back faces should be culled.
    /// </summary>
    public const int FRONT_AND_BACK	= 0x0408;

#endregion

// Constants passed to WebGLRenderingContext.enable() or WebGLRenderingContext.disable().
#region Enabling and disabling

    /// <summary>
    /// Passed to enable/disable to turn on/off blending. Can also be used with getParameter to find the current blending method.
    /// </summary>
    public const int BLEND = 0x0BE2;

    /// <summary>
    /// Passed to enable/disable to turn on/off the depth test. Can also be used with getParameter to query the depth test.
    /// </summary>
    public const int DEPTH_TEST = 0x0B71;

    /// <summary>
    /// Passed to enable/disable to turn on/off dithering. Can also be used with getParameter to find the current dithering method.
    /// </summary>
    public const int DITHER = 0x0BD0;

    /// <summary>
    /// Passed to enable/disable to turn on/off the polygon offset. Useful for rendering hidden-line images, decals, and or solids with highlighted edges. 
    /// Can also be used with getParameter to query the scissor test.
    /// </summary>
    public const int POLYGON_OFFSET_FILL = 0x8037;

    /// <summary>
    /// Passed to enable/disable to turn on/off the alpha to coverage. Used in multi-sampling alpha channels.
    /// </summary>
    public const int SAMPLE_ALPHA_TO_COVERAGE = 0x809E;

    /// <summary>
    /// Passed to enable/disable to turn on/off the sample coverage. Used in multi-sampling.
    /// </summary>
    public const int SAMPLE_COVERAGE = 0x80A0;

    /// <summary>
    /// Passed to enable/disable to turn on/off the scissor test. Can also be used with getParameter to query the scissor test.
    /// </summary>
    public const int SCISSOR_TEST = 0x0C11; 

    /// <summary>
    /// Passed to enable/disable to turn on/off the stencil test. Can also be used with getParameter to query the stencil test.
    /// </summary>
    public const int STENCIL_TEST = 0x0B90;

#endregion

// Constants returned from WebGLRenderingContext.getError().
#region Errors

    /// <summary>
    /// Returned from getError.
    /// </summary>
    public const int NO_ERROR = 0;

    /// <summary>
    /// Returned from getError.
    /// </summary>
    public const int INVALID_ENUM = 0x0500;

    /// <summary>
    /// Returned from getError.
    /// </summary>
    public const int INVALID_VALUE = 0x0501;

    /// <summary>
    /// Returned from getError.
    /// </summary>
    public const int INVALID_OPERATION = 0x0502;

    /// <summary>
    /// Returned from getError.
    /// </summary>
    public const int OUT_OF_MEMORY = 0x0505;

    /// <summary>
    /// Returned from getError.
    /// </summary>
    public const int CONTEXT_LOST_WEBGL = 0x9242;

#endregion

// Constants passed to WebGLRenderingContext.frontFace().
#region Front face directions

    /// <summary>
    /// Passed to frontFace to specify the front face of a polygon is drawn in the clockwise direction
    /// </summary>
    public const int CW	= 0x0900;

    /// <summary>
    /// Passed to frontFace to specify the front face of a polygon is drawn in the counter clockwise direction
    /// </summary>
    public const int CCW = 0x0901;

#endregion

// Constants passed to WebGLRenderingContext.hint()
#region Hints

    /// <summary>
    /// There is no preference for this behavior.
    /// </summary>
    public const int DONT_CARE = 0x1100;

    /// <summary>
    /// The most efficient behavior should be used.
    /// </summary>
    public const int FASTEST = 0x1101;

    /// <summary>
    /// The most correct or the highest quality option should be used.
    /// </summary>
    public const int NICEST = 0x1102;

    /// <summary>
    /// Hint for the quality of filtering when generating mipmap images with WebGLRenderingContext.generateMipmap().
    /// </summary>
    public const int GENERATE_MIPMAP_HINT = 0x8192;

#endregion

#region Data types

    public const int BYTE = 0x1400;

    public const int UNSIGNED_BYTE = 0x1401;

    public const int SHORT = 0x1402;

    public const int UNSIGNED_SHORT = 0x1403;

    public const int INT = 0x1404;

    public const int UNSIGNED_INT = 0x1405;

    public const int FLOAT = 0x1406;
    
#endregion

#region Pixel formats

    public const int DEPTH_COMPONENT = 0x1902;
    
    public const int ALPHA = 0x1906;
    
    public const int RGB = 0x1907;
    
    public const int RGBA = 0x1908;
    
    public const int LUMINANCE = 0x1909;
    
    public const int LUMINANCE_ALPHA = 0x190A;

#endregion

#region Pixel types

    // public const int UNSIGNED_BYTE = 0x1401;

    public const int UNSIGNED_SHORT_4_4_4_4 = 0x8033;

    public const int UNSIGNED_SHORT_5_5_5_1 = 0x8034;

    public const int UNSIGNED_SHORT_5_6_5 = 0x8363;

#endregion

// Constants passed to WebGLRenderingContext.createShader() or WebGLRenderingContext.getShaderParameter()
#region Shaders

    /// <summary>
    /// Passed to createShader to define a fragment shader.
    /// </summary>
    public const int FRAGMENT_SHADER = 0x8B30;
    
    /// <summary>
    /// Passed to createShader to define a vertex shader
    /// </summary>
    public const int VERTEX_SHADER = 0x8B31;

    /// <summary>
    /// Passed to getShaderParameter to get the status of the compilation. Returns false if the shader was not compiled. 
    /// You can then query getShaderInfoLog to find the exact error
    /// </summary>
    public const int COMPILE_STATUS = 0x8B81;

    /// <summary>
    /// Passed to getShaderParameter to determine if a shader was deleted via deleteShader. Returns true if it was, false otherwise.
    /// </summary>
    public const int DELETE_STATUS = 0x8B80;

    /// <summary>
    /// Passed to getProgramParameter after calling linkProgram to determine if a program was linked correctly. 
    /// Returns false if there were errors. Use getProgramInfoLog to find the exact error.
    /// </summary>
    public const int LINK_STATUS = 0x8B82;

    /// <summary>
    /// Passed to getProgramParameter after calling validateProgram to determine if it is valid. 
    /// Returns false if errors were found.
    /// </summary>
    public const int VALIDATE_STATUS = 0x8B83;

    /// <summary>
    /// Passed to getProgramParameter after calling attachShader to determine if the shader was attached correctly. Returns false if errors occurred.
    /// </summary>
    public const int ATTACHED_SHADERS = 0x8B85;

    /// <summary>
    /// Passed to getProgramParameter to get the number of attributes active in a program.
    /// </summary>
    public const int ACTIVE_ATTRIBUTES = 0x8B89;

    /// <summary>
    /// Passed to getProgramParameter to get the number of uniforms active in a program.
    /// </summary>
    public const int ACTIVE_UNIFORMS = 0x8B86;

    /// <summary>
    /// The maximum number of entries possible in the vertex attribute list.
    /// </summary>
    public const int MAX_VERTEX_ATTRIBS = 0x8869;

    public const int MAX_VERTEX_UNIFORM_VECTORS = 0x8DFB;
    
    public const int MAX_VARYING_VECTORS = 0x8DFC;
    
    public const int MAX_COMBINED_TEXTURE_IMAGE_UNITS = 0x8B4D;
    
    public const int MAX_VERTEX_TEXTURE_IMAGE_UNITS = 0x8B4C;

    /// <summary>
    /// Implementation dependent number of maximum texture units. At least 8.
    /// </summary>
    public const int MAX_TEXTURE_IMAGE_UNITS = 0x8872;

    public const int MAX_FRAGMENT_UNIFORM_VECTORS = 0x8DFD;
    
    public const int SHADER_TYPE = 0x8B4F;
    
    public const int SHADING_LANGUAGE_VERSION = 0x8B8C;
    
    public const int CURRENT_PROGRAM = 0x8B8D;

#endregion

// Constants passed to WebGLRenderingContext.depthFunc() or WebGLRenderingContext.stencilFunc().
#region Depth or stencil tests

    /// <summary>
    /// Passed to depthFunction or stencilFunction to specify depth or stencil tests will never pass, i.e., nothing will be drawn.
    /// </summary>
    public const int NEVER = 0x0200;

    /// <summary>
    /// Passed to depthFunction or stencilFunction to specify depth or stencil tests will pass if the new depth value is less than the stored value.
    /// </summary>
    public const int LESS = 0x0201;

    /// <summary>
    /// 	Passed to depthFunction or stencilFunction to specify depth or stencil tests will pass if the new depth value is equals to the stored value.
    /// </summary>
    public const int EQUAL = 0x0202;

    /// <summary>
    /// Passed to depthFunction or stencilFunction to specify depth or stencil tests will pass if the new depth value is less than or equal to the stored value.
    /// </summary>
    public const int LEQUAL = 0x0203;

    /// <summary>
    /// Passed to depthFunction or stencilFunction to specify depth or stencil tests will pass if the new depth value is greater than the stored value.
    /// </summary>
    public const int GREATER = 0x0204;

    /// <summary>
    /// Passed to depthFunction or stencilFunction to specify depth or stencil tests will pass if the new depth value is not equal to the stored value.
    /// </summary>
    public const int NOTEQUAL = 0x0205;

    /// <summary>
    /// Passed to depthFunction or stencilFunction to specify depth or stencil tests will pass if the new depth value is greater than or equal to the stored value.
    /// </summary>
    public const int GEQUAL = 0x0206;

    /// <summary>
    /// Passed to depthFunction or stencilFunction to specify depth or stencil tests will always pass, i.e., pixels will be drawn in the order they are drawn.
    /// </summary>
    public const int ALWAYS = 0x0207;

#endregion

// Constants passed to WebGLRenderingContext.stencilOp().
#region Stencil actions

    public const int KEEP = 0x1E00;
 
    public const int REPLACE = 0x1E01;
 
    public const int INCR = 0x1E02;
 
    public const int DECR = 0x1E03;
 
    public const int INVERT = 0x150A;
 
    public const int INCR_WRAP = 0x8507;
 
    public const int DECR_WRAP = 0x8508;

#endregion

// Constants passed to WebGLRenderingContext.texParameteri(), WebGLRenderingContext.texParameterf(), WebGLRenderingContext.bindTexture(), WebGLRenderingContext.texImage2D(), and others.
#region Textures

    public const int NEAREST = 0x2600;
    
    public const int LINEAR = 0x2601;
    
    public const int NEAREST_MIPMAP_NEAREST = 0x2700;
    
    public const int LINEAR_MIPMAP_NEAREST = 0x2701;
    
    public const int NEAREST_MIPMAP_LINEAR = 0x2702;
    
    public const int LINEAR_MIPMAP_LINEAR = 0x2703;
    
    public const int TEXTURE_MAG_FILTER = 0x2800;
    
    public const int TEXTURE_MIN_FILTER = 0x2801;
    
    public const int TEXTURE_WRAP_S = 0x2802;
    
    public const int TEXTURE_WRAP_T = 0x2803;
    
    public const int TEXTURE_2D = 0x0DE1;
    
    public const int TEXTURE = 0x1702;
    
    public const int TEXTURE_CUBE_MAP = 0x8513;
    
    public const int TEXTURE_BINDING_CUBE_MAP = 0x8514;
    
    public const int TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515;
    
    public const int TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516;
    
    public const int TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517;
    
    public const int TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518;
    
    public const int TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519;
    
    public const int TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A;
    
    public const int MAX_CUBE_MAP_TEXTURE_SIZE = 0x851C;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE0 =	0x84C0;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE1 =	0x84C1;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE2 =	0x84C2;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE3 =	0x84C3;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE4 =	0x84C4;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE5 =	0x84C5;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE6 =	0x84C6;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE7 =	0x84C7;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE8 =	0x84C8;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE9 =	0x84C9;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE10 = 0x84CA;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE11 = 0x84CB;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE12 = 0x84CC;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE13 = 0x84CD;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE14 = 0x84CE;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE15 = 0x84CF;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE16 = 0x84D0;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE17 = 0x84D1;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE18 = 0x84D2;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE19 = 0x84D3;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE20 = 0x84D4;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE21 = 0x84D5;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE22 = 0x84D6;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE23 = 0x84D7;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE24 = 0x84D8;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE25 = 0x84D9;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE26 = 0x84DA;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE27 = 0x84DB;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE28 = 0x84DC;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE29 = 0x84DD;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE30 = 0x84DE;

    /// <summary>
    /// A texture unit
    /// </summary>
    public const int TEXTURE31 = 0x84DF;

    /// <summary>
    /// The current active texture unit.
    /// </summary>
    public const int ACTIVE_TEXTURE = 0x84E0;

    public const int REPEAT = 0x2901;

    public const int CLAMP_TO_EDGE = 0x812F;

    public const int MIRRORED_REPEAT = 0x8370;
    
#endregion

#region Uniform types

    public const int FLOAT_VEC2 = 0x8B50;

    public const int FLOAT_VEC3 = 0x8B51;

    public const int FLOAT_VEC4 = 0x8B52;

    public const int INT_VEC2 = 0x8B53;

    public const int INT_VEC3 = 0x8B54;

    public const int INT_VEC4 = 0x8B55;

    public const int BOOL = 0x8B56;

    public const int BOOL_VEC2 = 0x8B57;

    public const int BOOL_VEC3 = 0x8B58;

    public const int BOOL_VEC4 = 0x8B59;

    public const int FLOAT_MAT2 = 0x8B5A;

    public const int FLOAT_MAT3 = 0x8B5B;

    public const int FLOAT_MAT4 = 0x8B5C;

    public const int SAMPLER_2D = 0x8B5E;

    public const int SAMPLER_CUBE = 0x8B60;

#endregion

#region Shader precision-specified types

    public const int LOW_FLOAT = 0x8DF0;

    public const int MEDIUM_FLOAT = 0x8DF1;

    public const int HIGH_FLOAT = 0x8DF2;

    public const int LOW_INT = 0x8DF3;

    public const int MEDIUM_INT = 0x8DF4;

    public const int HIGH_INT = 0x8DF5;

#endregion

#region Framebuffers and renderbuffers

    public const int FRAMEBUFFER = 0x8D40;
    
    public const int RENDERBUFFER = 0x8D41;
    
    public const int RGBA4 = 0x8056;
    
    public const int RGB5_A1 = 0x8057;
    
    public const int RGB565 = 0x8D62;
    
    public const int DEPTH_COMPONENT16 = 0x81A5;
    
    public const int STENCIL_INDEX8 = 0x8D48;
    
    public const int DEPTH_STENCIL = 0x84F9;
    
    public const int RENDERBUFFER_WIDTH = 0x8D42;
    
    public const int RENDERBUFFER_HEIGHT = 0x8D43;
    
    public const int RENDERBUFFER_INTERNAL_FORMAT = 0x8D44;
    
    public const int RENDERBUFFER_RED_SIZE = 0x8D50;
    
    public const int RENDERBUFFER_GREEN_SIZE = 0x8D51;
    
    public const int RENDERBUFFER_BLUE_SIZE = 0x8D52;
    
    public const int RENDERBUFFER_ALPHA_SIZE = 0x8D53;
    
    public const int RENDERBUFFER_DEPTH_SIZE = 0x8D54;
    
    public const int RENDERBUFFER_STENCIL_SIZE = 0x8D55;
    
    public const int FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = 0x8CD0;
    
    public const int FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = 0x8CD1;
    
    public const int FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = 0x8CD2;
    
    public const int FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = 0x8CD3;
    
    public const int COLOR_ATTACHMENT0 = 0x8CE0;
    
    public const int DEPTH_ATTACHMENT = 0x8D00;
    
    public const int STENCIL_ATTACHMENT = 0x8D20;
    
    public const int DEPTH_STENCIL_ATTACHMENT = 0x821A;
    
    public const int NONE = 0;
    
    public const int FRAMEBUFFER_COMPLETE = 0x8CD5;
    
    public const int FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6;
    
    public const int FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7;
    
    public const int FRAMEBUFFER_INCOMPLETE_DIMENSIONS = 0x8CD9;
    
    public const int FRAMEBUFFER_UNSUPPORTED = 0x8CDD;
    
    public const int FRAMEBUFFER_BINDING = 0x8CA6;
    
    public const int RENDERBUFFER_BINDING = 0x8CA7;
    
    public const int MAX_RENDERBUFFER_SIZE = 0x84E8;
    
    public const int INVALID_FRAMEBUFFER_OPERATION = 0x0506;

#endregion

// Constants passed to WebGLRenderingContext.pixelStorei().
#region Pixel storage modes
    
    public const int UNPACK_FLIP_Y_WEBGL = 0x9240;
    
    public const int UNPACK_PREMULTIPLY_ALPHA_WEBGL = 0x9241;
    
    public const int UNPACK_COLORSPACE_CONVERSION_WEBGL = 0x9243;

#endregion

}