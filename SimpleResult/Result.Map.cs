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

	public TValue2 MapOrElse<TErrorData, TValue2, TValueData>(
		Func<TError, TErrorData, TValue2> errorMapper, TErrorData errorData,
		Func<TValue, TValueData, TValue2> valueMapper, TValueData valueData) =>
		IsOk ? valueMapper(_value!, valueData) : errorMapper(_error!, errorData);

	public TValue2 MapOrElse<TValue2, TValueData>(
		Func<TError, TValue2> errorMapper,
		Func<TValue, TValueData, TValue2> valueMapper, TValueData valueData) =>
		MapOrElse(static (error, em) => em(error), errorMapper, valueMapper, valueData);

	public TValue2 MapOrElse<TErrorData, TValue2>(
		Func<TError, TErrorData, TValue2> errorMapper, TErrorData errorData,
		Func<TValue, TValue2> valueMapper) =>
		MapOrElse(errorMapper, errorData, static (value, vm) => vm(value), valueMapper);

	public TValue2 MapOrElse<TValue2>(
		Func<TError, TValue2> errorMapper,
		Func<TValue, TValue2> valueMapper) =>
		MapOrElse(static (error, em) => em(error), errorMapper, static (value, vm) => vm(value), valueMapper);

	public Result<TValue, TError2> MapError<TError2, TData>(Func<TError, TData, TError2> errorMapper, TData data) =>
		IsOk ? new Result<TValue, TError2>(_value!) : new Result<TValue, TError2>(errorMapper(_error!, data));

	public Result<TValue, TError2> MapError<TError2>(Func<TError, TError2> errorMapper) =>
		MapError((error, func) => func(error), errorMapper);
}