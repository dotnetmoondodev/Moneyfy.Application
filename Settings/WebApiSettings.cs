namespace Application.Settings;

public class WebApiSettings: BaseSettings
{
    public static string DBConnUrl => $"{nameof( WebApiSettings )}:DBConnection";
    public string? DBConnection { get; init; }
    public string? KeyVaultName { get; init; }

    public override bool DataIsValid()
    {
        return base.DataIsValid() &&
            !string.IsNullOrEmpty( DBConnection );
    }
}