namespace Model.Requests
{
    public class ClassPaginatedRequest : PaginationBaseRequest
    {
        public string? ClassName { get; set; } = null;
        public string? Grade { get; set; } = null ;
        public string? TeacherId { get; set; } = null;
        public bool? IsDeleted { get; set; } = false ;
    }
}
