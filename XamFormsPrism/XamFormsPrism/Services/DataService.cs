namespace XamFormsPrism.Services
{
    public class DataService : IDataService
    {
        private readonly IPlatformSpecificService _platformSpecificService;
        private readonly string _injectedString;

        public DataService(IPlatformSpecificService platformSpecificService, string injectedString)
        {
            _platformSpecificService = platformSpecificService;
            _injectedString = injectedString;
        }

        public string GetData()
        {
            return $"Title - {_platformSpecificService.GetData()} - {_injectedString}";
        }
    }
}