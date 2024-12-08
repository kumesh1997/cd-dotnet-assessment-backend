namespace Model.Responses
{
    public class BaseResponse<T>
    {
        public BaseResponse() { }
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

        public class ResponseData<T>
        {
            public string Message { get; set; }
            public Result<T> Result { get; set; }
        }

        public class Result<T>
        {
            public T[] Entities { get; set; }
            public Pagination Pagination { get; set; }
        }

        public class Pagination
        {
            public long Length { get; set; }
            public int PageSize { get; set; }
        }

        public T? Data { get; set; }
        public bool Succeeded { get; set; } = false;
        public string? Message { get; set; }
    }
}
