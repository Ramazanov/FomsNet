namespace Foms.Services
{
    public class ServicesProvider
    {
        private IServices _iServices = null;
        private static ServicesProvider _theUniqueInstance;

        private ServicesProvider()
        {
                _iServices = new Standard();
        }

        public static IServices GetInstance()
        {
            if (_theUniqueInstance == null)
                _theUniqueInstance = new ServicesProvider();

            return _theUniqueInstance._iServices;
        }

        public static ServicesProvider GetServiceProvider()
        {
            if (_theUniqueInstance == null)
                return _theUniqueInstance = new ServicesProvider();
            return _theUniqueInstance;
        }

        static bool _status = false;
        

    }
}
