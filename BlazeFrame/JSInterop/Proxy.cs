using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazeFrame.JSInterop;

public abstract class Proxy 
{
    [JsonPropertyName("proxyId")]
    public Guid Id { get; internal set; } = Guid.NewGuid();
    
    [JsonIgnore]
    public JSInvoker? Invoker { internal get; init; }

    [JsonIgnore]
    public bool HasValue { get; set; }

    public abstract void SetValue(JsonElement json);

    public abstract object? GetValue();
}

public class Proxy<T> : Proxy
{
    [JsonIgnore]
    public T? Value { get; internal set; }

    public override void SetValue(JsonElement json)
    {
        var value = JsonSerializer.Deserialize<T>(json);
        if(value != null)
        {
            Value = value;
            HasValue = true;
        }
    }

    public override object? GetValue() => Value;

    public override string ToString()
    {
        return HasValue ? Value?.ToString() ?? "null" : "proxy@" + Id;
    }

    public override bool Equals(object? obj) => HasValue ? Value?.Equals(obj) ?? obj == null : base.Equals(obj);
    public override int GetHashCode() => HasValue ? Value?.GetHashCode() ?? 0 : Id.GetHashCode();
}
