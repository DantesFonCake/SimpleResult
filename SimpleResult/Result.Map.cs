namespace SimpleResult;

public readonly partial struct Result<TValue, TError>
{
	public Result<TValue2, TError> Map<TValue2>(Func<TValue, Result<TValue2, TError>> valueMapper) =>
		IsOk ? valueMapper(_value!) : new Result<TValue2, TError>(_error!);

	public Result<TValue2, TError> Map<TValue2>(Func<TValue, TValue2> valueMapper) => 
		IsOk ? new Result<TValue2, TError>(valueMapper(_value!)) : new Result<TValue2, TError>(_error!);

	public TValue2 MapOr<TValue2>(Func<TValue, TValue2> valueMapper, TValue2 defaultValue) =>
		IsOk ? valueMapper(_value!) : defaultValue;

	public Result<TValue, TError2> MapError<TError2>(Func<TError, TError2> errorMapper) =>
		IsOk ? new Result<TValue, TError2>(_value!) : new Result<TValue, TError2>(errorMapper(_error!));
}