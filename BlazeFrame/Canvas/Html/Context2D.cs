using System.Numerics;
using BlazeFrame.Logic;
using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.Html;

public class Context2D(JSInvoker invoker, IJSObjectReference JSObject) : ModuleJSObject(invoker, JSObject)
{
    private string fillStyle { get; set; } = string.Empty;
    public string FillStyle { get => fillStyle; set => SetProperty(nameof(fillStyle), fillStyle = value); }
    
    private string strokeStyle { get; set; } = string.Empty;
    public string StrokeStyle { get => strokeStyle; set => SetProperty(nameof(strokeStyle), strokeStyle = value); }
    
    private double lineWidth { get; set; }
    public double LineWidth { get => lineWidth; set => SetProperty(nameof(lineWidth), lineWidth = value); }

    private EnumStringWrapper<LineJoinType> lineJoin { get; set; } = default(LineJoinType);
    public EnumStringWrapper<LineJoinType> LineJoin { get => lineJoin; set => SetProperty(nameof(lineJoin), Enum.GetName<LineJoinType>(lineJoin = value)?.ToLower()); }

    private EnumStringWrapper<LineCapType> lineCap { get; set; } = default(LineCapType);
    public EnumStringWrapper<LineCapType> LineCap { get => lineCap; set => SetProperty(nameof(lineCap), Enum.GetName<LineCapType>(lineCap = value)?.ToLower()); }

    private JSFont font { get; set; } = JSFont.Default;
    public JSFont Font { get => font; set => SetProperty<string>(nameof(font), font = value); }

    private double shadowBlur { get; set; }
    public double ShadowBlur { get => shadowBlur; set => SetProperty(nameof(shadowBlur), shadowBlur = value); } 

    private string shadowColor { get; set; } = string.Empty;
    public string ShadowColor { get => shadowColor; set => SetProperty(nameof(shadowColor), shadowColor = value); } 

    private double shadowOffsetX  { get; set; }
    public double ShadowOffsetX { get => shadowOffsetX; set => SetProperty(nameof(shadowOffsetX), shadowOffsetX = value); } 

    private double shadowOffsetY  { get; set; }
    public double ShadowOffsetY { get => shadowOffsetY; set => SetProperty(nameof(shadowOffsetY), shadowOffsetY = value); }

    internal async Task InitializeProperties()
    {
        fillStyle = await GetProperty<string>(nameof(fillStyle));
        strokeStyle = await GetProperty<string>(nameof(strokeStyle));
        lineWidth = await GetProperty<double>(nameof(lineWidth));
        lineJoin = EnumHelper.GetByName<LineJoinType>(await GetProperty<string>(nameof(lineJoin)));
        lineCap = EnumHelper.GetByName<LineCapType>(await GetProperty<string>(nameof(lineCap)));
        shadowBlur = await GetProperty<double>(nameof(shadowBlur));
        shadowColor = await GetProperty<string>(nameof(shadowColor));
        shadowOffsetX = await GetProperty<double>(nameof(shadowOffsetX));
        shadowOffsetY = await GetProperty<double>(nameof(shadowOffsetY));
    }

    public void StartBatch() => Invoker.BeginBatch();

    public async Task EndBatch() => await Invoker.EndBatch();

    public async Task ClearRectAsync(int x, int y, int width, int height) =>
        await Invoke("clearRect", x, y, width, height);

    public async Task FillRectAsync(int x, int y, int width, int height) =>
        await Invoke("fillRect", x, y, width, height);

    public async Task BeginPathAsync() =>
        await Invoke("beginPath");

    public async Task LineToAsync(int x, int y) =>
        await Invoke("lineTo", x, y);

    public async Task MoveToAsync(int x, int y) =>
        await Invoke("moveTo", x, y);

    public async Task ClosePathAsync() =>
        await Invoke("closePath");

    public async Task StrokeAsync() =>
        await Invoke("stroke");

    public async Task FillAsync() =>
        await Invoke("fill");

    public async Task ResetAsync() =>
        await Invoke("reset");

    public async Task SaveAsync() =>
        await Invoke("save");

    public async Task RestoreAsync() =>
        await Invoke("restore");

    public async Task ArcAsync(int x, int y, double radius, double startAngle, double endAngle, bool counterClockwise = false) =>
        await Invoke("arc", x, y, radius, startAngle, endAngle, counterClockwise);

    public async Task ArcToAsync(int x1, int y1, int x2, int y2, double radius) =>
        await Invoke("arcTo", x1, y1, x2, y2, radius);

    public async Task QuadraticCurveToAsync(double cpx, double cpy, double x, double y) =>
        await Invoke("quadraticCurveTo", cpx, cpy, x, y);

    public async Task BrazierCurveToAsync(double cp1x, double cp1y, double cp2x, double cp2y, double x, double y) =>
        await Invoke("bezierCurveTo", cp1x, cp1y, cp2x, cp2y, x, y);

    public async Task FillTextAsync(string text, int x, double y) =>
        await Invoke("fillText", text, x, y);

    public async Task FillTextAsync(string text, int x, double y, int maxWidth) =>
        await Invoke("fillText", text, x, y, maxWidth);

    public async Task StrokeTextAsync(string text, int x, int y) =>
        await Invoke("strokeText", text, x, y);

    public async Task StrokeTextAsync(string text, int x, int y, int maxWidth) =>
        await Invoke("strokeText", text, x, y, maxWidth);

    public async Task StrokeRectAsync(int x, int y, int width, int height) =>
        await Invoke("strokeRect", x, y, width, height);

    public async Task RectAsync(int x, int y, int width, int height) =>
        await Invoke("rect", x, y, width, height);

    public async Task RoundRectAsync(int x, int y, int width, int height, int radii) =>
        await Invoke("roundRect", x, y, width, height, radii);

    public async Task RoundRectAsync(int x, int y, int width, int height, int[] radii) =>
        await Invoke("roundRect", x, y, width, height, radii);

    public async Task EllipseAsync(int x, int y, double radiusX, double radiusY, double rotation, double startAngle, double endAngle, bool counterClockwise = false) =>
        await Invoke("ellipse", x, y, radiusX, radiusY, rotation, startAngle, endAngle, counterClockwise);

    public async Task SetLineDashAsync(int[] segments) =>
        await Invoke("setLineDash", segments);

    public async Task SetTransform(double a, double b, double c, double d, double e, double f) =>
        await Invoke("setTransform", a, b, c, d, e, f);

    public async Task SetTransform(Matrix4x4 matrix) =>
        await Invoke("setTransform", (DOMMatrix) matrix);

    public async Task<Matrix4x4> GetTransform() =>
        await GetProperty<DOMMatrix>("getTransform");

    public async Task Scale(float x, float y) =>
        await Invoke("scale", x, y);

    public async Task Rotate(float angle) => 
        await Invoke("rotate", angle);

    public async Task Translate(float x, float y) => 
        await Invoke("translate", x, y);

    public async Task Transform(double a, double b, double c, double d, double e, double f) =>
        await Invoke("transform", a, b, c, d, e, f);

    public async Task ResetTransform() => 
        await Invoke("resetTransform");

    // CreateRadialGradient

    // public async Task<IJSObjectReference> MeasureText()

    public async Task ScaleCanvasToDisplay(HtmlCanvas? canvas = null) {
        if(!Invoker.InvokeBatched(null, "scaleContextToDisplay", JSObject))
            await Invoker.Module!.InvokeVoidAsync("scaleContextToDisplay", JSObject);
        
        if(canvas != null)
            await canvas.InitializePropertiesAsync();
    }

}
