namespace SimpleResult;

public partial struct Result<TValue, TError>
{
    public Result<TValue, TError> OnValue<TData>(Action<TValue, TData> func, TData data)
    {
        if (IsOk)
            func(_value!, data);

        return this;
    }

    public Result<TValue, TError> OnValue(Action<TValue> func) => OnValue(static (value, func) => func(value), func);
    
    public Result<TValue, TError> OnError<TData>(Action<TError, TData> func, TData data)
    {
        if (IsError)
            func(_error!, data);

        return this;
    }

    public Result<TValue, TError> OnError(Action<TError> func) => OnError(static (value, func) => func(value), func);
}