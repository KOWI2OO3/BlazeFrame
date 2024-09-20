using BlazeFrame.Logic;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazeFrame;

public class ModuleJSObject(JSInvoker invoker, IJSObjectReference JSObject)
{
    protected JSInvoker Invoker { get; } = invoker;

    public IJSObjectReference JSObject { get; } = JSObject;

    protected async ValueTask<T> GetProperty<T>(string property) {
        return await Invoker.GetPropertyAsync<T>(JSObject, property);
    }
    
    protected async void SetProperty<T>(string property, T value) {
        if(!Invoker.SetPropertyBatched(JSObject, property, value))
            await Invoker.SetPropertyAsync(JSObject, property, value);
    }

    protected async Task Invoke(string method, params object[] args) {
        if(!Invoker.InvokeBatched(JSObject, method, args))
            await Invoker.InvokeVoidAsync(JSObject, method, args);
    }
}
