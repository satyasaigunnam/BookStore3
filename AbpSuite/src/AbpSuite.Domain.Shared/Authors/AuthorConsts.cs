namespace AbpSuite.Authors
{
    public static class AuthorConsts
    {
        private const string DefaultSorting = "{0}SureName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Author." : string.Empty);
        }

        public const int SureNameMinLength = 3;
        public const int SureNameMaxLength = 25;
        public const int AgeMinLength = 2;
        public const int AgeMaxLength = 90;
    }
}