﻿
namespace MamyCare.abstraction
{
    public class Result
    {
        public Result(bool isSuccess, Error error)
        {
            if ((isSuccess && error != Error.None) && (IsSuccess == false && error == Error.None))
            {
                throw new ArgumentException("Invalid operation");
            }
            IsSuccess = isSuccess;
              Error = error;
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public Error Error { get; set; }
        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);

        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
        public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        public Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Failure results cannot have value");
    }
}