
namespace MovieApp.Application.Movies.GetAllMovie
{
    public class PageResultMovie<T>
    {
        public IReadOnlyList<T> Items { get; init; } = [];
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages =>
            (int)Math.Ceiling((double)TotalCount / PageSize);
        //این Property تعداد کل صفحات Pagination را حساب می‌کند.

    }
}
