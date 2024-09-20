using System.Net;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazeFrame.Canvas.Html;

public static class HtmlCanvasHelper
{
    public static async Task<Context2D> GetContext2D(this ElementReference canvas)
    {
        var jscontext = await JSInvoker.INSTANCE.InvokeAsync<IJSObjectReference>(canvas, "getContext", "2d");
        var context = new Context2D(JSInvoker.INSTANCE, jscontext);
        await context.InitializeProperties();
        return context;
    }

    public static async Task<HtmlCanvas> asHtmlCanvas(this ElementReference element)
    {
        var canvas = new HtmlCanvas(JSInvoker.INSTANCE, element);
        await canvas.InitializePropertiesAsync();
        return canvas;
    }

}
