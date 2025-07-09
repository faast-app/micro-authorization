namespace ApiAuth.Common;

public class AppSettings
{
    public Integration Integration { get; set; } = new Integration();
}

public class Integration
{
    public Cavali Cavali { get; set; } = new Cavali();
    public Webhook Webhook { get; set; } = new Webhook();
}

public class Cavali
{
    public string SecretKey { get; set; } = string.Empty;
}
public class Webhook
{
    public string SecretKey { get; set; } = string.Empty;
}