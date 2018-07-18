using XamFormsPrism.Services;

namespace XamFormsPrism.Droid
{
    public class AndroidService : IPlatformSpecificService
    {
        public string GetData()
        {
            return "from Android";
        }
    }
}