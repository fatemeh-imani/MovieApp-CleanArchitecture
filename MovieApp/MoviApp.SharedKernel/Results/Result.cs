using MovieApp.SharedKernel.Errors;

namespace MovieApp.SharedKernel.Results
{
    public  class Result(bool isSuccess, Error error)
    {
        public bool IsSuccess { get; } = isSuccess;
        public Error Error { get; }=error;
        public bool IsFailure => !IsSuccess;

       
        public static Result Success()
        {
            return new Result(true, Error.None);
        }
         public static Result Failure(Error error)
        {
            return new Result(false , error);
        }
    }
}
