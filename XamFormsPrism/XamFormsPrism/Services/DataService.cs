namespace XamFormsPrism.Services
{
    public class DataService : IDataService
    {
        private readonly string _injectedString;

        public DataService(string injectedString)
        {
            _injectedString = injectedString;
        }

        public string GetData()
        {
            return $"New Title - {_injectedString}";
        }
    }
}