namespace Model.Requests
{
    public class ClassPaginatedRequest : PaginationBaseRequest
    {
        public string? ClassName { get; set; }
        public string? Grade { get; set; }
        public string? TeacherId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
