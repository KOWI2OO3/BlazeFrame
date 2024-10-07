using System.Drawing;
using BlazeFrame.Canvas.Html;
using BlazeFrame.Canvas.WebGL;
using BlazeFrame.Element;
using BlazeFrame.Logic;
using BlazeFrame.Logic.WebGL;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazeFrame.Components;

public partial class GLFrame : RenderLoopComponentBase
{
    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    protected readonly string Id = Guid.NewGuid().ToString();

    [Parameter]
    public int Height { get; set; } = 600;

    [Parameter]
    public int Width { get; set; } = 800;

    [Parameter]
    public string Class { get; set; } = string.Empty;

    [Parameter]
    public string Style { get; set; } = string.Empty;

    [Parameter]
    public Color? BackgroundColor { get; set; }

    [Parameter]
    public bool AutoScale { get; set; } = true;

    [Parameter]
    public Func<WebGLRenderingContext, Task>? OnRenderFrame { get; set; }

    private ElementReference _canvasRef { get; set; }

    public ElementReference CanvasRef => _canvasRef;

    public WebGLRenderingContext? Context { get; set; }

    public HtmlCanvas? Canvas { get; set; }

    private ParentElement? ParentElement { get; set; }

    private string GetClass()
    {
        var classes = $"bf-canvas {Class}";
        if(AutoScale)
            classes += " bf-canvas-auto-scale";
        return classes;
    }

    protected override async Task Update()
    {
        await FetchDataAsync();

        if(Context == null) return;
        Context.StartBatch();

        if(BackgroundColor != null)
        {
            Context.ClearColor(BackgroundColor.Value);
            await Context.Clear(GL.COLOR_BUFFER_BIT);
        }

        if(OnRenderFrame != null)
            await OnRenderFrame.Invoke(Context);

        await Context.EndBatch();
    }

    /// <summary>
    /// Used to fetch data from the javascript side, this can relate to client rect data or other relevant data.
    /// </summary>
    private async Task FetchDataAsync()
    {
        JSInvoker.INSTANCE ??= await JSInvoker.Create(JSRuntime);

        Canvas ??= await CanvasRef.AsHtmlCanvas();
        Context ??= await CanvasRef.GetWebGLContext();
        ParentElement ??= await CanvasRef.GetParentElement();
    }
}
