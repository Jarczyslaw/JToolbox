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

        public new static Result<T> AsError(string error)
        {
            var result = new Result<T>();
            result.AddError(error);
            return result;
        }

        public new static Result<T> AsError(Exception exc)
        {
            var result = new Result<T>();
            result.AddError(exc);
            return result;
        }

        public override void Clear()
        {
            base.Clear();
            Value = default;
        }
    }
}