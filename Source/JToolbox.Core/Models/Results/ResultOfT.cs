using System;

namespace JToolbox.Core.Models.Results
{
    public class Result<T> : Result
    {
        public Result(Result other)
            : base(other)
        {
        }

        public Result(Result<T> other)
            : base(other)
        {
            Value = other.Value;
        }

        public Result(T value = default)
        {
            Value = value;
        }

        public T Value { get; set; }

        public new static Result<T> AsError(
            string error,
            Exception exception = default,
            int code = default,
            object tag = default)
        {
            var result = new Result<T>();
            result.AddError(error, exception, code, tag);
            return result;
        }

        public new static Result<T> AsError(
            Exception exception,
            int code = default,
            object tag = default)
        {
            var result = new Result<T>();
            result.AddError(exception, code, tag);
            return result;
        }

        public override void Clear()
        {
            base.Clear();
            Value = default;
        }
    }
}