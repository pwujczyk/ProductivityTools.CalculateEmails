namespace CalculateEmails.Configuration
{
    public interface IConfig
    {
        string QueneName { get; }
        string MQAdress { get; }

        string OnlineAddress { get; }
        string GetSqlServerConnectionString();

        string GetDataSourceConnectionString();

        string GetSqlDataSource();

        string GetSqlServerDataBaseName();
    }
}