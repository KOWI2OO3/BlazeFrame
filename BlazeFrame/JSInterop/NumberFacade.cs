using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace BlazeFrame.JSInterop;

public class NumberFacade<T> : Facade<T> where T : struct, INumber<T>
{
    public NumberFacade()
    {
        Value = default;
    }

    public static explicit operator T(NumberFacade<T> facade) => facade.Value!;
    
    public static NumberFacade<T> operator -(NumberFacade<T> a) => Operation.Negate.Compute(a, default(T)!, ResolveOperation);

    public static NumberFacade<T> operator +(NumberFacade<T> a, T b) => Operation.Add.Compute(a, b, ResolveOperation);
    public static NumberFacade<T> operator +(NumberFacade<T> a, NumberFacade<T> b) => Operation.Add.Compute<T, NumberFacade<T>>(a, b, ResolveOperation);
    public static NumberFacade<T> operator +(T a, NumberFacade<T> b) => b + a;

    public static NumberFacade<T> operator -(NumberFacade<T> a, T b) => Operation.Subtract.Compute(a, b, ResolveOperation);
    public static NumberFacade<T> operator -(T a, NumberFacade<T> b) => Operation.Subtract.Compute(a, b, ResolveOperation);
    public static NumberFacade<T> operator -(NumberFacade<T> a, NumberFacade<T> b) => Operation.Subtract.Compute<T, NumberFacade<T>>(a, b, ResolveOperation);

    public static NumberFacade<T> operator *(NumberFacade<T> a, T b) => Operation.Multiply.Compute(a, b, ResolveOperation);
    public static NumberFacade<T> operator *(NumberFacade<T> a, NumberFacade<T> b) => Operation.Multiply.Compute<T, NumberFacade<T>>(a, b, ResolveOperation);
    public static NumberFacade<T> operator *(T a, NumberFacade<T> b) => b * a;

    public static NumberFacade<T> operator /(NumberFacade<T> a, T b) => Operation.Divide.Compute(a, b, ResolveOperation);
    public static NumberFacade<T> operator /(T a, NumberFacade<T> b) => Operation.Divide.Compute(a, b, ResolveOperation);
    public static NumberFacade<T> operator /(NumberFacade<T> a, NumberFacade<T> b) => Operation.Divide.Compute<T, NumberFacade<T>>(a, b, ResolveOperation);

    public static NumberFacade<T> operator %(NumberFacade<T> a, T b) => Operation.Modulo.Compute(a, b, ResolveOperation);
    public static NumberFacade<T> operator %(T a, NumberFacade<T> b) => Operation.Modulo.Compute(a, b, ResolveOperation);
    public static NumberFacade<T> operator %(NumberFacade<T> a, NumberFacade<T> b) => Operation.Modulo.Compute<T, NumberFacade<T>>(a, b, ResolveOperation);

    public static BooleanFacade operator ==(NumberFacade<T> a, T b) => Operation.Equal.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator ==(T a, NumberFacade<T> b) => Operation.Equal.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator ==(NumberFacade<T> a, NumberFacade<T> b) =>Operation.Equal.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);

    public static BooleanFacade operator !=(NumberFacade<T> a, T b) => Operation.NotEqual.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator !=(T a, NumberFacade<T> b) => Operation.NotEqual.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator !=(NumberFacade<T> a, NumberFacade<T> b) => Operation.NotEqual.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);

    public static BooleanFacade operator <(NumberFacade<T> a, T b) => Operation.Less.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator <(T a, NumberFacade<T> b) => Operation.Less.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator <(NumberFacade<T> a, NumberFacade<T> b) => Operation.Less.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);

    public static BooleanFacade operator >(NumberFacade<T> a, T b) => Operation.Greater.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator >(T a, NumberFacade<T> b) => Operation.Greater.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator >(NumberFacade<T> a, NumberFacade<T> b) => Operation.Greater.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);

    public static BooleanFacade operator <=(NumberFacade<T> a, T b) => Operation.LEqual.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator <=(T a, NumberFacade<T> b) => Operation.LEqual.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator <=(NumberFacade<T> a, NumberFacade<T> b) => Operation.LEqual.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);

    public static BooleanFacade operator >=(NumberFacade<T> a, T b) => Operation.GEqual.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator >=(T a, NumberFacade<T> b) => Operation.GEqual.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator >=(NumberFacade<T> a, NumberFacade<T> b) => Operation.GEqual.Compute<T, bool, NumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);

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
