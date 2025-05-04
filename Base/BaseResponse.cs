
namespace Base
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public BaseResponse()
        {
            Success = true;
        }

        public BaseResponse(T data, string message = "")
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }
}