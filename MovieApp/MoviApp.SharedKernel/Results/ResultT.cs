using MovieApp.SharedKernel.Errors;


namespace MovieApp.SharedKernel.Results
{
    public class Result<T> : Result
    {
        public T? Value { get;}

        protected Result(T? value, bool isSuccess, Error error) 
            : base(isSuccess, error)
        {
            Value = value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value, true, Error.None);
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>(default,false,error);
        }
    }
}
