
using Foms.Shared.Settings;

namespace Foms.CoreDomain.Online
{
    public class CoreDomainProvider
    {
        private readonly ICoreDomain _iCoreDomain = null;
        private static CoreDomainProvider _theUniqueInstance;

        private CoreDomainProvider()
        {
                _iCoreDomain = new Standard();
        }

        public static ICoreDomain GetInstance()
        {
            if (_theUniqueInstance == null)
                _theUniqueInstance = new CoreDomainProvider();

            return _theUniqueInstance._iCoreDomain;
        }
    }
}
