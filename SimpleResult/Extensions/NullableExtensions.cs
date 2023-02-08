namespace SimpleResult.Extensions;

public static class NullableExtensions
{
	public static Result<TValue, TError> ValueOr<TValue, TError>(this TValue? value, TError error) =>
		value is not null ? value : error;

	public static Result<TValue, TError> ValueOrElse<TValue, TError, TData>(this TValue? value,
		Func<TData, TError> errorFactory, TData data) =>
		value is not null ? value : errorFactory(data);

	public static Result<TValue, TError> ValueOrElse<TValue, TError>(this TValue? value, Func<TError> errorFactory) =>
		value.ValueOrElse(static ef => ef(), errorFactory);
}