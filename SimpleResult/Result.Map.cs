namespace SimpleResult;

public partial struct Result<TValue, TError>
{
	public Result<TValue2, TError> Map<TValue2, TData>(Func<TValue, TData, TValue2> valueMapper, TData data) =>
		IsOk ? new Result<TValue2, TError>(valueMapper(_value!, data)) : new Result<TValue2, TError>(_error!);

	public Result<TValue2, TError> Map<TValue2>(Func<TValue, TValue2> valueMapper) =>
		Map(static (value, func) => func(value), valueMapper);

	public async Task<Result<TValue2, TError>> Map<TValue2, TData>(Func<TValue, TData, Task<TValue2>> valueMapper,
		TData data) =>
		IsOk ? await valueMapper(_value!, data) : _error!;

	public Task<Result<TValue2, TError>> Map<TValue2>(Func<TValue, Task<TValue2>> valueMapper) =>
		Map(static (value, map) => map(value), valueMapper);

	public async ValueTask<Result<TValue2, TError>> Map<TValue2, TData>(
		Func<TValue, TData, ValueTask<TValue2>> valueMapper, TData data) =>
		IsOk ? await valueMapper(_value!, data) : _error!;

	public ValueTask<Result<TValue2, TError>> Map<TValue2>(Func<TValue, ValueTask<TValue2>> valueMapper) =>
		Map(static (value, map) => map(value), valueMapper);

	public TValue2 MapOr<TValue2, TData>(Func<TValue, TData, TValue2> valueMapper, TData data, TValue2 defaultValue) =>
		IsOk ? valueMapper(_value!, data) : defaultValue;

	public TValue2 MapOr<TValue2>(Func<TValue, TValue2> valueMapper, TValue2 defaultValue) =>
		MapOr(static (value, func) => func(value), valueMapper, defaultValue);

	public async Task<TValue2> MapOr<TValue2, TData>(Func<TValue, TData, Task<TValue2>> valueMapper, TData data,
		TValue2 defaultValue) =>
		IsOk ? await valueMapper(_value!, data) : defaultValue;

	public Task<TValue2> MapOr<TValue2>(Func<TValue, Task<TValue2>> valueMapper, TValue2 defaultValue) =>
		MapOr(static (value, map) => map(value), valueMapper, defaultValue);
	
	public async ValueTask<TValue2> MapOr<TValue2, TData>(Func<TValue, TData, ValueTask<TValue2>> valueMapper, TData data,
		TValue2 defaultValue) =>
		IsOk ? await valueMapper(_value!, data) : defaultValue;

	public ValueTask<TValue2> MapOr<TValue2>(Func<TValue, ValueTask<TValue2>> valueMapper, TValue2 defaultValue) =>
		MapOr(static (value, map) => map(value), valueMapper, defaultValue);

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
	
	public async Task<TValue2> MapOrElse<TErrorData, TValue2, TValueData>(
		Func<TError, TErrorData, Task<TValue2>> errorMapper, TErrorData errorData,
		Func<TValue, TValueData, Task<TValue2>> valueMapper, TValueData valueData) =>
		IsOk ? await valueMapper(_value!, valueData) : await errorMapper(_error!, errorData);

	public Task<TValue2> MapOrElse<TValue2, TValueData>(
		Func<TError, Task<TValue2>> errorMapper,
		Func<TValue, TValueData, Task<TValue2>> valueMapper, TValueData valueData) =>
		MapOrElse(static (error, em) => em(error), errorMapper, valueMapper, valueData);

	public Task<TValue2> MapOrElse<TErrorData, TValue2>(
		Func<TError, TErrorData, Task<TValue2>> errorMapper, TErrorData errorData,
		Func<TValue, Task<TValue2>> valueMapper) =>
		MapOrElse(errorMapper, errorData, static (value, vm) => vm(value), valueMapper);

	public Task<TValue2> MapOrElse<TValue2>(
		Func<TError, Task<TValue2>> errorMapper,
		Func<TValue, Task<TValue2>> valueMapper) =>
		MapOrElse(static (error, em) => em(error), errorMapper, static (value, vm) => vm(value), valueMapper);
	
	public async ValueTask<TValue2> MapOrElse<TErrorData, TValue2, TValueData>(
		Func<TError, TErrorData, ValueTask<TValue2>> errorMapper, TErrorData errorData,
		Func<TValue, TValueData, ValueTask<TValue2>> valueMapper, TValueData valueData) =>
		IsOk ? await valueMapper(_value!, valueData) : await errorMapper(_error!, errorData);

	public ValueTask<TValue2> MapOrElse<TValue2, TValueData>(
		Func<TError, ValueTask<TValue2>> errorMapper,
		Func<TValue, TValueData, ValueTask<TValue2>> valueMapper, TValueData valueData) =>
		MapOrElse(static (error, em) => em(error), errorMapper, valueMapper, valueData);

	public ValueTask<TValue2> MapOrElse<TErrorData, TValue2>(
		Func<TError, TErrorData, ValueTask<TValue2>> errorMapper, TErrorData errorData,
		Func<TValue, ValueTask<TValue2>> valueMapper) =>
		MapOrElse(errorMapper, errorData, static (value, vm) => vm(value), valueMapper);

	public ValueTask<TValue2> MapOrElse<TValue2>(
		Func<TError, ValueTask<TValue2>> errorMapper,
		Func<TValue, ValueTask<TValue2>> valueMapper) =>
		MapOrElse(static (error, em) => em(error), errorMapper, static (value, vm) => vm(value), valueMapper);

	public Result<TValue, TError2> MapError<TError2, TData>(Func<TError, TData, TError2> errorMapper, TData data) =>
		IsOk ? new Result<TValue, TError2>(_value!) : new Result<TValue, TError2>(errorMapper(_error!, data));

	public Result<TValue, TError2> MapError<TError2>(Func<TError, TError2> errorMapper) =>
		MapError(static (error, func) => func(error), errorMapper);
	
	public async Task<Result<TValue, TError2>> MapError<TError2, TData>(Func<TError, TData, Task<TError2>> errorMapper, TData data) =>
		IsOk ? _value! : await errorMapper(_error!, data);

	public Task<Result<TValue, TError2>> MapError<TError2>(Func<TError, Task<TError2>> errorMapper) =>
		MapError( static (error, func) => func(error), errorMapper);
	
	public async ValueTask<Result<TValue, TError2>> MapError<TError2, TData>(Func<TError, TData, ValueTask<TError2>> errorMapper, TData data) =>
		IsOk ? _value! : await errorMapper(_error!, data);

	public ValueTask<Result<TValue, TError2>> MapError<TError2>(Func<TError, ValueTask<TError2>> errorMapper) =>
		MapError( static (error, func) => func(error), errorMapper);
}