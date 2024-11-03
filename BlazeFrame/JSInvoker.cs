using BlazeFrame.JSInterop;
using Microsoft.JSInterop;
using System.Text.Json;

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

    private readonly Dictionary<Guid, Proxy> batchedProxies = [];

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
        args = ResolveProxies(args);
        
        object?[] batchedCall = [INVOKE_FUNCTION, JSObject, methodName, .. args];
        batchedCalls.Add(batchedCall);

        return true;
    }

    public bool InvokeBatched<K>(object? JSObject, string methodName, out K proxy, params object?[] args) where K : Proxy, new()
    {
        proxy = null;
        if(!isBatching) return false;
        args = ResolveProxies(args);

        proxy = new K()
        {
            Invoker = this
        };

        batchedProxies.Add(proxy.Id, proxy);
        object?[] batchedCall = [INVOKE_CALLBACK_FUNCTION, JSObject, methodName, proxy, .. args];
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
            if(!Guid.TryParse(entry.Key, out var proxyId) || !batchedProxies.TryGetValue(proxyId, out var proxy))
                continue;
            
            if(proxy != null && entry.Value is JsonElement json)
                proxy.SetValue(json);
        }

        batchedProxies.Clear();
        batchedCalls.Clear();
    }

    private static object?[] ResolveProxies(object?[] args) => args.Select(x => x is Proxy proxy && proxy.HasValue ? proxy.GetValue() : x).ToArray();

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
