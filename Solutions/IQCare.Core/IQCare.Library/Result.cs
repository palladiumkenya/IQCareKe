using System;
using System.Collections.Generic;

namespace IQCare.Library
{
    public class Result<T>
    {
        static readonly List<Error> Empty = new List<Error>();

        public T Value { get; }
        public IReadOnlyList<Error> Errors { get; }
        public bool IsValid => Errors.Count == 0;
        protected Result(T value, IReadOnlyList<Error> errors)
        {
            Value = value;
            Errors = errors ?? Empty;
        }

        public static implicit operator bool(Result<T> result)
            => result.Errors == null || result.Errors.Count == 0;

        public static Result<T> Valid(T value)
            => new Result<T>(value, null);

        public static Result<T> Invalid(string error)
            => Invalid(new Error[] { error });

        public static Result<T> Invalid(int code, string error)
            => Invalid(new Error[] { new Error(code, error) });

        public static Result<T> Invalid(IReadOnlyList<Error> errors)
        {
            if (errors == null || errors.Count == 0)
                throw new ArgumentException("Invalid result must have a list of errors");
            return new Result<T>(default(T), errors);
        }
    }

    public struct Error
    {
        public int Code { get; }
        public string Message { get; }

        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }


        public override string ToString()
            => $"Error code {Code}: {Message}";

        public static implicit operator Error(string message)
            => new Error(0, message);
    }
}