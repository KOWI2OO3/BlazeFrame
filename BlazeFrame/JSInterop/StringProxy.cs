namespace BlazeFrame.JSInterop;

public class StringProxy : Proxy<string>
{
    public static StringProxy operator +(StringProxy a, string b) => Operation.Add.Compute(a, b, ResolveOperation);
    public static StringProxy operator +(string a, StringProxy b) => b + a;
    public static StringProxy operator +(StringProxy a, StringProxy b) => Operation.Add.Compute<string, StringProxy>(a, b, ResolveOperation);

    public BinaryNumberProxy<int> Length => Compute<int, StringProxy, BinaryNumberProxy<int>>("length", this, [], ResolveOperation);

    protected static string ResolveOperation(Operation operation, string a, string b) => operation switch
    {
        Operation.Add => a + b,
        _ => throw new InvalidOperationException("Invalid operation")
    };

    protected static object ResolveOperation(string operation, string a, object?[] args) => operation switch
    {
        "length" => a.Length,
        _ => throw new InvalidOperationException("Invalid operation")
    };

    public static V Compute<Q, K, V>(string operation, K a, object?[] args, Func<string, string, object?[], object> operationResolver) where K : Proxy<string>, new() where V : Proxy<Q>, new()
    {
        if(a.HasValue || !(a.Invoker?.InvokeBatched<V>(null, "resolveProxyOperation", out var proxy, operation.ToLower() ?? "null", a, args) ?? false))
            return new V() { Value = (Q)operationResolver(operation, a.Value ?? string.Empty, args), HasValue = true  };
        return proxy;
    }

}
