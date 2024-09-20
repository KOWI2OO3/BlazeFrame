namespace BlazeFrame.Canvas.Html;

public class DOMPoint
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
    public float w { get; set; }

    public DOMPoint() {}

    public DOMPoint(float x, float y = 0, float z = 0, float w = 0)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
}
