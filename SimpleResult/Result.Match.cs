namespace SimpleResult;

public readonly partial struct Result<TValue, TError>
{
    public T Match<T, TValueData, TErrorData>(
        Func<TValue, TValueData, T> valueArm,
        TValueData valueData,
        Func<TError, TErrorData, T> errorArm,
        TErrorData errorData
    ) => IsOk ? valueArm(_value!, valueData) : errorArm(_error!, errorData);

    public T Match<T, TErrorData>(
        Func<TValue, T> valueArm,
        Func<TError, TErrorData, T> errorArm,
        TErrorData errorData
    ) => Match(static (value, func) => func(value), valueArm, errorArm, errorData);

    public T Match<T, TValueData>(
        Func<TValue, TValueData, T> valueArm,
        TValueData valueData,
        Func<TError, T> errorArm
    ) => Match(valueArm, valueData, static (error, func) => func(error), errorArm);

    public T Match<T>(
        Func<TValue, T> valueArm,
        Func<TError, T> errorArm
    ) => Match(static (value, func) => func(value), valueArm, static (error, func) => func(error), errorArm);

    public async Task<T> Match<T, TValueData, TErrorData>(
        Func<TValue, TValueData, Task<T>> valueArm,
        TValueData valueData,
        Func<TError, TErrorData, Task<T>> errorArm,
        TErrorData errorData
    ) => IsOk ? await valueArm(_value!, valueData) : await errorArm(_error!, errorData);

    public Task<T> Match<T, TErrorData>(
        Func<TValue, Task<T>> valueArm,
        Func<TError, TErrorData, Task<T>> errorArm,
        TErrorData errorData
    ) => Match(static (value, func) => func(value), valueArm, errorArm, errorData);

    public Task<T> Match<T, TValueData>(
        Func<TValue, TValueData, Task<T>> valueArm,
        TValueData valueData,
        Func<TError, Task<T>> errorArm
    ) => Match(valueArm, valueData, static (error, func) => func(error), errorArm);

    public Task<T> Match<T>(
        Func<TValue, Task<T>> valueArm,
        Func<TError, Task<T>> errorArm
    ) => Match(static (value, func) => func(value), valueArm, static (error, func) => func(error), errorArm);
    
    public async ValueTask<T> Match<T, TValueData, TErrorData>(
        Func<TValue, TValueData, ValueTask<T>> valueArm,
        TValueData valueData,
        Func<TError, TErrorData, ValueTask<T>> errorArm,
        TErrorData errorData
    ) => IsOk ? await valueArm(_value!, valueData) : await errorArm(_error!, errorData);

    public ValueTask<T> Match<T, TErrorData>(
        Func<TValue, ValueTask<T>> valueArm,
        Func<TError, TErrorData, ValueTask<T>> errorArm,
        TErrorData errorData
    ) => Match(static (value, func) => func(value), valueArm, errorArm, errorData);

    public ValueTask<T> Match<T, TValueData>(
        Func<TValue, TValueData, ValueTask<T>> valueArm,
        TValueData valueData,
        Func<TError, ValueTask<T>> errorArm
    ) => Match(valueArm, valueData, static (error, func) => func(error), errorArm);

    public ValueTask<T> Match<T>(
        Func<TValue, ValueTask<T>> valueArm,
        Func<TError, ValueTask<T>> errorArm
    ) => Match(static (value, func) => func(value), valueArm, static (error, func) => func(error), errorArm);
}