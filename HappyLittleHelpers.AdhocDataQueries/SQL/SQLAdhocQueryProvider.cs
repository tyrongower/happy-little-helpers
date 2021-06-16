namespace HappyLittleHelpers.AdhocDataQueries.SQL
{
    public class SQLAdhocQueryProvider : AdhocQueryProvider
    {
        public SQLAdhocQueryProvider(string connectionString) : base(connectionString)
        {
            ConnectionProvider = new SQLConnectionProvider();
        }

        public override void Configure(AdhocQueryProviderConfiguration configuration)
        {
            configuration.SetParameterPrefix();
        }
    }
}