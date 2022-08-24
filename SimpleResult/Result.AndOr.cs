namespace SimpleResult;

public readonly partial struct Result<TValue, TError>
{
	public Result<TValue2, TError> And<TValue2>(Result<TValue2, TError> next) =>
		IsOk ? next : new Result<TValue2, TError>(_error!);

	public Result<TValue2, TError> AndThen<TValue2>(Func<TValue, Result<TValue2, TError>> then) =>
		IsOk ? then(_value!) : new Result<TValue2, TError>(_error!);

	public Result<TValue, TError2> Or<TError2>(Result<TValue, TError2> next) =>
		IsError ? next : new Result<TValue, TError2>(_value!);

	public Result<TValue, TError2> OrThen<TError2>(Func<TError, Result<TValue, TError2>> then) =>
		IsError ? then(_error!) : new Result<TValue, TError2>(_value!);
}