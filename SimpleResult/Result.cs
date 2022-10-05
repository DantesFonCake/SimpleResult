namespace SimpleResult;

public readonly record struct Unit;

public static class Result
{
    public static Result<Unit, Unit> Ok() => Result<Unit, Unit>.Ok(new Unit());
    public static Result<Unit, Unit> Error() => Result<Unit, Unit>.Err(new Unit());

    public static Result<TValue, Unit> Ok<TValue>(TValue value) => Result<TValue, Unit>.Ok(value);
    public static Result<TValue, Unit> Error<TValue>() => Result<TValue, Unit>.Err(new Unit());

    public static Result<Unit, TError> Ok<TError>() => Result<Unit, TError>.Ok(new Unit());
    public static Result<Unit, TError> Error<TError>(TError error) => Result<Unit, TError>.Err(error);

    public static Result<TValue, TError> Ok<TValue, TError>(TValue value) => Result<TValue, TError>.Ok(value);
    public static Result<TValue, TError> Error<TValue, TError>(TError error) => Result<TValue, TError>.Err(error);

    public static Result<TValue, TError> Try<TValue, TError, TData, TErrorData>(Func<TData, TValue> func, TData data,
        Func<Exception, TErrorData, TError> exceptionMapper, TErrorData errorData)
    {
        try
        {
            return new Result<TValue, TError>(func(data));
        }
        catch (Exception e)
        {
            return new Result<TValue, TError>(exceptionMapper(e, errorData));
        }
    }

    public static Result<TValue, TError> Try<TValue, TError, TData>(Func<TData, TValue> func, TData data,
        Func<Exception, TError> exceptionMapper) 
        => Try(func, data, (e, mapper) => mapper(e), exceptionMapper);

    public static Result<TValue, TError> Try<TValue, TError, TErrorData>(Func<TValue> func,
        Func<Exception, TErrorData, TError> exceptionMapper, TErrorData errorData)
        => Try(valueSource => valueSource(), func, exceptionMapper, errorData);

    public static Result<TValue, Exception> Try<TValue, TData>(Func<TData, TValue> func, TData data)
        => Try(func, data, e => e);

    public static Result<TValue, TError> Try<TValue, TError>(Func<TValue> func,
        Func<Exception, TError> exceptionMapper) 
        => Try(valueSource => valueSource(), func, exceptionMapper);

    public static Result<TValue, Exception> Try<TValue>(Func<TValue> func) 
        => Try(func, e => e);
}

public readonly partial struct Result<TValue, TError>
{
    private readonly TError? _error;
    private readonly TValue? _value;

    public Result(TValue value)
    {
        _value = value;
        IsOk = true;
        _error = default;
    }

    public Result(TError error)
    {
        _error = error;
        IsOk = false;
        _value = default;
    }

    public static Result<TValue, TError> Ok(TValue value) => new(value);

    public static Result<TValue, TError> Err(TError error) => new(error);

    public bool IsOk { get; }
    public bool IsError => !IsOk;

    public TError Error => GetErrorOrThrow();

    public TError GetErrorOrThrow() =>
        IsError ? _error! : throw new InvalidOperationException($"Result is in status ok. Error is not set.");

    public TValue Value => GetValueOrThrow();

    public TValue? GetValueOr(TValue? defaultValue) =>
        IsOk ? _value! : defaultValue;

    public TValue GetValueOrThrow() =>
        IsOk ? _value! : throw new InvalidOperationException($"Result is in status error. Value is not set.");

    public TValue? GetValueOrDefault() => GetValueOr(default);

    internal TValue GetValueUnchecked() => _value!;

    internal TError GetErrorUnchecked() => _error!;
}