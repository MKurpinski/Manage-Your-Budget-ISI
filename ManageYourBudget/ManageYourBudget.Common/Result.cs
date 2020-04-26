using System;
using System.Collections.Generic;

namespace ManageYourBudget.Common
{
    public class Result<T> : Result
    {
        public T Value { get; set; }

        private Result(T value, bool succeded, IDictionary<string, string> errorList): base(succeded, errorList)
        {
            Value = value;
        }

        public Result(T value) : this(value, true, default) { }

        public Result(IDictionary<string, string> errorList) : this(default, false, errorList) { }

        public Result(KeyValuePair<string, string> error) : this(default, false, new Dictionary<string, string>(){ {error.Key, error.Value} }) { }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public new static Result<T> Failure(IDictionary<string, string> errors = default)
        {
            return new Result<T>(default, false, errors);
        }
    }

    public class Result
    {
        public bool Succedeed { get; set; }
        public IDictionary<string, string> Errors { get; set; }

        protected Result(bool success, IDictionary<string, string> errors)
        {
            Succedeed = success;
            Errors = errors;
        }

        public static Result Failure(IDictionary<string, string> errors = default)
        {
            return new Result(false, errors);
        }

        public static Result Success(IDictionary<string, string> errors = default)
        {
            return new Result(true, errors);
        }

    }
}
