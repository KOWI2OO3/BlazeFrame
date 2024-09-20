using BlazeFrame.Element;
using BlazeFrame.Logic;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.Html;

public class HtmlCanvas(JSInvoker invoker, ElementReference element)
{
    private readonly JSInvoker Invoker = invoker;

    private readonly ElementReference Element = element;

     private double width { get; set; }
    public double Width { get => width; set => Element.SetAttributeProperty(nameof(width), height = value); }

    private double height { get; set; }
    public double Height { get => height; set => Element.SetAttributeProperty(nameof(height), height = value); }

    internal async Task<HtmlCanvas> InitializePropertiesAsync() {
        width = await Element.GetAttribute<double>(nameof(width));
        height = await Element.GetAttribute<double>(nameof(height));
        return this;
    }

    public async Task<BoundingClientRect> BoundingClientRect() => await Element.GetBoundingClientRect();

    public async Task<(double, double)> MouseToCanvasCoordinates(double mouseX, double mouseY) {
        var rect = await BoundingClientRect();
        
        var scaleX = Width / rect.Width;
        var scaleY = Height / rect.Height;
        return (
            (mouseX - rect.Left) * scaleX,
            (mouseY - rect.Top) * scaleY
        );
    }

    public async Task ScaleCanvasToDisplay() {
        if(!Invoker.InvokeBatched(null, "scaleCanvasToDisplay", Element))
            await Invoker.Module!.InvokeVoidAsync("scaleCanvasToDisplay", Element);
        await InitializePropertiesAsync();
    }

    public async Task<(int, int)> MouseToCanvas((int, int) mousePosition) {
        var point = await Invoker.Module!.InvokeAsync<DOMPoint>("mouseToCanvasCoordinates", Element, mousePosition.Item1, mousePosition.Item2);
        return ((int)point.x, (int)point.y);
    }
}
