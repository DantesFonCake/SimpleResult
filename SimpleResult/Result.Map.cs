namespace SimpleResult;

public partial struct Result<TValue, TError>
{
	public Result<TValue2, TError> Map<TValue2, TData>(Func<TValue, TData, TValue2> valueMapper, TData data) =>
		IsOk ? new Result<TValue2, TError>(valueMapper(_value!, data)) : new Result<TValue2, TError>(_error!);

	public Result<TValue2, TError> Map<TValue2>(Func<TValue, TValue2> valueMapper) =>
		Map((value, func) => func(value), valueMapper);
	
	public TValue2 MapOr<TValue2, TData>(Func<TValue, TData, TValue2> valueMapper, TData data, TValue2 defaultValue) =>
		IsOk ? valueMapper(_value!, data) : defaultValue;

	public TValue2 MapOr<TValue2>(Func<TValue, TValue2> valueMapper, TValue2 defaultValue) =>
		MapOr((value, func) => func(value), valueMapper, defaultValue);
	
	public Result<TValue, TError2> MapError<TError2, TData>(Func<TError, TData, TError2> errorMapper, TData data) =>
		IsOk ? new Result<TValue, TError2>(_value!) : new Result<TValue, TError2>(errorMapper(_error!, data));

	public Result<TValue, TError2> MapError<TError2>(Func<TError, TError2> errorMapper) =>
		MapError((error, func) => func(error), errorMapper);
}