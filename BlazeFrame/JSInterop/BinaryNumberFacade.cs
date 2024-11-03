using System.Numerics;

namespace BlazeFrame.JSInterop;

public class BinaryNumberFacade<T> : Facade<T> where T : struct, IBinaryNumber<T>, IBinaryInteger<T>, INumber<T>
{
    public BinaryNumberFacade()
    {
        Value = default;
    }

    public static implicit operator NumberFacade<T>(BinaryNumberFacade<T> facade) => new() { Value = facade.Value!, HasValue = facade.HasValue, Id = facade.Id };
    public static implicit operator BinaryNumberFacade<T>(NumberFacade<T> facade) => new() { Value = facade.Value!, HasValue = facade.HasValue, Id = facade.Id };

    public static BinaryNumberFacade<T> operator -(BinaryNumberFacade<T> a) => Operation.Negate.Compute(a, default(T)!, ResolveOperation);

    public static BinaryNumberFacade<T> operator +(BinaryNumberFacade<T> a, T b) => Operation.Add.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator +(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.Add.Compute<T, BinaryNumberFacade<T>>(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator +(T a, BinaryNumberFacade<T> b) => b + a;

    public static BinaryNumberFacade<T> operator -(BinaryNumberFacade<T> a, T b) => Operation.Subtract.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator -(T a, BinaryNumberFacade<T> b) => Operation.Subtract.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator -(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.Subtract.Compute<T, BinaryNumberFacade<T>>(a, b, ResolveOperation);

    public static BinaryNumberFacade<T> operator *(BinaryNumberFacade<T> a, T b) => Operation.Multiply.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator *(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.Multiply.Compute<T, BinaryNumberFacade<T>>(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator *(T a, BinaryNumberFacade<T> b) => b * a;

    public static BinaryNumberFacade<T> operator /(BinaryNumberFacade<T> a, T b) => Operation.Divide.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator /(T a, BinaryNumberFacade<T> b) => Operation.Divide.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator /(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.Divide.Compute<T, BinaryNumberFacade<T>>(a, b, ResolveOperation);

    public static BinaryNumberFacade<T> operator %(BinaryNumberFacade<T> a, T b) => Operation.Modulo.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator %(T a, BinaryNumberFacade<T> b) => Operation.Modulo.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator %(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.Modulo.Compute<T, BinaryNumberFacade<T>>(a, b, ResolveOperation);

    public static BinaryNumberFacade<T> operator <<(BinaryNumberFacade<T> a, T b) => Operation.LShift.Compute(a, b, ResolveBinaryOperation);
    public static BinaryNumberFacade<T> operator >>(BinaryNumberFacade<T> a, T b) => Operation.RShift.Compute(a, b, ResolveBinaryOperation);

    public static BinaryNumberFacade<T> operator &(BinaryNumberFacade<T> a, T b) => Operation.And.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator &(T a, BinaryNumberFacade<T> b) => Operation.And.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator &(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.And.Compute<T, BinaryNumberFacade<T>>(a, b, ResolveOperation);

    public static BinaryNumberFacade<T> operator |(BinaryNumberFacade<T> a, T b) => Operation.Or.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator |(T a, BinaryNumberFacade<T> b) => Operation.Or.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator |(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.Or.Compute<T, BinaryNumberFacade<T>>(a, b, ResolveOperation);

    public static BinaryNumberFacade<T> operator ^(BinaryNumberFacade<T> a, T b) => Operation.Xor.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator ^(T a, BinaryNumberFacade<T> b) => Operation.Xor.Compute(a, b, ResolveOperation);
    public static BinaryNumberFacade<T> operator ^(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.Xor.Compute<T, BinaryNumberFacade<T>>(a, b, ResolveOperation);
    
    public static BooleanFacade operator ==(BinaryNumberFacade<T> a, T b) => Operation.Equal.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator ==(T a, BinaryNumberFacade<T> b) => Operation.Equal.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator ==(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.Equal.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);

    public static BooleanFacade operator !=(BinaryNumberFacade<T> a, T b) => Operation.NotEqual.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator !=(T a, BinaryNumberFacade<T> b) => Operation.NotEqual.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator !=(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.NotEqual.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);

    public static BooleanFacade operator <(BinaryNumberFacade<T> a, T b) => Operation.Less.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator <(T a, BinaryNumberFacade<T> b) => Operation.Less.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator <(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.Less.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);

    public static BooleanFacade operator >(BinaryNumberFacade<T> a, T b) => Operation.Greater.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator >(T a, BinaryNumberFacade<T> b) => Operation.Greater.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator >(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.Greater.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);

    public static BooleanFacade operator <=(BinaryNumberFacade<T> a, T b) => Operation.LEqual.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator <=(T a, BinaryNumberFacade<T> b) => Operation.LEqual.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator <=(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.LEqual.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);

    public static BooleanFacade operator >=(BinaryNumberFacade<T> a, T b) => Operation.GEqual.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator >=(T a, BinaryNumberFacade<T> b) => Operation.GEqual.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    public static BooleanFacade operator >=(BinaryNumberFacade<T> a, BinaryNumberFacade<T> b) => Operation.GEqual.Compute<T, bool, BinaryNumberFacade<T>, BooleanFacade>(a, b, ResolveComparisonOperation);
    
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
