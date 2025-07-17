namespace ApiAuth.Common;

public class AppSettings
{
    public Integration Integration { get; set; } = new Integration();
}

public class Integration
{
    public Cavali Cavali { get; set; } = new Cavali();
    public PortalDigital PortalDigital { get; set; } = new PortalDigital();
    public Security Security { get; set; } = new Security();
}

public class Cavali
{
    public string SecretKey { get; set; } = string.Empty;
}
public class PortalDigital
{
    public string SecretKey { get; set; } = string.Empty;
}
public class Security
{
    public string SecretKey { get; set; } = string.Empty;
}