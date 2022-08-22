namespace LectureSchedule.Data.Pagination
{
    public class PageParams
    {
        public const int MaxPageSize = 50;

        public int PageSize { get; set; } = 10;

        private int pageNumber = 1;

        public int PageNumber
        {
            get { return pageNumber; }
            set
            {
                if (value > MaxPageSize)
                    pageNumber = MaxPageSize;
                else if (value < 0)
                    pageNumber = 0;
                else
                    pageNumber = value;
            }
        }

        public string Term { get; set; } = string.Empty;
    }
}
