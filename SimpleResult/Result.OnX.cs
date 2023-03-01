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
    
    public async Task<Result<TValue, TError>> OnValue<TData>(Func<TValue, TData, Task> func, TData data)
    {
        if (IsOk)
            await func(_value!, data);

        return this;
    }
    
    public Task<Result<TValue, TError>> OnValue(Func<TValue, Task> func) => OnValue(static (value, func) => func(value), func);
    
    public async ValueTask<Result<TValue, TError>> OnValue<TData>(Func<TValue, TData, ValueTask> func, TData data)
    {
        if (IsOk)
            await func(_value!, data);

        return this;
    }
    
    public ValueTask<Result<TValue, TError>> OnValue(Func<TValue, ValueTask> func) => OnValue(static (value, func) => func(value), func);
    
    public Result<TValue, TError> OnError<TData>(Action<TError, TData> func, TData data)
    {
        if (IsError)
            func(_error!, data);

        return this;
    }

    public Result<TValue, TError> OnError(Action<TError> func) => OnError(static (value, func) => func(value), func);
    
    public async Task<Result<TValue, TError>> OnError<TData>(Func<TError, TData, Task> func, TData data)
    {
        if (IsError)
            await func(_error!, data);

        return this;
    }

    public Task<Result<TValue, TError>> OnError(Func<TError, Task> func) => OnError(static (value, func) => func(value), func);
    
    public async ValueTask<Result<TValue, TError>> OnError<TData>(Func<TError, TData, ValueTask> func, TData data)
    {
        if (IsError)
            await func(_error!, data);

        return this;
    }

    public ValueTask<Result<TValue, TError>> OnError(Func<TError, ValueTask> func) => OnError(static (value, func) => func(value), func);
}