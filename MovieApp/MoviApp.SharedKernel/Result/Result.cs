using MoviApp.SharedKernel.Errors;

namespace MoviApp.SharedKernel.Result
{
    public  class Result
    {
        public bool IsSuccess { get;}
        public Error Error { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess , Error error) 
        {
            IsSuccess = isSuccess;
            Error = error; ;
        }
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
