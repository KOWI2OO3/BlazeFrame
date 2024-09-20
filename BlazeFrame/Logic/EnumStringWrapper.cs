namespace BlazeFrame.Logic;

public sealed class EnumStringWrapper<TEnum>(TEnum @enum = default) where TEnum : struct, Enum
{
    private TEnum EnumValue { get; set; } = @enum;

    public static implicit operator EnumStringWrapper<TEnum>(TEnum value) => new(value);

    public static implicit operator TEnum(EnumStringWrapper<TEnum> value) => value.EnumValue;

    public static implicit operator string(EnumStringWrapper<TEnum> value) => Enum.GetName(value.EnumValue)?.ToLower() ?? default(TEnum).ToString().ToLower();

    public static implicit operator EnumStringWrapper<TEnum>(string value) => new(EnumHelper.GetByName<TEnum>(value));

}
