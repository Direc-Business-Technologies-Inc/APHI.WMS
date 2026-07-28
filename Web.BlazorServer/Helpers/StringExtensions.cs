namespace Web.BlazorServer.Helpers
{
    public static class StringExtensions
    {
        public static string OrWhenNull(this string? a, string alternative)
        {
            if (string.IsNullOrEmpty(a)) return alternative;
            return a;
        }
    }
}
