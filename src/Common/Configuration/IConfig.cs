namespace Configuration
{
    public interface IConfig
    {
        string Address { get; }
        string GetSqlServerConnectionString();

        string GetDataSourceConnectionString();

        string GetSqlDataSource();

        string GetSqlServerDataBaseName();
    }
}