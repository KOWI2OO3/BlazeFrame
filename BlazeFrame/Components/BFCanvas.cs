using BlazeFrame.Canvas.Html;
using BlazeFrame.Element;
using BlazeFrame.Logic;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Drawing;
using System.Reflection.Metadata;

namespace BlazeFrame.Components;

public partial class BFCanvas : ComponentBase
{
    // TODO: Use JSRuntime to not require the services if the user only uses the components?? 
    // TODO: Allow screen scaling (needs experimenting)
    
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
    public Func<Context2D, Task>? OnRenderFrame { get; set; }

    private ElementReference _canvasRef { get; set; }

    public ElementReference CanvasRef => _canvasRef;

    public Context2D? Context { get; set; }

    public HtmlCanvas? Canvas { get; set; }

    private ParentElement? ParentElement { get; set; }

    private string GetClass()
    {
        var classes = $"bf-canvas {Class}";
        if(AutoScale)
            classes += " bf-canvas-auto-scale";
        return classes;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await FetchDataAsync();

        if(Context == null) return;
        Context.StartBatch();

        if(AutoScale)
        {
            if(ParentElement != null) {
                CanvasRef.SetAttributeProperty("width", Width = ParentElement.Width, true);
                CanvasRef.SetAttributeProperty("height", Height = ParentElement.Height, true);
            }
            await Context.ScaleCanvasToDisplay(Canvas);
        }

        if(BackgroundColor != null)
        {
            Context.SetColor(BackgroundColor.Value);
            await Context.FillRectAsync(0, 0, (int)Width, (int)Height);
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

        Canvas ??= await CanvasRef.asHtmlCanvas();
        Context ??= await CanvasRef.GetContext2D();
        ParentElement ??= await CanvasRef.GetParentElement();
    }
}
