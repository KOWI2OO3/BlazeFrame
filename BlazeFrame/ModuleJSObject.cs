using System.Reflection;
using BlazeFrame.JSInterop;
using Microsoft.JSInterop;

namespace BlazeFrame;

public class ModuleJSObject(JSInvoker invoker, IJSObjectReference JSObject) : IWrapJSObject
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

    protected void InvokeBatched(string method, params object[] args) {
        if(!Invoker.InvokeBatched(JSObject, method, args))
            throw new InvalidOperationException("Batched call failed");
    }

    protected T InvokeBatched<T>(string method, params object[] args) where T : Proxy, new()
    {
        if(!Invoker.InvokeBatched<T>(JSObject, method, out var result, args))
            throw new InvalidOperationException("Batched call failed");
        return result;
    }

    protected async Task<T> Invoke<T>(string method, params object[] args) 
    {
        if(IsBatching() && typeof(T).IsAssignableTo(typeof(Proxy<>)))
        {
            
            var batchedMethod = typeof(ModuleJSObject).GetMethod("InvokeBatched")?.MakeGenericMethod(typeof(T).GetGenericArguments()[0]);
            if(batchedMethod is not null)
                return (T)batchedMethod?.Invoke(this, [JSObject, method, args])!;
        }
        if(typeof(T).IsAssignableTo(typeof(ModuleJSObject)))
        {
            var result = await JSObject.InvokeAsync<IJSObjectReference>(method, args);
            return (T)Activator.CreateInstance(typeof(T), Invoker, result)!;
        }else if(typeof(T).IsAssignableTo(typeof(IWrapJSObject)))
        {
            var result = await JSObject.InvokeAsync<IJSObjectReference>(method, args);
            return (T)Activator.CreateInstance(typeof(T), result)!;
        }else if(typeof(T).IsArray && typeof(T).GetElementType()!.IsAssignableTo(typeof(ModuleJSObject)))
        {
            var result = await JSObject.InvokeAsync<IJSObjectReference[]>(method, args);
            return (T)Activator.CreateInstance(typeof(T), result.Select(x => (T)Activator.CreateInstance(typeof(T).GetElementType()!, x)!).ToArray())!;
        }
        else
            return await JSObject.InvokeAsync<T>(method, args);
    }

    public bool IsBatching() => Invoker.IsBatching();

    public void StartBatch() => Invoker.BeginBatch();

    public async Task EndBatch() => await Invoker.EndBatch();
}
