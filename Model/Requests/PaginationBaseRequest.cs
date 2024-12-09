namespace Model.Requests
{
    public class PaginationBaseRequest
    {
        public int page { get; set; } = 1;
        public int limit { get; set; } = 10;
        public string? sortBy { get; set; } = null;
        public bool accending { get; set; } = true;
    }
}
