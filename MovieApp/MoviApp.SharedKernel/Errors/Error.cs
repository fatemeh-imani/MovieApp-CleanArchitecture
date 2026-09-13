

namespace MovieApp.SharedKernel.Errors
{
    public sealed record Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }

        public Error (string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }
        public static readonly Error None = new(
            string.Empty, string.Empty, ErrorType.Failure);

        public static  Error NotFound (
            string code, string message, ErrorType type)
        {
            return new(code , message, ErrorType.NotFound);
        }

        public static Error Validation(
            string code, string message, ErrorType type)
        {
            return new(code, message, ErrorType.Validation); 
        }
        public static Error Unauthorized(
            string code, string message)
        {
            return new Error(code, message, ErrorType.Unauthorized);
        }
        public static Error Forbidden(
            string code, string message)
        {
            return new Error(code, message, ErrorType.Forbidden);
        }

        public static Error Failure(
            string code , string message)
        {
            return new(code,message, ErrorType.Failure);
        }
    }
}
