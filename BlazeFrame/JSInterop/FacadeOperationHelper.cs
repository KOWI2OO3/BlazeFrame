using System.Numerics;

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

public static class FacadeOperationHelper
{
    public static K Compute<T, K>(this Operation operation, K a, T b, Func<Operation, T, T, T> operationResolver) where K : Facade<T>, new() =>
        Compute<T, T, K, K>(operation, a, b, operationResolver);

    public static K Compute<T, K>(this Operation operation, T a, K b, Func<Operation, T, T, T> operationResolver) where K : Facade<T>, new() =>
        Compute<T, T, K, K>(operation, a, b, operationResolver);

    public static K Compute<T, K>(this Operation operation, K a, K b, Func<Operation, T, T, T> operationResolver) where K : Facade<T>, new() =>
        Compute<T, T, K, K>(operation, a, b, operationResolver);

    public static V Compute<T, Q, K, V>(this Operation operation, K a, T b, Func<Operation, T, T, Q> operationResolver) where K : Facade<T>, new() where V : Facade<Q>, new()
    {
        if(a.HasValue || !(a.Invoker?.InvokeBatched<V>(null, "compute", out var facade, Enum.GetName(operation)?.ToLower() ?? "null", a, b) ?? false))
            return new V() { Value = operationResolver(operation, a.Value!, b), HasValue = true  };
        return facade;
    }

    public static V Compute<T, Q, K, V>(this Operation operation, T a, K b, Func<Operation, T, T, Q> operationResolver) where K : Facade<T>, new() where V : Facade<Q>, new()
    {
        if(b.HasValue || !(b.Invoker?.InvokeBatched<V>(null, "compute", out var facade, Enum.GetName(operation)?.ToLower() ?? "null", a, b) ?? false))
            return new V() { Value = operationResolver(operation, a, b.Value!), HasValue = true  };
        return facade;
    }

    public static V Compute<T, Q, K, V>(this Operation operation, K a, K b, Func<Operation, T, T, Q> operationResolver) where K : Facade<T>, new() where V : Facade<Q>, new()
    {
        if((a.HasValue && b.HasValue) || !(a.Invoker?.InvokeBatched<V>(null, "compute", out var facade, Enum.GetName(operation)?.ToLower() ?? "null", a, b) ?? false))
            return new V() { Value = operationResolver(operation, a.Value!, b.Value!), HasValue = true  };
        return facade;
    }
}
