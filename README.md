# SimpleResult
Attempt at copying Rust's Result in C#  
## Delegate usage
Library uses delegate (like `Func`) types heavily and so, to prevent unnecessary closure allocation, all methods that use them have overloads that take additional generic parameter that will be passed to delegate alongside its original argument  
```csharp
Example:
Result<TValue2, TError> Map<TValue2>(Func<TValue, TValue2> map);
Result<TValue2, TError> Map<TValue2, TData>(Func<TValue, TData, TValue2> map, TData data);
```
## Async interface
Most methods that accept delegates contain overloads that accept same delegates but with `Task` or `ValueTask` return types
```csharp
Example:
Result<TValue2, TError> Map<TValue2>(Func<TValue, TValue2> valueMapper);Task<Result<TValue2, TError>> Map<TValue2>(Func<TValue, Task<TValue2>> valueMapper);
ValueTask<Result<TValue2, TError>> Map<TValue2>(Func<TValue, ValueTask<TValue2>> valueMapper);
```
## Exception handling
Most methods that accept delegates does not expect them to throw. For now only exception to this are `Try` methods that handle thrown exception. 
## Control Flow methods  
Result can be used to perform flow control based on your result' status
### `And` and `AndThen`
```csharp
Result<TValue2, TError> And<TValue2>(Result<TValue2, TError> res);
Result<TValue2, TError> AndThen<TValue2>(Func<TValue, Result<TValue2, TError>> then);
```
Both methods behaves similarly - if original result is in status `Ok` method returns `res` (or result of `then` called with original result's value). Else original `Error` is returned.
### `Or` and `OrElse`
```csharp
Result<TValue, TError2> Or<TError2>(Result<TValue, TError2> res);
Result<TValue, TError2> OrElse<TValue2>(Func<TError, Result<TValue, TError2>> @else);
```
If original result is in status `Ok` method returns original value. Else `res` is returned (or result of `@else` called with original result's error)
## Mapping
Library provides selection of mapping methods
### Map
```csharp
Result<TValue2, TError> Map<TValue2>(Func<TValue, TValue2> valueMapper);
```
Maps value of result from `TValue` to `TValue2` using `valueMapper`. If result is in status error - does nothing
### MapOr
```csharp
TValue2 MapOr<TValue2>(
    Func<TValue, TValue2> valueMapper,
    TValue2 defaultValue);
```
Maps value of result from `TValue` to `TValue2` using `valueMapper`. If result is in status error - returns `defaultValue`
### MapOrElse
```csharp
TValue2 MapOrElse<TValue2>(
		Func<TError, TValue2> errorMapper,
		Func<TValue, TValue2> valueMapper);
```
Maps value of result from `TValue` to `TValue2` using `valueMapper`. If result is in status error - returns maps error of type `TError` to `TValue2` using `errorMapper`
### MapError
```csharp
Result<TValue, TError2> MapError<TError2>(Func<TError, TError2> errorMapper);
```
Maps error of result from `TError` to `TError2` using `errorMapper`. If result is in status ok - does nothing
### Match
```csharp
T Match<T>(
        Func<TValue, T> valueArm,
        Func<TError, T> errorArm);
```
Maps either value or error of original result from their type to `T`
## Inspecting
You can inspect value or error of result using either `OnValue` or `OnError` respectively
### OnValue
```csharp
Result<TValue, TError> OnValue(Action<TValue> action);
```
Performs an `action` with result's value if it's in status `Ok`. Otherwise does nothing. Either case returns original result 
### OnError
```csharp
Result<TValue, TError> OnError(Action<TError> action);
```
Performs an `action` with result's error if it's in status `Error`. Otherwise does nothing. Either case returns original result 
## Unwrapping
You can extract value (or error) from the result
### Unwrap
```csharp
TValue Unwrap();
```
Returns value from result. If result is in status `Error` throws an `InvalidUnwrapException<TError>` containing the error
### UnwrapError
```csharp
TError UnwrapError();
```
Returns error from result. If result is in status `Ok` throws an `InvalidUnwrapException<TValue>` containing the value
### UnwrapOr
```csharp
TValue UnwrapOr(TValue defaultValue);
```
If result is in status `Ok` returns value from result. Otherwise returns `defaultValue`
### UnwrapOrElse
```csharp
TValue UnwrapOrElse(Func<TError, TValue> map);
```
If result is in status `Ok` returns value from result. Otherwise performs a mapping from error to `TValue` using provided `map`
### UnwrapOrDefault
```csharp
TValue? UnwrapOrDefault();
```
If result is in status `Ok` returns value from result. Otherwise returns default value for the type
## Convenience features
### Implicit conversions
Value of type `TValue` or `TError` can be implicitly converted to `Result<TValue, TError>` in its respective state
```csharp
Result<TValue, TError> Do(){
    TValue value = ...;
    ...
    return value; // implicit conversion to Result in state Ok.
}

Result<TValue, TError> Do(){
    TError error = ...;
    ...
    return error; // implicit conversion to Result in state Error.
}
```
### Try... methods
Static class `Result` have 2 `Try` methods. They can be used to wrap unsafe (throwing) delegates in a `Result<TValue, TError>` type
```csharp
Result<TValue, TError> Try<TValue, TError>(Func<TValue> func, Func<Exception, TError> exceptionMapper); // tries to get value with func, otherwise maps thrown exception with provided exceptionMapper
Result<TValue, Exception> Try<TValue>(Func<TValue> func); // tries to get value with func, otherwise returns thrown exception
```
### ValueOr and ValueOrElse
Both methods are used to convert a nullable to Result
#### ValueOr
```csharp
Result<TValue, TError> ValueOr<TValue, TError>(this TValue? value, TError error);
```
If value is not null returns `value`. Otherwise returns `error`
#### ValueOrElse
```csharp
Result<TValue, TError> ValueOrElse<TValue, TError>(this TValue? value, Func<TError> errorFactory);
```
If value is not null returns `value`. Otherwise creates `TError` using `errorFactory` and returns it