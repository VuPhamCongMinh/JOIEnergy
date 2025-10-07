using JOIEnergy.Repositories;

namespace JOIEnergy.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public string GetPricePlanIdForSmartMeterId(string smartMeterId)
        {
            return _accountRepository.GetPricePlanForSmartMeter(smartMeterId);
        }
    }
}
