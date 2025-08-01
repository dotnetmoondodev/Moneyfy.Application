namespace Application.Payments;

public static class ApiEndpoints
{
    private const string VER_FORMAT = "{v:apiVersion}";
    private const string API_BASE = $"{WebApiBaseUrl}/api/v{VER_FORMAT}";

    public const double CurrentVersion = 1.0;
    public const string WebApiBaseUrl = "payments";

    public static string MapVersion( string endpoint )
    {
        return endpoint.Replace( VER_FORMAT, Math.Truncate( CurrentVersion ).ToString() );
    }

    public static class Payments
    {
        private const string BASE = API_BASE;

        public const string Create = BASE;
        public const string GetAll = BASE;
        public const string GetOne = $"{BASE}/{{id:guid}}";
        public const string Update = BASE;
        public const string Delete = $"{BASE}/{{id:guid}}";
        public const string Base = BASE;
    }

    public static class Health
    {
        private const string BASE = $"{API_BASE}/health";

        public const string Live = $"{BASE}/live";
        public const string Ready = $"{BASE}/ready";
    }
}
