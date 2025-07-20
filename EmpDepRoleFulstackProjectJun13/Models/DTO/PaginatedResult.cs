namespace EmpDepRoleFulstackProjectJun13.Models.DTO
{
    public class PaginatedResult<T>
    {
            public List<T> Data { get; set; } = new();      // List of records returned in current page
            public int PageNumber { get; set; }             // Current page number (e.g., 1, 2, 3)
            public int PageSize { get; set; }               // Number of records per page (e.g., 10)
            public int TotalRecords { get; set; }           // Total number of records found (after filtering)
            public int TotalPages { get; set; }             // Total pages = TotalRecords / PageSize (rounded up)

            public bool HasPreviousPage => PageNumber > 1;                     // Is there a page before this?
            public bool HasNextPage => PageNumber < TotalPages;               // Is there a page after this?
        }
    }
