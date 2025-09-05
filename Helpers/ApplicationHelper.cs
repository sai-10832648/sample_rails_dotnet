namespace sample_rails_app_8th_edNT.Helpers
{
    public static class ApplicationHelper
    {
        // Returns the full title on a per-page basis
        public static string FullTitle(string pageTitle = "")
        {
            const string baseTitle = "Ruby on Rails Tutorial Sample App";
            return string.IsNullOrEmpty(pageTitle) ? baseTitle : $"{pageTitle} | {baseTitle}";
        }
    }
}
