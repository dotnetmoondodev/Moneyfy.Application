namespace Application.Settings;

public class GatewaySettings: BaseSettings
{
    public double MaxWindowSecondsTimeout { get; init; }
    public int MaxPermitCounters { get; init; }

    public override bool DataIsValid()
    {
        return base.DataIsValid() &&
            ( MaxWindowSecondsTimeout > 0 ) &&
            ( MaxPermitCounters > 0 );
    }
}