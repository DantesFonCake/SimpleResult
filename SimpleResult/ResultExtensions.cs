namespace SimpleResult;

public static class ResultExtensions
{
	public static Result<TValue, Unit> ToOk<TValue>(this TValue value)
	{
		return new Result<TValue, Unit>(value);
	}
	
	public static Result<Unit, TError> ToError<TError>(this TError error)
	{
		return new Result<Unit, TError>(error);
	}

	public static Result<TValue, TError> ToOk<TValue, TError>(this TValue value)
	{
		return new Result<TValue, TError>(value);
	}
	
	public static Result<TValue, TError> ToError<TValue, TError>(this TError error)
	{
		return new Result<TValue, TError>(error);
	}

	public static Result<TValue, TError> AndThen<TValue, TError>(this Result<Unit, TError> result,
		Func<Result<TValue, TError>> then)
	{
		return result.IsOk
			? then()
			: new Result<TValue, TError>(result._error!);
	}
}