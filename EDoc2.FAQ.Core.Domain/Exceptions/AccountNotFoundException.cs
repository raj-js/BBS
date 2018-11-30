namespace EDoc2.FAQ.Core.Domain.Exceptions
{
    public class AccountNotFoundException : DomainException
    {
        public string AccountId { get; }

        public AccountNotFoundException(string accountId)
            :base($"未找到编号为 {accountId} 的用户")
        {
            AccountId = accountId;
        }
    }
}
