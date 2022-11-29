namespace AbpSuite.Books
{
    public static class BookConsts
    {
        private const string DefaultSorting = "{0}Title asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Book." : string.Empty);
        }

        public const int TitleMinLength = 2;
        public const int TitleMaxLength = 25;
        public const int YearMinLength = 1;
        public const int YearMaxLength = 100;
    }
}