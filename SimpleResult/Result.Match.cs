namespace SimpleResult;

public readonly partial struct Result<TValue, TError>
{
    public T Match<T, TValueData, TErrorData>(
        Func<TValue, TValueData, T> valueArm,
        TValueData valueData,
        Func<TError, TErrorData, T> errorArm,
        TErrorData errorData
    )
    {
        return IsOk ? valueArm(_value!, valueData) : errorArm(_error!, errorData);
    }

    public T Match<T, TErrorData>(
        Func<TValue, T> valueArm,
        Func<TError, TErrorData, T> errorArm,
        TErrorData errorData
    )
    {
        return Match(static (value, func) => func(value), valueArm, errorArm, errorData);
    }

    public T Match<T, TValueData>(
        Func<TValue, TValueData, T> valueArm,
        TValueData valueData,
        Func<TError, T> errorArm
    )
    {
        return Match(valueArm, valueData, static (error, func) => func(error), errorArm);
    }

    public T Match<T>(
        Func<TValue, T> valueArm,
        Func<TError, T> errorArm
    )
    {
        return Match(static (value, func) => func(value), valueArm, static (error, func) => func(error), errorArm);
    }
}