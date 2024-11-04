using System.Numerics;
using System.Text.Json;

namespace BlazeFrame.JSInterop;

public enum Operation
{
    Add,
    Subtract,
    Multiply,
    Divide,
    Modulo,
    Negate,
    Invert,
    Equal,
    NotEqual,
    Greater,
    Less,
    GEqual,
    LEqual,

    And,
    Or,
    Xor,

    LShift,
    RShift,
}

public static class ProxyOperationHelper
{
    public static K Compute<T, K>(this Operation operation, K a, T b, Func<Operation, T, T, T> operationResolver) where K : Proxy<T>, new() =>
        Compute<T, T, K, K>(operation, a, b, operationResolver);

    public static K Compute<T, K>(this Operation operation, T a, K b, Func<Operation, T, T, T> operationResolver) where K : Proxy<T>, new() =>
        Compute<T, T, K, K>(operation, a, b, operationResolver);

    public static K Compute<T, K>(this Operation operation, K a, K b, Func<Operation, T, T, T> operationResolver) where K : Proxy<T>, new() =>
        Compute<T, T, K, K>(operation, a, b, operationResolver);

    public static V Compute<T, Q, K, V>(this Operation operation, K a, T b, Func<Operation, T, T, Q> operationResolver) where K : Proxy<T>, new() where V : Proxy<Q>, new()
    {
        if(a.HasValue || !(a.Invoker?.InvokeBatched<V>(null, "compute", out var proxy, Enum.GetName(operation)?.ToLower() ?? "null", a, b) ?? false))
            return new V() { Value = operationResolver(operation, a.Value!, b), HasValue = true  };
        return proxy;
    }

    public static V Compute<T, Q, K, V>(this Operation operation, T a, K b, Func<Operation, T, T, Q> operationResolver) where K : Proxy<T>, new() where V : Proxy<Q>, new()
    {
        if(b.HasValue || !(b.Invoker?.InvokeBatched<V>(null, "compute", out var proxy, Enum.GetName(operation)?.ToLower() ?? "null", a, b) ?? false))
            return new V() { Value = operationResolver(operation, a, b.Value!), HasValue = true  };
        return proxy;
    }

    public static V Compute<T, Q, K, V>(this Operation operation, K a, K b, Func<Operation, T, T, Q> operationResolver) where K : Proxy<T>, new() where V : Proxy<Q>, new()
    {
        if((a.HasValue && b.HasValue) || !(a.Invoker?.InvokeBatched<V>(null, "compute", out var proxy, Enum.GetName(operation)?.ToLower() ?? "null", a, b) ?? false))
            return new V() { Value = operationResolver(operation, a.Value!, b.Value!), HasValue = true  };
        return proxy;
    }

    private static readonly Dictionary<Type, Type> primitiveProxyMap = new()
    {
        { typeof(int), typeof(BinaryNumberProxy<int>) },
        { typeof(uint), typeof(BinaryNumberProxy<uint>) },
        { typeof(long), typeof(BinaryNumberProxy<long>) },
        { typeof(ulong), typeof(BinaryNumberProxy<ulong>) },
        { typeof(short), typeof(BinaryNumberProxy<short>) },
        { typeof(ushort), typeof(BinaryNumberProxy<ushort>) },
        { typeof(byte), typeof(BinaryNumberProxy<byte>) },
        { typeof(sbyte), typeof(BinaryNumberProxy<sbyte>) },
        { typeof(float), typeof(NumberProxy<float>) },
        { typeof(double), typeof(NumberProxy<double>) },
        { typeof(decimal), typeof(NumberProxy<decimal>) },
        { typeof(BigInteger), typeof(BinaryNumberProxy<BigInteger>) },
        { typeof(string), typeof(StringProxy) },
        { typeof(bool), typeof(BooleanProxy) },
        // { typeof(DateTime), typeof(DateTimeProxy) },
        // { typeof(TimeSpan), typeof(TimeSpanProxy) },
        // { typeof(Guid), typeof(GuidProxy) },
    };

    public static Proxy<T> CreateFromJson<T>(JsonElement value) 
    {
        Proxy<T>? proxy = null;
        if(typeof(T).IsAssignableTo(typeof(Proxy)))
            proxy = (Proxy<T>?)Activator.CreateInstance(typeof(T));
        else if(primitiveProxyMap.TryGetValue(typeof(T), out var proxyType))
            proxy = (Proxy<T>?)Activator.CreateInstance(proxyType);

        proxy ??= new Proxy<T>();
        proxy.SetValue(value);
        return proxy;
    }

    public static T CreateFromJsonProxy<T>(JsonElement value) where T : Proxy, new()
    {
        if(typeof(T) == typeof(Proxy<>))
        {
            var generics = typeof(T).GetGenericArguments();
            if(generics.Length > 0) 
            {
                var result =  (T)typeof(ProxyOperationHelper).GetMethod(nameof(CreateFromJson))!.MakeGenericMethod(generics[0]).Invoke(null, [value])!;
                if(result is not null)
                    return result;
            }   
        }
        
        var proxy = new T();
        proxy.SetValue(value);
        return proxy;
    }

    public static T? CreatePotentialProxy<T>(JsonElement json) 
    {
        var proxy = CreateFromJson<T>(json);
        return typeof(T).IsAssignableTo(typeof(Proxy)) ? (T)(object)proxy : proxy.Value;
    }
}
