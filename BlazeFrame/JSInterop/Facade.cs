using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.JSInterop;

namespace BlazeFrame.JSInterop;

public abstract class Facade 
{
    [JsonPropertyName("facadeId")]
    public Guid Id { get; } = Guid.NewGuid();
    
    [JsonIgnore]
    public JSInvoker? Invoker { internal get; init; }

    [JsonIgnore]
    public bool HasValue { get; set; }

    public abstract void SetValue(JsonElement json);

    public abstract object? GetValue();
}

public class Facade<T> : Facade
{
    [JsonIgnore]
    public T? Value { get; protected set; }

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
        return HasValue ? Value?.ToString() ?? "null" : "facade@" + Id;
    }
}
