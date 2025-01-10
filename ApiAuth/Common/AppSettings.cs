namespace ApiAuth.Common;

public class AppSettings
{
    public Integration Integration { get; set; } = new Integration();
}

public class Integration
{
    public Cavali Cavali { get; set; } = new Cavali();
}

public class Cavali
{
    public string SecretKey { get; set; } = string.Empty;
}