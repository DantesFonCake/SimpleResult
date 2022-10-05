namespace SimpleResult;

public partial struct Result<TValue, TError>
{
	public Result<TValue2, TError> And<TValue2>(Result<TValue2, TError> next) =>
		IsOk ? next : new Result<TValue2, TError>(_error!);

	public Result<TValue2, TError> And<TValue2>(TValue2 next) =>
		And(new Result<TValue2, TError>(next));

	public Result<TValue2, TError> AndThen<TValue2, TData>(Func<TValue, TData, Result<TValue2, TError>> then, TData data) =>
		IsOk ? then(_value!, data) : new Result<TValue2, TError>(_error!);

	public Result<TValue2, TError> AndThen<TValue2>(Func<TValue, Result<TValue2, TError>> then) =>
		AndThen((value, func) => func(value), then);

	public Result<TValue, TError2> Or<TError2>(in Result<TValue, TError2> next) =>
		IsError ? next : new Result<TValue, TError2>(_value!);

	public Result<TValue, TError2> Or<TError2>(TError2 next) => 
		Or(new Result<TValue, TError2>(next));

	public Result<TValue, TError2> OrThen<TError2, TData>(Func<TError, TData, Result<TValue, TError2>> then, TData data) =>
		IsError ? then(_error!, data) : new Result<TValue, TError2>(_value!);

	public Result<TValue, TError2> OrThen<TError2>(Func<TError, Result<TValue, TError2>> then) =>
		OrThen((error, func) => func(error), then);
}