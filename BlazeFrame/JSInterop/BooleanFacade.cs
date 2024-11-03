namespace BlazeFrame.JSInterop;

public class BooleanFacade : Facade<bool>
{
    public BooleanFacade()
    {
        Value = default;
    }

    public static BooleanFacade operator !(BooleanFacade a) => FacadeOperationHelper.Compute(Operation.Invert, a, default(bool)!, ResolveOperation);

    public static BooleanFacade operator ==(BooleanFacade a, bool b) => FacadeOperationHelper.Compute(Operation.Equal, a, b, ResolveOperation);
    public static BooleanFacade operator ==(bool a, BooleanFacade b) => FacadeOperationHelper.Compute(Operation.Equal, a, b, ResolveOperation);
    public static BooleanFacade operator ==(BooleanFacade a, BooleanFacade b) => FacadeOperationHelper.Compute<bool, BooleanFacade>(Operation.Equal, a, b, ResolveOperation);

    public static BooleanFacade operator !=(BooleanFacade a, bool b) => !(a == b);
    public static BooleanFacade operator !=(bool a, BooleanFacade b) => !(a == b);
    public static BooleanFacade operator !=(BooleanFacade a, BooleanFacade b) => !(a == b);

    public static BooleanFacade operator &(BooleanFacade a, bool b) => FacadeOperationHelper.Compute(Operation.And, a, b, ResolveOperation);
    public static BooleanFacade operator &(bool a, BooleanFacade b) => FacadeOperationHelper.Compute(Operation.And, a, b, ResolveOperation);
    public static BooleanFacade operator &(BooleanFacade a, BooleanFacade b) => FacadeOperationHelper.Compute<bool, BooleanFacade>(Operation.And, a, b, ResolveOperation);

    public static BooleanFacade operator |(BooleanFacade a, bool b) => FacadeOperationHelper.Compute(Operation.Or, a, b, ResolveOperation);
    public static BooleanFacade operator |(bool a, BooleanFacade b) => FacadeOperationHelper.Compute(Operation.Or, a, b, ResolveOperation);
    public static BooleanFacade operator |(BooleanFacade a, BooleanFacade b) => FacadeOperationHelper.Compute<bool, BooleanFacade>(Operation.Or, a, b, ResolveOperation);

    private static bool ResolveOperation(Operation operation, bool a, bool b) => operation switch
    {
        Operation.Invert => !a,
        Operation.Equal => a == b,
        _ => throw new InvalidOperationException("Invalid operation")
    };

    public override bool Equals(object? obj) => HasValue ? Value.Equals(obj) : base.Equals(obj);
    public override int GetHashCode() => HasValue ? Value.GetHashCode() : Id.GetHashCode();
}
