namespace SimpleResult.Extensions;

public static class ResultExtensions
{
	public static Result<TValue, Unit> ToOk<TValue>(this TValue value)
	{
		return value;
	}
	
	public static Result<Unit, TError> ToError<TError>(this TError error)
	{
		return error;
	}

	public static Result<TValue, TError> ToOk<TValue, TError>(this TValue value)
	{
		return value;
	}
	
	public static Result<TValue, TError> ToError<TValue, TError>(this TError error)
	{
		return error;
	}

	public static Result<TValue, TError> AndThen<TValue, TError>(this Result<Unit, TError> result,
		Func<Result<TValue, TError>> then)
	{
		return result.IsOk
			? then()
			: result._error!;
	}
}