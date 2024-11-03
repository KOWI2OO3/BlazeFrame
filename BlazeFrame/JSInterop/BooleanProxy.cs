namespace BlazeFrame.JSInterop;

public class BooleanProxy : Proxy<bool>
{
    public BooleanProxy()
    {
        Value = default;
    }

    public static BooleanProxy operator !(BooleanProxy a) => ProxyOperationHelper.Compute(Operation.Invert, a, default(bool)!, ResolveOperation);

    public static BooleanProxy operator ==(BooleanProxy a, bool b) => ProxyOperationHelper.Compute(Operation.Equal, a, b, ResolveOperation);
    public static BooleanProxy operator ==(bool a, BooleanProxy b) => ProxyOperationHelper.Compute(Operation.Equal, a, b, ResolveOperation);
    public static BooleanProxy operator ==(BooleanProxy a, BooleanProxy b) => ProxyOperationHelper.Compute<bool, BooleanProxy>(Operation.Equal, a, b, ResolveOperation);

    public static BooleanProxy operator !=(BooleanProxy a, bool b) => !(a == b);
    public static BooleanProxy operator !=(bool a, BooleanProxy b) => !(a == b);
    public static BooleanProxy operator !=(BooleanProxy a, BooleanProxy b) => !(a == b);

    public static BooleanProxy operator &(BooleanProxy a, bool b) => ProxyOperationHelper.Compute(Operation.And, a, b, ResolveOperation);
    public static BooleanProxy operator &(bool a, BooleanProxy b) => ProxyOperationHelper.Compute(Operation.And, a, b, ResolveOperation);
    public static BooleanProxy operator &(BooleanProxy a, BooleanProxy b) => ProxyOperationHelper.Compute<bool, BooleanProxy>(Operation.And, a, b, ResolveOperation);

    public static BooleanProxy operator |(BooleanProxy a, bool b) => ProxyOperationHelper.Compute(Operation.Or, a, b, ResolveOperation);
    public static BooleanProxy operator |(bool a, BooleanProxy b) => ProxyOperationHelper.Compute(Operation.Or, a, b, ResolveOperation);
    public static BooleanProxy operator |(BooleanProxy a, BooleanProxy b) => ProxyOperationHelper.Compute<bool, BooleanProxy>(Operation.Or, a, b, ResolveOperation);

    private static bool ResolveOperation(Operation operation, bool a, bool b) => operation switch
    {
        Operation.Invert => !a,
        Operation.Equal => a == b,
        _ => throw new InvalidOperationException("Invalid operation")
    };

    public override bool Equals(object? obj) => HasValue ? Value.Equals(obj) : base.Equals(obj);
    public override int GetHashCode() => HasValue ? Value.GetHashCode() : Id.GetHashCode();
}
