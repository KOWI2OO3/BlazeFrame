using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace BlazeFrame.JSInterop;

public class NumberProxy<T> : Proxy<T> where T : struct, INumber<T>
{
    public NumberProxy()
    {
        Value = default;
    }

    public static explicit operator T(NumberProxy<T> proxy) => proxy.Value!;
    
    public static NumberProxy<T> operator -(NumberProxy<T> a) => Operation.Negate.Compute(a, default(T)!, ResolveOperation);

    public static NumberProxy<T> operator +(NumberProxy<T> a, T b) => Operation.Add.Compute(a, b, ResolveOperation);
    public static NumberProxy<T> operator +(NumberProxy<T> a, NumberProxy<T> b) => Operation.Add.Compute<T, NumberProxy<T>>(a, b, ResolveOperation);
    public static NumberProxy<T> operator +(T a, NumberProxy<T> b) => b + a;

    public static NumberProxy<T> operator -(NumberProxy<T> a, T b) => Operation.Subtract.Compute(a, b, ResolveOperation);
    public static NumberProxy<T> operator -(T a, NumberProxy<T> b) => Operation.Subtract.Compute(a, b, ResolveOperation);
    public static NumberProxy<T> operator -(NumberProxy<T> a, NumberProxy<T> b) => Operation.Subtract.Compute<T, NumberProxy<T>>(a, b, ResolveOperation);

    public static NumberProxy<T> operator *(NumberProxy<T> a, T b) => Operation.Multiply.Compute(a, b, ResolveOperation);
    public static NumberProxy<T> operator *(NumberProxy<T> a, NumberProxy<T> b) => Operation.Multiply.Compute<T, NumberProxy<T>>(a, b, ResolveOperation);
    public static NumberProxy<T> operator *(T a, NumberProxy<T> b) => b * a;

    public static NumberProxy<T> operator /(NumberProxy<T> a, T b) => Operation.Divide.Compute(a, b, ResolveOperation);
    public static NumberProxy<T> operator /(T a, NumberProxy<T> b) => Operation.Divide.Compute(a, b, ResolveOperation);
    public static NumberProxy<T> operator /(NumberProxy<T> a, NumberProxy<T> b) => Operation.Divide.Compute<T, NumberProxy<T>>(a, b, ResolveOperation);

    public static NumberProxy<T> operator %(NumberProxy<T> a, T b) => Operation.Modulo.Compute(a, b, ResolveOperation);
    public static NumberProxy<T> operator %(T a, NumberProxy<T> b) => Operation.Modulo.Compute(a, b, ResolveOperation);
    public static NumberProxy<T> operator %(NumberProxy<T> a, NumberProxy<T> b) => Operation.Modulo.Compute<T, NumberProxy<T>>(a, b, ResolveOperation);

    public static BooleanProxy operator ==(NumberProxy<T> a, T b) => Operation.Equal.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator ==(T a, NumberProxy<T> b) => Operation.Equal.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator ==(NumberProxy<T> a, NumberProxy<T> b) =>Operation.Equal.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);

    public static BooleanProxy operator !=(NumberProxy<T> a, T b) => Operation.NotEqual.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator !=(T a, NumberProxy<T> b) => Operation.NotEqual.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator !=(NumberProxy<T> a, NumberProxy<T> b) => Operation.NotEqual.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);

    public static BooleanProxy operator <(NumberProxy<T> a, T b) => Operation.Less.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator <(T a, NumberProxy<T> b) => Operation.Less.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator <(NumberProxy<T> a, NumberProxy<T> b) => Operation.Less.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);

    public static BooleanProxy operator >(NumberProxy<T> a, T b) => Operation.Greater.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator >(T a, NumberProxy<T> b) => Operation.Greater.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator >(NumberProxy<T> a, NumberProxy<T> b) => Operation.Greater.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);

    public static BooleanProxy operator <=(NumberProxy<T> a, T b) => Operation.LEqual.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator <=(T a, NumberProxy<T> b) => Operation.LEqual.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator <=(NumberProxy<T> a, NumberProxy<T> b) => Operation.LEqual.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);

    public static BooleanProxy operator >=(NumberProxy<T> a, T b) => Operation.GEqual.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator >=(T a, NumberProxy<T> b) => Operation.GEqual.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator >=(NumberProxy<T> a, NumberProxy<T> b) => Operation.GEqual.Compute<T, bool, NumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);

    protected static T ResolveOperation(Operation operation, T a, T b) => operation switch
    {
        Operation.Add => a + b,
        Operation.Subtract => a - b,
        Operation.Multiply => a * b,
        Operation.Divide => a / b,
        Operation.Modulo => a % b,
        Operation.Negate => -a,
        _ => throw new InvalidOperationException("Invalid operation")
    };

    private static bool ResolveComparisonOperation(Operation operation, T a, T b) => operation switch
    {
        Operation.Equal => a == b,
        Operation.NotEqual => a != b,
        Operation.Greater => a > b,
        Operation.Less => a < b,
        Operation.GEqual => a >= b,
        Operation.LEqual => a <= b,
        _ => throw new InvalidOperationException("Invalid operation")
    };

    public override bool Equals(object? obj) => HasValue ? Value.Equals(obj) : base.Equals(obj);
    public override int GetHashCode() => HasValue ? Value.GetHashCode() : Id.GetHashCode();
}
