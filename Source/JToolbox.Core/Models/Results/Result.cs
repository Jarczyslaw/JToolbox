using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Models.Results
{
    public class Result
    {
        public Result()
        {
        }

        public Result(Result other)
        {
            Messages = other.Messages;
        }

        public Error Error => Errors.FirstOrDefault();

        public string ErrorContent => Error?.Content;

        public List<Error> Errors => GetMessages<Error>();

        public Information Information => Informations.FirstOrDefault();

        public List<Information> Informations => GetMessages<Information>();

        public bool IsError => !IsSuccess;

        public bool IsSuccess => Errors.Count == 0;

        public List<Message> Messages { get; } = new List<Message>();

        public Warning Warning => Warnings.FirstOrDefault();

        public List<Warning> Warnings => GetMessages<Warning>();

        public static Result AsError(
            string error,
            Exception exception = default,
            int code = default,
            object tag = default)
        {
            var result = new Result();
            result.AddError(error, exception, code, tag);
            return result;
        }

        public static Result AsError(
            Exception exception,
            int code = default,
            object tag = default)
        {
            var result = new Result();
            result.AddError(exception, code, tag);
            return result;
        }

        public void AddError(
            string error,
            Exception exception = default,
            int code = default,
            object tag = default)
        {
            AddMessage(new Error
            {
                Content = error,
                Exception = exception,
                Code = code,
                Tag = tag
            });
        }

        public void AddError(
            Exception exception,
            int code = default,
            object tag = default)
        {
            AddError(exception.Message, exception, code, tag);
        }

        public void AddInfo(
            string info,
            int code = default,
            object tag = default)
        {
            AddMessage(new Information
            {
                Content = info,
                Code = code,
                Tag = tag
            });
        }

        public void AddMessage(Message message) => Messages.Add(message);

        public void AddWarning(
            string warning,
            int code = default,
            object tag = default)
        {
            AddMessage(new Warning
            {
                Content = warning,
                Code = code,
                Tag = tag
            });
        }

        public virtual void Clear()
        {
            Messages.Clear();
        }

        public Result<TResult> OfType<TResult>(TResult value = default)
        {
            return new Result<TResult>(this)
            {
                Value = value
            };
        }

        private List<T> GetMessages<T>() => Messages.OfType<T>().ToList();
    }
}