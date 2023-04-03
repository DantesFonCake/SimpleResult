namespace SimpleResult;

public partial struct Result<TValue, TError>
{
	public Result<TValue2, TError> And<TValue2>(Result<TValue2, TError> next) =>
		IsOk ? next : _error!;

	public Result<TValue2, TError> And<TValue2>(TValue2 next) =>
		And(new Result<TValue2, TError>(next));

	public Result<TValue2, TError> AndThen<TValue2, TData>(Func<TValue, TData, Result<TValue2, TError>> then,
		TData data) =>
		IsOk ? then(_value!, data) : _error!;

	public Result<TValue2, TError> AndThen<TValue2>(Func<TValue, Result<TValue2, TError>> then) =>
		AndThen(static (value, func) => func(value), then);

	public async Task<Result<TValue2, TError>> AndThen<TValue2, TData>(
		Func<TValue, TData, Task<Result<TValue2, TError>>> then, TData data) =>
		IsOk ? await then(_value!, data) : _error!;

	public Task<Result<TValue2, TError>> AndThen<TValue2>(Func<TValue, Task<Result<TValue2, TError>>> then) =>
		AndThen(static (value, func) => func(value), then);

	public async ValueTask<Result<TValue2, TError>> AndThen<TValue2, TData>(
		Func<TValue, TData, ValueTask<Result<TValue2, TError>>> then, TData data) =>
		IsOk ? await then(_value!, data) : _error!;

	public ValueTask<Result<TValue2, TError>> AndThen<TValue2>(Func<TValue, ValueTask<Result<TValue2, TError>>> then) =>
		AndThen(static (value, func) => func(value), then);


	public Result<TValue, TError2> Or<TError2>(Result<TValue, TError2> next) =>
		IsError ? next : _value!;

	public Result<TValue, TError2> Or<TError2>(TError2 next) =>
		Or(new Result<TValue, TError2>(next));

	public Result<TValue, TError2> OrElse<TError2, TData>(Func<TError, TData, Result<TValue, TError2>> @else,
		TData data) =>
		IsError ? @else(_error!, data) : _value!;

	public Result<TValue, TError2> OrElse<TError2>(Func<TError, Result<TValue, TError2>> @else) =>
		OrElse(static (error, func) => func(error), @else);

	public async Task<Result<TValue, TError2>> OrElse<TError2, TData>(
		Func<TError, TData, Task<Result<TValue, TError2>>> @else, TData data) =>
		IsError ? await @else(_error!, data) : _value!;

	public Task<Result<TValue, TError2>> OrElse<TError2>(Func<TError, Task<Result<TValue, TError2>>> @else) =>
		OrElse(static (error, func) => func(error), @else);

	public async ValueTask<Result<TValue, TError2>> OrElse<TError2, TData>(
		Func<TError, TData, ValueTask<Result<TValue, TError2>>> @else, TData data) =>
		IsError ? await @else(_error!, data) : _value!;

	public ValueTask<Result<TValue, TError2>> OrElse<TError2>(Func<TError, ValueTask<Result<TValue, TError2>>> @else) =>
		OrElse(static (error, func) => func(error), @else);
}