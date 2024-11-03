using System.Text.Json;
using System.Text.Json.Serialization;
using BlazeFrame.Logic;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;

namespace BlazeFrame.JSInterop;

public class JSObjectFacade : Facade<JSObjectReference>
{
    [JsonPropertyName("requiresObjectReference")]
    public bool RequiresObjectReference { get; } = true;

    public override void SetValue(JsonElement json)
    {
        var value = JsonSerializer.Deserialize<long>(json);
        if(JSInvoker.INSTANCE.JSRuntime is JSRuntime runtime) {
            Value = JSObjectHelper.CreateJSObjectReference(runtime, value);
            HasValue = true;
        }
    }
}
