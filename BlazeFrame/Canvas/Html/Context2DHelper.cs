using System.Drawing;

namespace BlazeFrame.Canvas.Html;

public static class Context2DHelper
{
    public static void SetColor(this Context2D ctx, string color) => ctx.FillStyle = ctx.StrokeStyle = color;

    public static void SetColor(this Context2D ctx, Color color) => ctx.SetColor($"rgba({color.R}, {color.G}, {color.B}, {color.A})");

    public static async Task DrawRectangleAsync(this Context2D ctx, int x, int y, int width, int height, string? color = null)
    {
        if(color != null)
            SetColor(ctx, color);
        
        await ctx.FillRectAsync(x, y, width, height);
    }

    public static async Task DrawRectangleAsync(this Context2D ctx, int x, int y, int width, int height, Color color) => 
        await DrawRectangleAsync(ctx, x, y, width, height, $"rgb({color.R}, {color.G}, {color.B} / {color.A})");

    public static async Task DrawLineAsync(this Context2D ctx, params (int, int)[] points) 
    {
        if(points.Length <= 1) return;
         
        await ctx.BeginPathAsync();
        await ctx.MoveToAsync(points[0].Item1, points[1].Item2);
        for (int i = 1; i < points.Length; i++)
            await ctx.LineToAsync(points[i].Item1, points[i].Item2);
        
        await ctx.StrokeAsync();
    }

    public static async Task DrawCircleAsync(this Context2D ctx, int x, int y, int radius, string? color = null) 
    {
        if(color != null)
            SetColor(ctx, color);

        await ctx.BeginPathAsync();
        await ctx.ArcAsync(x, y, radius, 0, 2 * Math.PI);
        await ctx.FillAsync();
    }

    public static void SetFontSize(this Context2D ctx, string fontSize) => ctx.Font = ctx.Font.WithSize(fontSize);

    public static void SetFont(this Context2D ctx, string font) => ctx.Font = ctx.Font.WithFont(font);
}
