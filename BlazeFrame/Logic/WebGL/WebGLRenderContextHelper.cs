using BlazeFrame.Canvas.WebGL;
using BlazeFrame.Canvas.WebGL.GLObjects;
using System.Drawing;
using System.Numerics;

namespace BlazeFrame.Logic.WebGL;

public static class WebGLRenderingContextHelper
{
    public static void ClearColor(this WebGLRenderingContext context, Color color)
    {
        context.ClearColor(color.R, color.G, color.B, color.A).ConfigureAwait(false);
    }

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="v0">A floating point number for floating point values</param>
    public static async void Uniform1f(this WebGLRenderingContext context, WebGLProgram program, string name, float v0) =>
        await context.SetUniform(program, name, "uniform1f", v0);
        
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="value">A sequence of floating point numbers for floating point vector methods</param>
    public static async void Uniform1fv(this WebGLRenderingContext context, WebGLProgram program, string name, float[] value) =>
        await context.SetUniform(program, name, "uniform1fv", value);
    
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="v0">An integer number for integer values</param>
    public static async void Uniform1i(this WebGLRenderingContext context, WebGLProgram program, string name, int v0) =>
        await context.SetUniform(program, name, "uniform1i", v0);
        
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="value">An Int32Array for integer vector methods</param>
    public static async void Uniform1iv(this WebGLRenderingContext context, WebGLProgram program, string name, int[] value) =>
        await context.SetUniform(program, name, "uniform1iv", value);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="v0">A floating point number for floating point values</param>
    /// <param name="v1">A floating point number for floating point values</param>    
    public static async void Uniform2f(this WebGLRenderingContext context, WebGLProgram program, string name, float v0, float v1) =>
        await context.SetUniform(program, name, "uniform2fv", new float[] { v0, v1 });
    
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="value">A sequence of floating point numbers for floating point vector methods</param>
    public static async void Uniform2fv(this WebGLRenderingContext context, WebGLProgram program, string name, float[] value) =>
        await context.SetUniform(program, name, "uniform2fv", value);

        
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="v0">An integer number for integer values</param>
    /// <param name="v1">An integer number for integer values</param>
    public static async void Uniform2i(this WebGLRenderingContext context, WebGLProgram program, string name, int v0, int v1) =>
        await context.SetUniform(program, name, "uniform2iv", new int[] { v0, v1 });
    
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="value">An Int32Array for integer vector methods</param>
    public static async void Uniform2iv(this WebGLRenderingContext context, WebGLProgram program, string name, int[] value) =>
        await context.SetUniform(program, name, "uniform2iv", value);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="v0">A floating point number for floating point values</param>
    /// <param name="v1">A floating point number for floating point values</param>    
    /// <param name="v2">A floating point number for floating point values</param>    
    public static async void Uniform3f(this WebGLRenderingContext context, WebGLProgram program, string name, float v0, float v1, float v2) =>
        await context.SetUniform(program, name, "uniform3fv", new float[] { v0, v1, v2 });
    
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="value">A sequence of floating point numbers for floating point vector methods</param>
    public static async void Uniform3fv(this WebGLRenderingContext context, WebGLProgram program, string name, float[] value) =>
        await context.SetUniform(program, name, "uniform3fv", value);

        
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="v0">An integer number for integer values</param>
    /// <param name="v1">An integer number for integer values</param>
    /// <param name="v2">An integer number for integer values</param>
    public static async void Uniform3i(this WebGLRenderingContext context, WebGLProgram program, string name, int v0, int v1, int v2) =>
        await context.SetUniform(program, name, "uniform3iv", new int[] { v0, v1, v2 });
    
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="value">An Int32Array for integer vector methods</param>
    public static async void Uniform3iv(this WebGLRenderingContext context, WebGLProgram program, string name, int[] value) =>
        await context.SetUniform(program, name, "uniform3iv", value);

    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="v0">A floating point number for floating point values</param>
    /// <param name="v1">A floating point number for floating point values</param>    
    /// <param name="v2">A floating point number for floating point values</param>    
    /// <param name="v3">A floating point number for floating point values</param>    
    public static async void Uniform4f(this WebGLRenderingContext context, WebGLProgram program, string name, float v0, float v1, float v2, float v3) =>
        await context.SetUniform(program, name, "uniform4fv", new float[] { v0, v1, v2, v3 });
    
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="value">A sequence of floating point numbers for floating point vector methods</param>
    public static async void Uniform4fv(this WebGLRenderingContext context, WebGLProgram program, string name, float[] value) =>
        await context.SetUniform(program, name, "uniform4fv", value);

        
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="v0">An integer number for integer values</param>
    /// <param name="v1">An integer number for integer values</param>
    /// <param name="v2">An integer number for integer values</param>
    /// <param name="v3">An integer number for integer values</param>
    public static async void Uniform4i(this WebGLRenderingContext context, WebGLProgram program, string name, int v0, int v1, int v2, int v3) =>
        await context.SetUniform(program, name, "uniform4iv", new int[] { v0, v1, v2, v3 });
    
    /// <summary>
    /// specify values of uniform variables. All active uniform variables defined in a program object are initialized to 0 when the program object is linked successfully. 
    /// They retain the values assigned to them by a call to this method until the next successful link operation occurs on the program object, 
    /// when they are once again initialized to 0.
    /// 
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="value">An Int32Array for integer vector methods</param>
    public static async void Uniform4iv(this WebGLRenderingContext context, WebGLProgram program, string name, int[] value) =>
        await context.SetUniform(program, name, "uniform4iv", value);

    /// <summary>
    /// specify matrix values for uniform variables.
    /// The three versions of this method (uniformMatrix2fv(), uniformMatrix3fv(), and uniformMatrix4fv()) take as the input value 
    /// 2-component, 3-component, and 4-component square matrices, respectively. They are expected to have 4, 9 or 16 floats.
    ///
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="transpose">A GLboolean specifying whether to transpose the matrix. Must be false.</param>
    /// <param name="value">A Float32Array or sequence of GLfloat values. The values are assumed to be supplied in column major order.</param>
    public static async void UniformMatrix2fv(this WebGLRenderingContext context, WebGLProgram program, string name, bool transpose, float[] value) =>
        await context.SetMatrixUniform(program, name, "uniformMatrix2fv", transpose, value);

    /// <summary>
    /// specify matrix values for uniform variables.
    /// The three versions of this method (uniformMatrix2fv(), uniformMatrix3fv(), and uniformMatrix4fv()) take as the input value 
    /// 2-component, 3-component, and 4-component square matrices, respectively. They are expected to have 4, 9 or 16 floats.
    ///
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="transpose">A GLboolean specifying whether to transpose the matrix. Must be false.</param>
    /// <param name="value">A Float32Array or sequence of GLfloat values. The values are assumed to be supplied in column major order.</param>
    public static async void UniformMatrix3fv(this WebGLRenderingContext context, WebGLProgram program, string name, bool transpose, float[] value) =>
        await context.SetMatrixUniform(program, name, "uniformMatrix3fv", transpose, value);

    /// <summary>
    /// specify matrix values for uniform variables.
    /// The three versions of this method (uniformMatrix2fv(), uniformMatrix3fv(), and uniformMatrix4fv()) take as the input value 
    /// 2-component, 3-component, and 4-component square matrices, respectively. They are expected to have 4, 9 or 16 floats.
    ///
    /// This method makes it possible to batch the calls, which can improve performance. compared to getting the location and setting the uniform value in two separate calls.
    /// </summary>
    /// <param name="context">The context to set the uniform into</param>
    /// <param name="program">The program to find the uniform of</param>
    /// <param name="name">The name of the uniform location</param>
    /// <param name="transpose">A GLboolean specifying whether to transpose the matrix. Must be false.</param>
    /// <param name="value">A Float32Array or sequence of GLfloat values. The values are assumed to be supplied in column major order.</param>
    public static async void UniformMatrix4fv(this WebGLRenderingContext context, WebGLProgram program, string name, bool transpose, float[] value) =>
        await context.SetMatrixUniform(program, name, "uniformMatrix4fv", transpose, value);

    private static async Task SetUniform(this WebGLRenderingContext context, WebGLProgram program, string name, string type, object value)
    {
        if(!JSInvoker.INSTANCE.InvokeBatched(null, "setUniform", context, program, name, type, value))
            await JSInvoker.INSTANCE.InvokeVoidAsync(null, "setUniform", context, program, name, type, value);
    }

    private static async Task SetMatrixUniform(this WebGLRenderingContext context, WebGLProgram program, string name, string type, bool transpose, object value)
    {
        if(!JSInvoker.INSTANCE.InvokeBatched(null, "setMatrixUniform", context, program, name, type, transpose, value))
            await JSInvoker.INSTANCE.InvokeVoidAsync(null, "setMatrixUniform", context, program, name, type, transpose, value);
    }
}
