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
	
	public static async Task<Result<TValue, TError>> ValueOrElse<TValue, TError, TData>(this TValue? value,
		Func<TData, Task<TError>> errorFactory, TData data) =>
		value is not null ? value : await errorFactory(data);

	public static Task<Result<TValue, TError>> ValueOrElse<TValue, TError>(this TValue? value, Func<Task<TError>> errorFactory) =>
		value.ValueOrElse(static ef => ef(), errorFactory);
	
	public static async ValueTask<Result<TValue, TError>> ValueOrElse<TValue, TError, TData>(this TValue? value,
		Func<TData, ValueTask<TError>> errorFactory, TData data) =>
		value is not null ? value : await errorFactory(data);

	public static ValueTask<Result<TValue, TError>> ValueOrElse<TValue, TError>(this TValue? value, Func<ValueTask<TError>> errorFactory) =>
		value.ValueOrElse(static ef => ef(), errorFactory);
}