using System.Numerics;

namespace BlazeFrame.JSInterop;

public class BinaryNumberProxy<T> : Proxy<T> where T : struct, IBinaryNumber<T>, IBinaryInteger<T>, INumber<T>
{
    public BinaryNumberProxy()
    {
        Value = default;
    }

    public static implicit operator NumberProxy<T>(BinaryNumberProxy<T> proxy) => new() { Value = proxy.Value!, HasValue = proxy.HasValue, Id = proxy.Id };
    public static implicit operator BinaryNumberProxy<T>(NumberProxy<T> proxy) => new() { Value = proxy.Value!, HasValue = proxy.HasValue, Id = proxy.Id };

    public static BinaryNumberProxy<T> operator -(BinaryNumberProxy<T> a) => Operation.Negate.Compute(a, default(T)!, ResolveOperation);

    public static BinaryNumberProxy<T> operator +(BinaryNumberProxy<T> a, T b) => Operation.Add.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator +(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.Add.Compute<T, BinaryNumberProxy<T>>(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator +(T a, BinaryNumberProxy<T> b) => b + a;

    public static BinaryNumberProxy<T> operator -(BinaryNumberProxy<T> a, T b) => Operation.Subtract.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator -(T a, BinaryNumberProxy<T> b) => Operation.Subtract.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator -(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.Subtract.Compute<T, BinaryNumberProxy<T>>(a, b, ResolveOperation);

    public static BinaryNumberProxy<T> operator *(BinaryNumberProxy<T> a, T b) => Operation.Multiply.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator *(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.Multiply.Compute<T, BinaryNumberProxy<T>>(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator *(T a, BinaryNumberProxy<T> b) => b * a;

    public static BinaryNumberProxy<T> operator /(BinaryNumberProxy<T> a, T b) => Operation.Divide.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator /(T a, BinaryNumberProxy<T> b) => Operation.Divide.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator /(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.Divide.Compute<T, BinaryNumberProxy<T>>(a, b, ResolveOperation);

    public static BinaryNumberProxy<T> operator %(BinaryNumberProxy<T> a, T b) => Operation.Modulo.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator %(T a, BinaryNumberProxy<T> b) => Operation.Modulo.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator %(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.Modulo.Compute<T, BinaryNumberProxy<T>>(a, b, ResolveOperation);

    public static BinaryNumberProxy<T> operator <<(BinaryNumberProxy<T> a, T b) => Operation.LShift.Compute(a, b, ResolveBinaryOperation);
    public static BinaryNumberProxy<T> operator >>(BinaryNumberProxy<T> a, T b) => Operation.RShift.Compute(a, b, ResolveBinaryOperation);

    public static BinaryNumberProxy<T> operator &(BinaryNumberProxy<T> a, T b) => Operation.And.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator &(T a, BinaryNumberProxy<T> b) => Operation.And.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator &(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.And.Compute<T, BinaryNumberProxy<T>>(a, b, ResolveOperation);

    public static BinaryNumberProxy<T> operator |(BinaryNumberProxy<T> a, T b) => Operation.Or.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator |(T a, BinaryNumberProxy<T> b) => Operation.Or.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator |(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.Or.Compute<T, BinaryNumberProxy<T>>(a, b, ResolveOperation);

    public static BinaryNumberProxy<T> operator ^(BinaryNumberProxy<T> a, T b) => Operation.Xor.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator ^(T a, BinaryNumberProxy<T> b) => Operation.Xor.Compute(a, b, ResolveOperation);
    public static BinaryNumberProxy<T> operator ^(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.Xor.Compute<T, BinaryNumberProxy<T>>(a, b, ResolveOperation);
    
    public static BooleanProxy operator ==(BinaryNumberProxy<T> a, T b) => Operation.Equal.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator ==(T a, BinaryNumberProxy<T> b) => Operation.Equal.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator ==(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.Equal.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);

    public static BooleanProxy operator !=(BinaryNumberProxy<T> a, T b) => Operation.NotEqual.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator !=(T a, BinaryNumberProxy<T> b) => Operation.NotEqual.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator !=(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.NotEqual.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);

    public static BooleanProxy operator <(BinaryNumberProxy<T> a, T b) => Operation.Less.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator <(T a, BinaryNumberProxy<T> b) => Operation.Less.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator <(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.Less.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);

    public static BooleanProxy operator >(BinaryNumberProxy<T> a, T b) => Operation.Greater.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator >(T a, BinaryNumberProxy<T> b) => Operation.Greater.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator >(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.Greater.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);

    public static BooleanProxy operator <=(BinaryNumberProxy<T> a, T b) => Operation.LEqual.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator <=(T a, BinaryNumberProxy<T> b) => Operation.LEqual.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator <=(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.LEqual.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);

    public static BooleanProxy operator >=(BinaryNumberProxy<T> a, T b) => Operation.GEqual.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator >=(T a, BinaryNumberProxy<T> b) => Operation.GEqual.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    public static BooleanProxy operator >=(BinaryNumberProxy<T> a, BinaryNumberProxy<T> b) => Operation.GEqual.Compute<T, bool, BinaryNumberProxy<T>, BooleanProxy>(a, b, ResolveComparisonOperation);
    
    private static T ResolveBinaryOperation(Operation operation, T a, T b) => operation switch
    {
        Operation.And => a & b,
        Operation.Or => a | b,
        Operation.Xor => a ^ b,
        
        Operation.LShift => a << (int)(object)b,
        Operation.RShift => a >> (int)(object)b,
        _ => throw new InvalidOperationException("Invalid operation")
    };

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
