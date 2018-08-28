namespace ProductivityTools.CalculateEmails.Configuration
{
    public interface IConfig
    {
        string QueneName { get; }
        string MQAdress { get; }
        string OnlineAddress { get; }
        string OnlineWebAddress { get; }
        string GetSqlServerConnectionString();
        string GetDataSourceConnectionString();
        string GetSqlDataSource { get; }
        string GetSqlServerDataBaseName { get; }
    }
}