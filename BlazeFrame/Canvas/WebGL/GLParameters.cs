using BlazeFrame.Canvas.WebGL.GLObjects;
using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.WebGL;

public record GLParameter<T>(uint Name);

public class GLParameters
{
    public static readonly GLParameter<uint> ACTIVE_TEXTURE = new(GL.ACTIVE_TEXTURE);
    public static readonly GLParameter<float[]> ALIASED_LINE_WIDTH_RANGE = new(GL.ALIASED_LINE_WIDTH_RANGE);
    public static readonly GLParameter<float[]> ALIASED_POINT_SIZE_RANGE = new(GL.ALIASED_POINT_SIZE_RANGE);
    public static readonly GLParameter<int> ALPHA_BITS = new(GL.ALPHA_BITS);
    public static readonly GLParameter<WebGLBuffer> ARRAY_BUFFER_BINDING = new(GL.ARRAY_BUFFER_BINDING);
    public static readonly GLParameter<bool> BLEND = new(GL.BLEND);
    public static readonly GLParameter<float[]> BLEND_COLOR = new(GL.BLEND_COLOR);
    public static readonly GLParameter<uint> BLEND_DST_ALPHA = new(GL.BLEND_DST_ALPHA);
    public static readonly GLParameter<uint> BLEND_DST_RGB = new(GL.BLEND_DST_RGB);
    public static readonly GLParameter<uint> BLEND_EQUATION = new(GL.BLEND_EQUATION);
    public static readonly GLParameter<uint> BLEND_EQUATION_ALPHA = new(GL.BLEND_EQUATION_ALPHA);
    public static readonly GLParameter<uint> BLEND_EQUATION_RGB = new(GL.BLEND_EQUATION_RGB);
    public static readonly GLParameter<uint> BLEND_SRC_ALPHA = new(GL.BLEND_SRC_ALPHA);
    public static readonly GLParameter<uint> BLEND_SRC_RGB = new(GL.BLEND_SRC_RGB);
    public static readonly GLParameter<int> BLUE_BITS = new(GL.BLUE_BITS);
    public static readonly GLParameter<float[]> COLOR_CLEAR_VALUE = new(GL.COLOR_CLEAR_VALUE);
    public static readonly GLParameter<bool[]> COLOR_WRITEMASK = new(GL.COLOR_WRITEMASK);
    public static readonly GLParameter<uint[]> COMPRESSED_TEXTURE_FORMATS = new(GL.COMPRESSED_TEXTURE_FORMATS);
    public static readonly GLParameter<bool> CULL_FACE = new(GL.CULL_FACE);
    public static readonly GLParameter<uint> CULL_FACE_MODE = new(GL.CULL_FACE_MODE);
    public static readonly GLParameter<IJSObjectReference> CURRENT_PROGRAM = new(GL.CURRENT_PROGRAM);
    public static readonly GLParameter<int> DEPTH_BITS = new(GL.DEPTH_BITS);
    public static readonly GLParameter<float> DEPTH_CLEAR_VALUE = new(GL.DEPTH_CLEAR_VALUE);
    public static readonly GLParameter<uint> DEPTH_FUNC = new(GL.DEPTH_FUNC);
    public static readonly GLParameter<float[]> DEPTH_RANGE = new(GL.DEPTH_RANGE);
    public static readonly GLParameter<bool> DEPTH_TEST = new(GL.DEPTH_TEST);
    public static readonly GLParameter<bool> DEPTH_WRITEMASK = new(GL.DEPTH_WRITEMASK);
    public static readonly GLParameter<bool> DITHER = new(GL.DITHER);
    public static readonly GLParameter<WebGLBuffer> ELEMENT_ARRAY_BUFFER_BINDING = new(GL.ELEMENT_ARRAY_BUFFER_BINDING);
    public static readonly GLParameter<WebGLFramebuffer?> FRAMEBUFFER_BINDING = new(GL.FRAMEBUFFER_BINDING);
    public static readonly GLParameter<uint> FRONT_FACE = new(GL.FRONT_FACE);
    public static readonly GLParameter<uint> GENERATE_MIPMAP_HINT = new(GL.GENERATE_MIPMAP_HINT);
    public static readonly GLParameter<int> GREEN_BITS = new(GL.GREEN_BITS);
    public static readonly GLParameter<uint> IMPLEMENTATION_COLOR_READ_FORMAT = new(GL.IMPLEMENTATION_COLOR_READ_FORMAT);
    public static readonly GLParameter<uint> IMPLEMENTATION_COLOR_READ_TYPE = new(GL.IMPLEMENTATION_COLOR_READ_TYPE);
    public static readonly GLParameter<float> LINE_WIDTH = new(GL.LINE_WIDTH);
    public static readonly GLParameter<int> MAX_COMBINED_TEXTURE_IMAGE_UNITS = new(GL.MAX_COMBINED_TEXTURE_IMAGE_UNITS);
    public static readonly GLParameter<int> MAX_CUBE_MAP_TEXTURE_SIZE = new(GL.MAX_CUBE_MAP_TEXTURE_SIZE);
    public static readonly GLParameter<int> MAX_FRAGMENT_UNIFORM_VECTORS = new(GL.MAX_FRAGMENT_UNIFORM_VECTORS);
    public static readonly GLParameter<int> MAX_RENDERBUFFER_SIZE = new(GL.MAX_RENDERBUFFER_SIZE);
    public static readonly GLParameter<int> MAX_TEXTURE_IMAGE_UNITS = new(GL.MAX_TEXTURE_IMAGE_UNITS);
    public static readonly GLParameter<int> MAX_TEXTURE_SIZE = new(GL.MAX_TEXTURE_SIZE);
    public static readonly GLParameter<int> MAX_VARYING_VECTORS = new(GL.MAX_VARYING_VECTORS);
    public static readonly GLParameter<int> MAX_VERTEX_ATTRIBS = new(GL.MAX_VERTEX_ATTRIBS);
    public static readonly GLParameter<int> MAX_VERTEX_TEXTURE_IMAGE_UNITS = new(GL.MAX_VERTEX_TEXTURE_IMAGE_UNITS);
    public static readonly GLParameter<int> MAX_VERTEX_UNIFORM_VECTORS = new(GL.MAX_VERTEX_UNIFORM_VECTORS);
    public static readonly GLParameter<int[]> MAX_VIEWPORT_DIMS = new(GL.MAX_VIEWPORT_DIMS);
    public static readonly GLParameter<int> PACK_ALIGNMENT = new(GL.PACK_ALIGNMENT);
    public static readonly GLParameter<float> POLYGON_OFFSET_FACTOR = new(GL.POLYGON_OFFSET_FACTOR);
    public static readonly GLParameter<bool> POLYGON_OFFSET_FILL = new(GL.POLYGON_OFFSET_FILL);
    public static readonly GLParameter<float> POLYGON_OFFSET_UNITS = new(GL.POLYGON_OFFSET_UNITS);
    public static readonly GLParameter<int> RED_BITS = new(GL.RED_BITS);
    public static readonly GLParameter<WebGLRenderbuffer?> RENDERBUFFER_BINDING = new(GL.RENDERBUFFER_BINDING);
    public static readonly GLParameter<string> RENDERER = new(GL.RENDERER);
    public static readonly GLParameter<int> SAMPLE_BUFFERS = new(GL.SAMPLE_BUFFERS);
    public static readonly GLParameter<bool> SAMPLE_COVERAGE_INVERT = new(GL.SAMPLE_COVERAGE_INVERT);
    public static readonly GLParameter<float> SAMPLE_COVERAGE_VALUE = new(GL.SAMPLE_COVERAGE_VALUE);
    public static readonly GLParameter<int> SAMPLES = new(GL.SAMPLES);
    public static readonly GLParameter<int[]> SCISSOR_BOX = new(GL.SCISSOR_BOX);
    public static readonly GLParameter<bool> SCISSOR_TEST = new(GL.SCISSOR_TEST);
    public static readonly GLParameter<string> SHADING_LANGUAGE_VERSION = new(GL.SHADING_LANGUAGE_VERSION);
    public static readonly GLParameter<uint> STENCIL_BACK_FAIL = new(GL.STENCIL_BACK_FAIL);
    public static readonly GLParameter<uint> STENCIL_BACK_FUNC = new(GL.STENCIL_BACK_FUNC);
    public static readonly GLParameter<uint> STENCIL_BACK_PASS_DEPTH_FAIL = new(GL.STENCIL_BACK_PASS_DEPTH_FAIL);
    public static readonly GLParameter<uint> STENCIL_BACK_PASS_DEPTH_PASS = new(GL.STENCIL_BACK_PASS_DEPTH_PASS);
    public static readonly GLParameter<int> STENCIL_BACK_REF = new(GL.STENCIL_BACK_REF);
    public static readonly GLParameter<uint> STENCIL_BACK_VALUE_MASK = new(GL.STENCIL_BACK_VALUE_MASK);
    public static readonly GLParameter<uint> STENCIL_BACK_WRITEMASK = new(GL.STENCIL_BACK_WRITEMASK);
    public static readonly GLParameter<int> STENCIL_BITS = new(GL.STENCIL_BITS);
    public static readonly GLParameter<int> STENCIL_CLEAR_VALUE = new(GL.STENCIL_CLEAR_VALUE);
    public static readonly GLParameter<uint> STENCIL_FAIL = new(GL.STENCIL_FAIL);
    public static readonly GLParameter<uint> STENCIL_FUNC = new(GL.STENCIL_FUNC);
    public static readonly GLParameter<uint> STENCIL_PASS_DEPTH_FAIL = new(GL.STENCIL_PASS_DEPTH_FAIL);
    public static readonly GLParameter<uint> STENCIL_PASS_DEPTH_PASS = new(GL.STENCIL_PASS_DEPTH_PASS);
    public static readonly GLParameter<int> STENCIL_REF = new(GL.STENCIL_REF);
    public static readonly GLParameter<bool> STENCIL_TEST = new(GL.STENCIL_TEST);
    public static readonly GLParameter<uint> STENCIL_VALUE_MASK = new(GL.STENCIL_VALUE_MASK);
    public static readonly GLParameter<uint> STENCIL_WRITEMASK = new(GL.STENCIL_WRITEMASK);
    public static readonly GLParameter<int> SUBPIXEL_BITS = new(GL.SUBPIXEL_BITS);
    public static readonly GLParameter<WebGLTexture?> TEXTURE_BINDING_2D = new(GL.TEXTURE_BINDING_2D);
    public static readonly GLParameter<WebGLTexture?> TEXTURE_BINDING_CUBE_MAP = new(GL.TEXTURE_BINDING_CUBE_MAP);
    public static readonly GLParameter<int> UNPACK_ALIGNMENT = new(GL.UNPACK_ALIGNMENT);
    public static readonly GLParameter<uint> UNPACK_COLORSPACE_CONVERSION_WEBGL = new(GL.UNPACK_COLORSPACE_CONVERSION_WEBGL);
    public static readonly GLParameter<bool> UNPACK_FLIP_Y_WEBGL = new(GL.UNPACK_FLIP_Y_WEBGL);
    public static readonly GLParameter<bool> UNPACK_PREMULTIPLY_ALPHA_WEBGL = new(GL.UNPACK_PREMULTIPLY_ALPHA_WEBGL);
    public static readonly GLParameter<string> VENDOR = new(GL.VENDOR);
    public static readonly GLParameter<string> VERSION = new(GL.VERSION);
    public static readonly GLParameter<int[]> VIEWPORT = new(GL.VIEWPORT);

    // TODO: add webgl2 parameters and webgl extension parameters https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getParameter
}

public class GLFramebufferAttachmentParameter
{
    public static readonly GLParameter<uint> FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE = new(GL.FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE);
    public static readonly GLParameter<WebGLTexture> FRAMEBUFFER_ATTACHMENT_OBJECT_NAME = new(GL.FRAMEBUFFER_ATTACHMENT_OBJECT_NAME);
    public static readonly GLParameter<WebGLRenderbuffer> FRAMEBUFFER_ATTACHMENT_OBJECT_NAME_RENDERBUFFER = new(GL.FRAMEBUFFER_ATTACHMENT_OBJECT_NAME);
    public static readonly GLParameter<int> FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL = new(GL.FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL);
    public static readonly GLParameter<uint> FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE = new(GL.FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE);
    // public static readonly GLParameter<int> FRAMEBUFFER_ATTACHMENT_ALPHA_SIZE = new(GL.FRAMEBUFFER_ATTACHMENT_ALPHA_SIZE);
    // public static readonly GLParameter<int> FRAMEBUFFER_ATTACHMENT_BLUE_SIZE = new(GL.FRAMEBUFFER_ATTACHMENT_BLUE_SIZE);
    // public static readonly GLParameter<uint> FRAMEBUFFER_ATTACHMENT_COLOR_ENCODING = new(GL.FRAMEBUFFER_ATTACHMENT_COLOR_ENCODING);
    // public static readonly GLParameter<uint> FRAMEBUFFER_ATTACHMENT_COMPONENT_TYPE = new(GL.FRAMEBUFFER_ATTACHMENT_COMPONENT_TYPE);
    // public static readonly GLParameter<int> FRAMEBUFFER_ATTACHMENT_DEPTH_SIZE = new(GL.FRAMEBUFFER_ATTACHMENT_DEPTH_SIZE);
    // public static readonly GLParameter<int> FRAMEBUFFER_ATTACHMENT_GREEN_SIZE = new(GL.FRAMEBUFFER_ATTACHMENT_GREEN_SIZE);
    // public static readonly GLParameter<int> FRAMEBUFFER_ATTACHMENT_RED_SIZE = new(GL.FRAMEBUFFER_ATTACHMENT_RED_SIZE);
    // public static readonly GLParameter<int> FRAMEBUFFER_ATTACHMENT_STENCIL_SIZE = new(GL.FRAMEBUFFER_ATTACHMENT_STENCIL_SIZE);
    // public static readonly GLParameter<int> FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER = new(GL.FRAMEBUFFER_ATTACHMENT_TEXTURE_LAYER);
    // public static readonly GLParameter<uint> FRAMEBUFFER_ATTACHMENT_COLOR_ENCODING_EXT = new(GL.FRAMEBUFFER_ATTACHMENT_COLOR_ENCODING_EXT);
    // public static readonly GLParameter<int> FRAMEBUFFER_ATTACHMENT_TEXTURE_NUM_VIEWS_OVR = new(GL.FRAMEBUFFER_ATTACHMENT_TEXTURE_NUM_VIEWS_OVR);
    // public static readonly GLParameter<int> FRAMEBUFFER_ATTACHMENT_TEXTURE_BASE_VIEW_INDEX_OVR = new(GL.FRAMEBUFFER_ATTACHMENT_TEXTURE_BASE_VIEW_INDEX_OVR);
}

public class GLRenderbufferParameter
{
    public static readonly GLParameter<int> RENDERBUFFER_WIDTH = new(GL.RENDERBUFFER_WIDTH);
    public static readonly GLParameter<int> RENDERBUFFER_HEIGHT = new(GL.RENDERBUFFER_HEIGHT);
    public static readonly GLParameter<uint> RENDERBUFFER_INTERNAL_FORMAT = new(GL.RENDERBUFFER_INTERNAL_FORMAT);
    public static readonly GLParameter<int> RENDERBUFFER_GREEN_SIZE = new(GL.RENDERBUFFER_GREEN_SIZE);
    public static readonly GLParameter<int> RENDERBUFFER_BLUE_SIZE = new(GL.RENDERBUFFER_BLUE_SIZE);
    public static readonly GLParameter<int> RENDERBUFFER_RED_SIZE = new(GL.RENDERBUFFER_RED_SIZE);
    public static readonly GLParameter<int> RENDERBUFFER_ALPHA_SIZE = new(GL.RENDERBUFFER_ALPHA_SIZE);
    public static readonly GLParameter<int> RENDERBUFFER_DEPTH_SIZE = new(GL.RENDERBUFFER_DEPTH_SIZE);
    public static readonly GLParameter<int> RENDERBUFFER_STENCIL_SIZE = new(GL.RENDERBUFFER_STENCIL_SIZE);
}

public class GLTextureParameter
{
    public static readonly GLParameter<uint> TEXTURE_MAG_FILTER = new(GL.TEXTURE_MAG_FILTER);
    public static readonly GLParameter<uint> TEXTURE_MIN_FILTER = new(GL.TEXTURE_MIN_FILTER);
    public static readonly GLParameter<uint> TEXTURE_WRAP_S = new(GL.TEXTURE_WRAP_S);
    public static readonly GLParameter<uint> TEXTURE_WRAP_T = new(GL.TEXTURE_WRAP_T);
    
    // TODO: add webgl2 parameters and webgl extension parameters https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getTexParameter
}

public class GLProgramParameters
{
    public static readonly GLParameter<bool> DELETE_STATUS = new(GL.DELETE_STATUS);
    public static readonly GLParameter<bool> LINK_STATUS = new(GL.LINK_STATUS);
    public static readonly GLParameter<bool> VALIDATE_STATUS = new(GL.VALIDATE_STATUS);
    public static readonly GLParameter<int> ATTACHED_SHADERS = new(GL.ATTACHED_SHADERS);
    public static readonly GLParameter<int> ACTIVE_ATTRIBUTES = new(GL.ACTIVE_ATTRIBUTES);
    public static readonly GLParameter<int> ACTIVE_UNIFORMS = new(GL.ACTIVE_UNIFORMS);
    
    // TODO: add webgl2 parameters and webgl extension parameters https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getProgramParameter#gl.delete_status
}

public class GLShaderParameters
{
    public static readonly GLParameter<bool> DELETE_STATUS = new(GL.DELETE_STATUS);
    public static readonly GLParameter<bool> COMPILE_STATUS = new(GL.COMPILE_STATUS);
    public static readonly GLParameter<uint> SHADER_TYPE = new(GL.SHADER_TYPE);
}

public class GLVertexAttribParameters
{
    public static readonly GLParameter<WebGLBuffer> VERTEX_ATTRIB_ARRAY_BUFFER_BINDING = new(GL.VERTEX_ATTRIB_ARRAY_BUFFER_BINDING);
    public static readonly GLParameter<int> VERTEX_ATTRIB_ARRAY_SIZE = new(GL.VERTEX_ATTRIB_ARRAY_SIZE);
    public static readonly GLParameter<int> VERTEX_ATTRIB_ARRAY_STRIDE = new(GL.VERTEX_ATTRIB_ARRAY_STRIDE);
    public static readonly GLParameter<uint> VERTEX_ATTRIB_ARRAY_TYPE = new(GL.VERTEX_ATTRIB_ARRAY_TYPE);
    public static readonly GLParameter<bool> VERTEX_ATTRIB_ARRAY_NORMALIZED = new(GL.VERTEX_ATTRIB_ARRAY_NORMALIZED);
    public static readonly GLParameter<float[]> CURRENT_VERTEX_ATTRIB = new(GL.CURRENT_VERTEX_ATTRIB);
    
    // TODO: add webgl2 parameters and webgl extension parameters https://developer.mozilla.org/en-US/docs/Web/API/WebGLRenderingContext/getVertexAttrib
}