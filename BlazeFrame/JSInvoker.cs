using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using BlazeFrame.JSInterop;
using BlazeFrame.Logic;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;

namespace BlazeFrame;

public class JSInvoker
{
    public static JSInvoker INSTANCE { get; internal set; } 

    public const string NAMESPACE = "BlazeFrame";

    public const string SET_PROPERTY = "setProperty";
    public const string GET_PROPERTY = "getProperty";
    public const string INVOKE_FUNCTION = "invokeFunction";
    
    public const string INVOKE_CALLBACK_FUNCTION = "invokeCallbackFunction";
    
    public IJSRuntime? JSRuntime { get; private set; }

    internal IJSObjectReference? Module { get; private set; }

    private readonly List<object?[]> batchedCalls = [];

    private readonly Dictionary<Guid, Facade> batchedFacades = [];

    private bool isBatching = false;

    private JSInvoker(IJSRuntime? jsRuntime) => JSRuntime = jsRuntime;

    private async Task InitializeAsync() {
        if(JSRuntime == null)
            throw new InvalidOperationException("JSRuntime not initialized");
        Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazeFrame/JSInvoker.js");
    }

    public static async Task<JSInvoker> Create(IJSRuntime runtime) {
        var invoker = new JSInvoker(runtime);
        await invoker.InitializeAsync();
        return invoker;
    }

#region Batching

    public void BeginBatch() 
    {
        if(Module == null)
            throw new InvalidOperationException("Module not initialized");
        isBatching = true;
    }

    public bool SetPropertyBatched<T>(object? jSObject, string propertyName, T value) 
    {
        if(!isBatching) return false;
        object?[] batchedCall = [SET_PROPERTY, jSObject, propertyName, value];
        batchedCalls.Add(batchedCall);

        return true;
    }

    public bool InvokeBatched(object? JSObject, string methodName, params object?[] args) 
    {
        if(!isBatching) return false;
        args = ResolveFacades(args);
        
        object?[] batchedCall = [INVOKE_FUNCTION, JSObject, methodName, .. args];
        batchedCalls.Add(batchedCall);

        return true;
    }

    public bool InvokeBatched<K>(object? JSObject, string methodName, out K facade, params object?[] args) where K : Facade, new()
    {
        facade = null;
        if(!isBatching) return false;
        args = ResolveFacades(args);

        facade = new K()
        {
            Invoker = this
        };

        batchedFacades.Add(facade.Id, facade);
        object?[] batchedCall = [INVOKE_CALLBACK_FUNCTION, JSObject, methodName, facade, .. args];
        batchedCalls.Add(batchedCall);

        return true;
    }

    public bool IsBatching() => isBatching;

    public async Task EndBatch() 
    {
        if(Module == null)
            throw new InvalidOperationException("Module not initialized");
        isBatching = false;
        var results = await Module.InvokeAsync<Dictionary<string, object>>("invokeBatch", batchedCalls).ConfigureAwait(false);

        foreach(var entry in results) 
        {
            if(!Guid.TryParse(entry.Key, out var facadeId) || !batchedFacades.TryGetValue(facadeId, out var facade))
                continue;
            
            if(facade != null && entry.Value is JsonElement json)
                facade.SetValue(json);
        }

        batchedFacades.Clear();
        batchedCalls.Clear();
    }

    private object?[] ResolveFacades(object?[] args) => args.Select(x => x is Facade f && f.HasValue ? f.GetValue() : x).ToArray();

#endregion

#region Basic Operations
    public async ValueTask<T> GetPropertyAsync<T>(object jSObject, string propertyName) {
        if(Module == null)
            throw new InvalidOperationException("Module not initialized");
        return await Module.InvokeAsync<T>("getProperty", jSObject, propertyName);
    }

    [Obsolete("Use SetPropertyAsync instead")]
    public async void SetProperty<T>(object? jSObject, string propertyName, T value) => 
    await SetPropertyAsync(jSObject, propertyName, value);
    
    public async ValueTask SetPropertyAsync<T>(object? jSObject, string propertyName, T value) { 
        if(Module == null)
            throw new InvalidOperationException("Module not initialized");
        await Module.InvokeVoidAsync("setProperty", jSObject, propertyName, value);
    }

    public async ValueTask<T> InvokeAsync<T>(object? jSObject, string methodName, params object?[] args) {
        if(Module == null)
            throw new InvalidOperationException("Module not initialized");
        if(jSObject == null)
            return await Module.InvokeAsync<T>(methodName, args);
        return await Module.InvokeAsync<T>("invokeFunction", jSObject, methodName, args);
    }

    [Obsolete("Use InvokeVoidAsync instead")]
    public async void InvokeVoid(object? jSObject, string methodName, params object?[] args) => 
        await InvokeAsync<object>(jSObject, methodName, args);
    
    public async ValueTask InvokeVoidAsync(object? jSObject, string methodName, params object?[] args) => 
        await InvokeAsync<object>(jSObject, methodName, args);
#endregion

}
