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

        public static Result AsError(string error)
        {
            var result = new Result();
            result.AddError(error);
            return result;
        }

        public static Result AsError(Exception exc)
        {
            var result = new Result();
            result.AddError(exc);
            return result;
        }

        public void AddError(string error)
        {
            Messages.Add(new Error
            {
                Content = error
            });
        }

        public void AddError(Exception exc)
        {
            Messages.Add(new Error
            {
                Content = exc.Message,
                Exception = exc
            });
        }

        public void AddInfo(string info)
        {
            Messages.Add(new Information
            {
                Content = info
            });
        }

        public void AddWarning(string warning)
        {
            Messages.Add(new Warning
            {
                Content = warning
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