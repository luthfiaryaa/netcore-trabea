namespace Trabea.Presentation.Web.ViewModels;

public class PaginationInfoViewModel {
    public int TotalItem { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages {
        get {
            return (int)Math.Ceiling((double)TotalItem / PageSize);
        }
    }
}
