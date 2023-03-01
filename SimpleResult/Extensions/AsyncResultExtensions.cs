namespace SimpleResult.Extensions;

public static class AsyncResultExtensions
{
	public static async Task<Result<TValue2, TError>> AndThen<TValue, TValue2, TError, TData>(
		this Task<Result<TValue, TError>> asyncResult,
		Func<TValue, TData, Task<Result<TValue2, TError>>> then, TData data)
	{
		var result = await asyncResult;
		return await result.AndThen(then, data);
	}

	public static Task<Result<TValue2, TError>> AndThen<TValue, TValue2, TError>(
		this Task<Result<TValue, TError>> asyncResult, Func<TValue, Task<Result<TValue2, TError>>> then) =>
		asyncResult.AndThen(static (value, func) => func(value), then);

	public static async ValueTask<Result<TValue2, TError>> AndThen<TValue, TValue2, TError, TData>(
		this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TValue, TData, ValueTask<Result<TValue2, TError>>> then, TData data)
	{
		var result = await asyncResult;
		return await result.AndThen(then, data);
	}

	public static ValueTask<Result<TValue2, TError>> AndThen<TValue, TValue2, TError>(
		this ValueTask<Result<TValue, TError>> asyncResult, Func<TValue, ValueTask<Result<TValue2, TError>>> then) =>
		asyncResult.AndThen(static (value, func) => func(value), then);

	public static async Task<Result<TValue, TError2>> OrElse<TValue, TError, TError2, TData>(
		this Task<Result<TValue, TError>> asyncResult,
		Func<TError, TData, Task<Result<TValue, TError2>>> @else, TData data)
	{
		var result = await asyncResult;
		return await result.OrElse(@else, data);
	}

	public static Task<Result<TValue, TError2>> OrElse<TValue, TError, TError2>(
		this Task<Result<TValue, TError>> asyncResult, Func<TError, Task<Result<TValue, TError2>>> @else) =>
		asyncResult.OrElse(static (error, func) => func(error), @else);

	public static async ValueTask<Result<TValue, TError2>> OrElse<TValue, TError, TError2, TData>(
		this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TError, TData, ValueTask<Result<TValue, TError2>>> @else, TData data)
	{
		var result = await asyncResult;
		return await result.OrElse(@else, data);
	}

	public static ValueTask<Result<TValue, TError2>> OrElse<TValue, TError, TError2>(
		this ValueTask<Result<TValue, TError>> asyncResult, Func<TError, ValueTask<Result<TValue, TError2>>> @else) =>
		asyncResult.OrElse(static (error, func) => func(error), @else);

	public static async Task<Result<TValue2, TError>> Map<TValue, TValue2, TError, TData>(
		this Task<Result<TValue, TError>> asyncResult, Func<TValue, TData, Task<TValue2>> valueMapper,
		TData data)
	{
		var result = await asyncResult;
		return await result.Map(valueMapper, data);
	}

	public static Task<Result<TValue2, TError>> Map<TValue, TValue2, TError>(
		this Task<Result<TValue, TError>> asyncResult, Func<TValue, Task<TValue2>> valueMapper) =>
		asyncResult.Map(static (value, map) => map(value), valueMapper);

	public static async ValueTask<Result<TValue2, TError>> Map<TValue, TValue2, TError, TData>(
		this ValueTask<Result<TValue, TError>> asyncResult, Func<TValue, TData, ValueTask<TValue2>> valueMapper,
		TData data)
	{
		var result = await asyncResult;
		return await result.Map(valueMapper, data);
	}

	public static ValueTask<Result<TValue2, TError>> Map<TValue, TValue2, TError>(
		this ValueTask<Result<TValue, TError>> asyncResult, Func<TValue, ValueTask<TValue2>> valueMapper) =>
		asyncResult.Map(static (value, map) => map(value), valueMapper);

	public static async Task<TValue2> MapOr<TValue, TValue2, TError, TData>(
		this Task<Result<TValue, TError>> asyncResult, Func<TValue, TData, Task<TValue2>> valueMapper, TData data,
		TValue2 defaultValue)
	{
		var result = await asyncResult;
		return await result.MapOr(valueMapper, data, defaultValue);
	}

	public static Task<TValue2> MapOr<TValue, TValue2, TError>(this Task<Result<TValue, TError>> asyncResult,
		Func<TValue, Task<TValue2>> valueMapper, TValue2 defaultValue) =>
		asyncResult.MapOr(static (value, map) => map(value), valueMapper, defaultValue);

	public static async ValueTask<TValue2> MapOr<TValue, TValue2, TError, TData>(
		this ValueTask<Result<TValue, TError>> asyncResult, Func<TValue, TData, ValueTask<TValue2>> valueMapper,
		TData data,
		TValue2 defaultValue)
	{
		var result = await asyncResult;
		return await result.MapOr(valueMapper, data, defaultValue);
	}

	public static ValueTask<TValue2> MapOr<TValue, TValue2, TError>(this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TValue, ValueTask<TValue2>> valueMapper, TValue2 defaultValue) =>
		asyncResult.MapOr(static (value, map) => map(value), valueMapper, defaultValue);

	public static async Task<TValue2> MapOrElse<TValue, TError, TErrorData, TValue2, TValueData>(
		this Task<Result<TValue, TError>> asyncResult,
		Func<TError, TErrorData, Task<TValue2>> errorMapper, TErrorData errorData,
		Func<TValue, TValueData, Task<TValue2>> valueMapper, TValueData valueData)
	{
		var result = await asyncResult;
		return await result.MapOrElse(errorMapper, errorData, valueMapper, valueData);
	}

	public static Task<TValue2> MapOrElse<TValue, TError, TValue2, TValueData>(
		this Task<Result<TValue, TError>> asyncResult,
		Func<TError, Task<TValue2>> errorMapper,
		Func<TValue, TValueData, Task<TValue2>> valueMapper, TValueData valueData) =>
		asyncResult.MapOrElse(static (error, em) => em(error), errorMapper, valueMapper, valueData);

	public static Task<TValue2> MapOrElse<TValue, TError, TErrorData, TValue2>(
		this Task<Result<TValue, TError>> asyncResult,
		Func<TError, TErrorData, Task<TValue2>> errorMapper, TErrorData errorData,
		Func<TValue, Task<TValue2>> valueMapper) =>
		asyncResult.MapOrElse(errorMapper, errorData, static (value, vm) => vm(value), valueMapper);

	public static Task<TValue2> MapOrElse<TValue, TError, TValue2>(
		this Task<Result<TValue, TError>> asyncResult,
		Func<TError, Task<TValue2>> errorMapper,
		Func<TValue, Task<TValue2>> valueMapper) =>
		asyncResult.MapOrElse(static (error, em) => em(error), errorMapper, static (value, vm) => vm(value),
			valueMapper);

	public static async ValueTask<TValue2> MapOrElse<TValue, TError, TErrorData, TValue2, TValueData>(
		this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TError, TErrorData, ValueTask<TValue2>> errorMapper, TErrorData errorData,
		Func<TValue, TValueData, ValueTask<TValue2>> valueMapper, TValueData valueData)
	{
		var result = await asyncResult;
		return await result.MapOrElse(errorMapper, errorData, valueMapper, valueData);
	}

	public static ValueTask<TValue2> MapOrElse<TValue, TError, TValue2, TValueData>(
		this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TError, ValueTask<TValue2>> errorMapper,
		Func<TValue, TValueData, ValueTask<TValue2>> valueMapper, TValueData valueData) =>
		asyncResult.MapOrElse(static (error, em) => em(error), errorMapper, valueMapper, valueData);

	public static ValueTask<TValue2> MapOrElse<TValue, TError, TErrorData, TValue2>(
		this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TError, TErrorData, ValueTask<TValue2>> errorMapper, TErrorData errorData,
		Func<TValue, ValueTask<TValue2>> valueMapper) =>
		asyncResult.MapOrElse(errorMapper, errorData, static (value, vm) => vm(value), valueMapper);

	public static ValueTask<TValue2> MapOrElse<TValue, TError, TValue2>(
		this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TError, ValueTask<TValue2>> errorMapper,
		Func<TValue, ValueTask<TValue2>> valueMapper) =>
		asyncResult.MapOrElse(static (error, em) => em(error), errorMapper, static (value, vm) => vm(value),
			valueMapper);

	public static async Task<Result<TValue, TError2>> MapError<TValue, TError, TError2, TData>(
		this Task<Result<TValue, TError>> asyncResult, Func<TError, TData, Task<TError2>> errorMapper, TData data)
	{
		var result = await asyncResult;
		return await result.MapError(errorMapper, data);
	}

	public static Task<Result<TValue, TError2>> MapError<TValue, TError, TError2>(
		this Task<Result<TValue, TError>> asyncResult, Func<TError, Task<TError2>> errorMapper) =>
		asyncResult.MapError(static (error, func) => func(error), errorMapper);

	public static async ValueTask<Result<TValue, TError2>> MapError<TValue, TError, TError2, TData>(
		this ValueTask<Result<TValue, TError>> asyncResult, Func<TError, TData, ValueTask<TError2>> errorMapper,
		TData data)
	{
		var result = await asyncResult;
		return await result.MapError(errorMapper, data);
	}

	public static ValueTask<Result<TValue, TError2>> MapError<TValue, TError, TError2>(
		this ValueTask<Result<TValue, TError>> asyncResult, Func<TError, ValueTask<TError2>> errorMapper) =>
		asyncResult.MapError(static (error, func) => func(error), errorMapper);

	public static async Task<T> Match<TValue, TError, T, TValueData, TErrorData>(
		this Task<Result<TValue, TError>> asyncResult,
		Func<TValue, TValueData, Task<T>> valueArm,
		TValueData valueData,
		Func<TError, TErrorData, Task<T>> errorArm,
		TErrorData errorData
	)
	{
		var result = await asyncResult;
		return await result.Match(valueArm, valueData, errorArm, errorData);
	}

	public static Task<T> Match<TValue, TError, T, TErrorData>(this Task<Result<TValue, TError>> asyncResult,
		Func<TValue, Task<T>> valueArm,
		Func<TError, TErrorData, Task<T>> errorArm,
		TErrorData errorData
	) => asyncResult.Match(static (value, func) => func(value), valueArm, errorArm, errorData);

	public static Task<T> Match<TValue, TError, T, TValueData>(this Task<Result<TValue, TError>> asyncResult,
		Func<TValue, TValueData, Task<T>> valueArm,
		TValueData valueData,
		Func<TError, Task<T>> errorArm
	) => asyncResult.Match(valueArm, valueData, static (error, func) => func(error), errorArm);

	public static Task<T> Match<TValue, TError, T>(this Task<Result<TValue, TError>> asyncResult,
		Func<TValue, Task<T>> valueArm,
		Func<TError, Task<T>> errorArm
	) => asyncResult.Match(static (value, func) => func(value), valueArm,
		static (error, func) => func(error), errorArm);

	public static async ValueTask<T> Match<TValue, TError, T, TValueData, TErrorData>(
		this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TValue, TValueData, ValueTask<T>> valueArm,
		TValueData valueData,
		Func<TError, TErrorData, ValueTask<T>> errorArm,
		TErrorData errorData
	)
	{
		var result = await asyncResult;
		return await result.Match(valueArm, valueData, errorArm, errorData);
	}

	public static ValueTask<T> Match<TValue, TError, T, TErrorData>(
		this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TValue, ValueTask<T>> valueArm,
		Func<TError, TErrorData, ValueTask<T>> errorArm,
		TErrorData errorData
	) => asyncResult.Match(static (value, func) => func(value), valueArm, errorArm, errorData);

	public static ValueTask<T> Match<TValue, TError, T, TValueData>(
		this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TValue, TValueData, ValueTask<T>> valueArm,
		TValueData valueData,
		Func<TError, ValueTask<T>> errorArm
	) => asyncResult.Match(valueArm, valueData, static (error, func) => func(error), errorArm);

	public static ValueTask<T> Match<TValue, TError, T>(
		this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TValue, ValueTask<T>> valueArm,
		Func<TError, ValueTask<T>> errorArm
	) => asyncResult.Match(static (value, func) => func(value), valueArm,
		static (error, func) => func(error), errorArm);

	public static async Task<Result<TValue, TError>> OnValue<TValue, TError, TData>(
		this Task<Result<TValue, TError>> asyncResult, Func<TValue, TData, Task> func, TData data)
	{
		var result = await asyncResult;
		return await result.OnValue(func, data);
	}

	public static Task<Result<TValue, TError>> OnValue<TValue, TError>(
		this Task<Result<TValue, TError>> asyncResult, Func<TValue, Task> func) =>
		asyncResult.OnValue(static (value, func) => func(value), func);

	public static async ValueTask<Result<TValue, TError>> OnValue<TValue, TError, TData>(
		this ValueTask<Result<TValue, TError>> asyncResult, Func<TValue, TData, ValueTask> func, TData data)
	{
		var result = await asyncResult;
		return await result.OnValue(func, data);
	}

	public static ValueTask<Result<TValue, TError>> OnValue<TValue, TError>(
		this ValueTask<Result<TValue, TError>> asyncResult, Func<TValue, ValueTask> func) =>
		asyncResult.OnValue(static (value, func) => func(value), func);

	public static async Task<Result<TValue, TError>> OnError<TValue, TError, TData>(
		this Task<Result<TValue, TError>> asyncResult, Func<TError, TData, Task> func, TData data)
	{
		var result = await asyncResult;
		return await result.OnError(func, data);
	}

	public static Task<Result<TValue, TError>> OnError<TValue, TError>(this Task<Result<TValue, TError>> asyncResult,
		Func<TError, Task> func) => asyncResult.OnError(static (value, func) => func(value), func);

	public static async Task<TValue> UnwrapOrElse<TValue, TError, TData>(this Task<Result<TValue, TError>> asyncResult,
		Func<TError, TData, Task<TValue>> map, TData data)
	{
		var result = await asyncResult;
		return await result.UnwrapOrElse(map, data);
	}

	public static Task<TValue> UnwrapOrElse<TValue, TError, TData>(this Task<Result<TValue, TError>> asyncResult,
		Func<TError, Task<TValue>> map) =>
		asyncResult.UnwrapOrElse(static (error, map) => map(error), map);
	
	public static async ValueTask<TValue> UnwrapOrElse<TValue, TError, TData>(this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TError, TData, ValueTask<TValue>> map, TData data)
	{
		var result = await asyncResult;
		return await result.UnwrapOrElse(map, data);
	}

	public static ValueTask<TValue> UnwrapOrElse<TValue, TError, TData>(this ValueTask<Result<TValue, TError>> asyncResult,
		Func<TError, ValueTask<TValue>> map) =>
		asyncResult.UnwrapOrElse(static (error, map) => map(error), map);
}