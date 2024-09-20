using Microsoft.JSInterop;

namespace BlazeFrame;

public class JSInvoker
{
    public static JSInvoker INSTANCE { get; internal set; } = new(null);

    public const string NAMESPACE = "BlazeFrame";

    public const string SET_PROPERTY = "setProperty";
    public const string GET_PROPERTY = "getProperty";
    public const string INVOKE_FUNCTION = "invokeFunction";
    
    public const string INVOKE_CALLBACK_FUNCTION = "invokeCallbackFunction";
    
    private IJSRuntime? JSRuntime { get; set; }

    internal IJSObjectReference? Module { get; private set; }

    private readonly List<object?[]> batchedCalls = [];

    private readonly Dictionary<Guid, (Type, Action<object>)> batchCallbacks = [];

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

    public bool InvokeBatched(object? JSObject, string methodName, params object[] args) 
    {
        if(!isBatching) return false;
        object?[] batchedCall = [INVOKE_FUNCTION, JSObject, methodName, .. args];
        batchedCalls.Add(batchedCall);

        return true;
    }

    public async Task EndBatch() 
    {
        if(Module == null)
            throw new InvalidOperationException("Module not initialized");
        isBatching = false;
        await Module.InvokeVoidAsync("invokeBatch", batchedCalls);

        batchedCalls.Clear();
    }

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
