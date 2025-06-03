namespace ApiAuth.Common;

public class AppSettings
{
    public Integration Integration { get; set; } = new Integration();
}

public class Integration
{
    public Security Security { get; set; } = new Security();
}
public class Security
{
    public string SecretKey { get; set; } = string.Empty;
}