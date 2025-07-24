namespace Application.Settings;

public abstract class BaseSettings
{
    public string? ServiceName { get; init; }
    public string? Authority { get; init; }
    public string? Audience { get; init; }
    public string? SeqServerUrl { get; init; }

    public virtual bool DataIsValid()
    {
        return !string.IsNullOrEmpty( ServiceName ) &&
            !string.IsNullOrEmpty( Authority ) &&
            !string.IsNullOrEmpty( Audience ) &&
            !string.IsNullOrEmpty( SeqServerUrl );
    }

    public virtual string MaskStrValue( string strValue )
    {
        if ( strValue.Length < 4 )
            return new string( '*', strValue.Length );

        return new string( '*', strValue.Length - 4 ) + strValue[^4..];
    }
}