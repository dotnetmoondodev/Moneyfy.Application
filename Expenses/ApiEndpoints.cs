namespace Application.Expenses;

public static class ApiEndpoints
{
    private const string API_BASE = "api/v{v:apiVersion}";

    public static class Expenses
    {
        private const string BASE = $"{API_BASE}/expenses";

        public const string Create = BASE;
        public const string GetAll = BASE;
        public const string GetOne = $"{BASE}/{{id:guid}}";
        public const string Update = BASE;
        public const string Delete = $"{BASE}/{{id:guid}}";
    }

    public static class Health
    {
        private const string BASE = $"{API_BASE}/health";

        public const string Live = $"{BASE}/live";
        public const string Ready = $"{BASE}/ready";
    }
}
