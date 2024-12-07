namespace Model.Responses
{
    public class BaseResponse<T>
    {
        public BaseResponse(T data, string? message = null)
        {
            Succeeded = true;
            Data = data;
            Message = message;
        }

        public BaseResponse(string message)
        {
            Message = message;
        }

        public T? Data { get; set; }
        public bool Succeeded { get; set; } = false;
        public string? Message { get; set; }
    }
}
