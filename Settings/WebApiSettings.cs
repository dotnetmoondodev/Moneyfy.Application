namespace Application.Settings;

public class WebApiSettings: BaseSettings
{
    public string? DBConnection { get; init; }
    public string? KeyVaultName { get; init; }
    public string? JaegerServerUrl { get; init; }

    public override bool DataIsValid()
    {
        return base.DataIsValid() &&
            !string.IsNullOrEmpty( DBConnection ) &&
            !string.IsNullOrEmpty( JaegerServerUrl );
    }
}