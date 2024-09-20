namespace BlazeFrame.Logic;

public record JSFont(string SizeStyle, string Font) 
{
    public static JSFont Default => new("12px", "Arial");

    public override string ToString() => $"{SizeStyle} {Font}";

    internal JSFont WithSize(string size) => new(size, Font);
    
    internal JSFont WithFont(string font) => new(SizeStyle, font);

    public static implicit operator string(JSFont value) => value.ToString();

    public static implicit operator JSFont(string value) {
        var splits =  value.Split(' ', 1);
        return new(splits[0], splits[1]);
    }
}