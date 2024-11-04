using System.Text.Json;

namespace BlazeFrame.JSInterop;

public class ArrayProxy<T> : Proxy<T?[]>
{
    public override void SetValue(JsonElement json)
    {
        if(json.ValueKind == JsonValueKind.Null)
        {
            Value = null;
            HasValue = true;
            return;
        }
        if(json.ValueKind != JsonValueKind.Array)
        {
            throw new InvalidOperationException("Invalid JSON type");
        }

        var array = new T?[json.GetArrayLength()];
        var jsonArray = json.EnumerateArray();
        for(int i = 0; i < array.Length; i++)
        {
            if(!jsonArray.MoveNext())
                break;
            array[i] = ProxyOperationHelper.CreatePotentialProxy<T>(jsonArray.Current);
        }
        
        Value = array;
        HasValue = true;
    }
}
